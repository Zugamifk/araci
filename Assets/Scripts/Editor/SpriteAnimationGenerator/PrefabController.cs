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
            var prefab = PrefabUtility.SaveAsPrefabAssetAndConnect(go, assetPath, InteractionMode.UserAction);
            GameObject.DestroyImmediate(go);

            ConfigureNewSpriteAnimation(prefab);

            return prefab;
        }

        public void ConfigureNewSpriteAnimation(GameObject prefab)
        {
            var inst = (GameObject)PrefabUtility.InstantiatePrefab(prefab);

            var anim = inst.AddComponent<Animator>();
            var clip = new AnimationClip();

            var path = Path.Combine(k_AnimationAssetsPath, inst.name + ".controller");
            var cont = AnimatorController.CreateAnimatorControllerAtPathWithClip(path, clip);
            AssetDatabase.AddObjectToAsset(clip, cont);
            anim.runtimeAnimatorController = cont;

            var sa = inst.AddComponent<SpriteAnimation>();
            sa.AnimationClip = clip;

            var view = new GameObject("View");
            view.transform.SetParent(inst.transform);

            var sprite = view.AddComponent<SpriteRenderer>();

            PrefabUtility.ApplyPrefabInstance(inst, InteractionMode.UserAction);
            GameObject.DestroyImmediate(inst);
        }

        public void ApplySpriteAnimationChanges(GameObject prefab, string name, Texture2D texture, int frameCount, int rowCount, float time, bool loop)
        {
            ImportSprite(texture, frameCount, rowCount);
            
            var inst = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
            var sa = inst.GetComponent<SpriteAnimation>();
            sa.Texture = texture;
            sa.FrameCount = frameCount;
            sa.RowCount = rowCount;

            var anim = prefab.GetComponent<Animator>();
            var cont = anim.runtimeAnimatorController;
            var clip = cont.animationClips[0];
            var sprites = AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(texture)).OfType<Sprite>().ToArray();
            CreateAnimation(clip, sprites, time);
            if(!loop)
            {
                if (!prefab.GetComponent<AnimationEvents>())
                {
                    var ae = inst.AddComponent<AnimationEvents>();
                    ae.Root = inst;
                }

                var evt = new AnimationEvent()
                {
                    time = clip.length- time / frameCount,
                    functionName = "Destroy"
                };
                AnimationUtility.SetAnimationEvents(clip, new[] { evt });
            }

            PrefabUtility.ApplyPrefabInstance(inst, InteractionMode.UserAction);
            GameObject.DestroyImmediate(inst);

            RenameAssets(prefab, name);
        }

        public void DeleteAssets(GameObject prefab)
        {
            AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(prefab.GetComponent<Animator>().runtimeAnimatorController));
            AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(prefab));
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
            var h = tex.height / Mathf.CeilToInt(frameCount / (float)rowCount);
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
            EditorUtility.SetDirty(importer);
            importer.SaveAndReimport();
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

        void RenameAssets(GameObject root, string name)
        {
            AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(root), name);
            var anim = root.GetComponent<Animator>().runtimeAnimatorController;
            AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(anim), name);
            AssetDatabase.Refresh();
        }

    }
}
