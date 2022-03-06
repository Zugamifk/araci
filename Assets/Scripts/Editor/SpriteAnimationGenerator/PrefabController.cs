using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Animations;

namespace Editors.Spriteanimation
{
    public class PrefabController
    {
        const string k_SpriteAnimationAssetsPath = "Assets/Prefabs";
        const string k_AnimationAssetsPath = "Assets/Animations";

        public GameObject CreateNewSpriteAnimationAsset(string name = "New Sprite Animation")
        {
            var go = new GameObject(name);
            var assetPath = Path.Combine(k_SpriteAnimationAssetsPath, name + ".prefab");
            assetPath = AssetDatabase.GenerateUniqueAssetPath(assetPath);
            PrefabUtility.SaveAsPrefabAssetAndConnect(go, assetPath, InteractionMode.UserAction);

            ConfigureNewSpriteAnimation(go);

            return go;
        }

        public void ConfigureNewSpriteAnimation(GameObject root)
        {
            root.AddComponent<SpriteAnimation>();

            var anim = root.AddComponent<Animator>();
            var clip = new AnimationClip();
            var path = Path.Combine(k_AnimationAssetsPath, root.name + ".controller");
            var cont = AnimatorController.CreateAnimatorControllerAtPathWithClip(path, clip);
            anim.runtimeAnimatorController = cont;
            var view = new GameObject("View");
            view.transform.SetParent(root.transform);
            var sprite = view.AddComponent<SpriteRenderer>();

            PrefabUtility.ApplyPrefabInstance(root, InteractionMode.UserAction);
        }

        public void ApplySpriteAnimationChanges(GameObject root, Texture2D texture, int frameCount, int rowCount, float time)
        {
            ImportSprite(texture, frameCount, rowCount);

            var anim = root.GetComponent<Animator>();
            var cont = anim.runtimeAnimatorController;
            var clip = cont.animationClips[0];

            var sprites = AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(texture)).OfType<Sprite>().ToArray();
            CreateAnimation(clip, sprites, time);
        }

        void ImportSprite(Texture2D texture, int frameCount, int rowCount)
        {
            var path = AssetDatabase.GetAssetPath(texture);
            var importer = AssetImporter.GetAtPath(path) as TextureImporter;
            if (importer == null)
            {
                Debug.Log("no importer");
                return;
            }

            importer.textureType = TextureImporterType.Sprite;
            importer.spriteImportMode = SpriteImportMode.Multiple;
            importer.filterMode = FilterMode.Point;
            importer.isReadable = true;

            AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);

            var tex = AssetDatabase.LoadAssetAtPath<Texture2D>(path);
            var w = tex.width / rowCount;
            var h = tex.height / Mathf.CeilToInt(frameCount / rowCount);
            var mt = new SpriteMetaData[frameCount];
            for (int i = 0; i < frameCount; i++)
            {
                mt[i] = new SpriteMetaData()
                {
                    name = $"{texture.name}_{i}",
                    rect = new Rect(w * (i % rowCount), h * (i / rowCount), w, h),
                    alignment = 0,
                    pivot = new Vector2(0, 0)
                };
            }

            importer.spritesheet = mt;
            AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
        }

        void CreateAnimation(AnimationClip clip, Sprite[] sprites, float time)
        {
            var frameCount = sprites.Length;
            clip.frameRate = frameCount / time;
            var ecb = new EditorCurveBinding();
            ecb.type = typeof(SpriteRenderer);
            ecb.path = "View";
            ecb.propertyName = "m_Sprite";

            var frames = new ObjectReferenceKeyframe[frameCount];
            float t = 0;
            float step = time / frameCount;
            for(int i=0;i<frameCount;i++)
            {
                frames[i] = new ObjectReferenceKeyframe()
                {
                    time = t,
                    value = sprites[i]
                };
                t += step;
            }

            AnimationUtility.SetObjectReferenceCurve(clip, ecb, frames);
        }
    }
}
