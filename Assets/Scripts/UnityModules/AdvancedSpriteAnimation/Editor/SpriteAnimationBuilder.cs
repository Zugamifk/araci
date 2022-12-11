using Codice.Client.Common;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

namespace SpriteAnimation
{
    internal class SpriteAnimationBuilder
    {
        const string SPRITE_PROPERTY_PATH = "View/Spirte";
        const string SPRITE_PROPERTY_NAME = "m_Sprite";
        public SpriteAnimationData CreateNewAnimationData(string name, string path)
        {
            // create prefab
            var go = new GameObject(name);

            var viewRoot = new GameObject("View");
            var anim = viewRoot.AddComponent<Animator>();
            viewRoot.AddComponent<AnimationEvents>();
            var spriteRoot = new GameObject("Sprite");
            spriteRoot.AddComponent<SpriteRenderer>();
            spriteRoot.transform.SetParent(viewRoot.transform);
            viewRoot.transform.SetParent(go.transform);

            var assetPath = Path.Combine(path, name + ".prefab");
            assetPath = AssetDatabase.GenerateUniqueAssetPath(assetPath);
            var prefab = PrefabUtility.SaveAsPrefabAssetAndConnect(go, assetPath, InteractionMode.UserAction);

            // configure data
            var data = ScriptableObject.CreateInstance<SpriteAnimationData>();
            data.name = "Data";
            data.Name = name;
            data.Prefab = prefab;
            data.Animator = anim;
            AssetDatabase.AddObjectToAsset(data, prefab);

            // configure animator controller
            var controller = new AnimatorController();
            controller.name = "Animator";
            AssetDatabase.AddObjectToAsset(controller, prefab);

            var layer = new AnimatorControllerLayer();
            layer.name = "Base Layer";
            layer.stateMachine = new AnimatorStateMachine();
            layer.stateMachine.name = "Base Layer";
            layer.stateMachine.hideFlags = HideFlags.HideInHierarchy;
            controller.AddLayer(layer);
            AssetDatabase.AddObjectToAsset(layer.stateMachine, prefab);

            anim.runtimeAnimatorController = controller;

            PrefabUtility.ApplyPrefabInstance(go, InteractionMode.UserAction);
            GameObject.DestroyImmediate(go);

            return data;
        }

        public void CreateNewClipData(SpriteAnimationData data)
        {
            var clipData = new SpriteAnimationData.ClipData();

            data.Clips.Add(clipData);
            EditorUtility.SetDirty(data);
            AssetDatabase.SaveAssets();
        }

        public void RebuildAnimationData(SpriteAnimationData data)
        {
            foreach(var clipData in data.Clips)
            {
                CreateAnimationClip(data, clipData);
            }
        }

        void CreateAnimationClip(SpriteAnimationData data, SpriteAnimationData.ClipData clipData)
        {
            var clip = new AnimationClip();
            clip.name = clipData.Name;

            var anim = data.Prefab.GetComponentInChildren<Animator>();
            var controller = (AnimatorController)anim.runtimeAnimatorController;
            var sprites = AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(clipData.Source))
                .OfType<Sprite>()
                .Skip(clipData.StartIndex)
                .Take(clipData.FrameCount)
                .ToArray();
            var renderer = data.Prefab.GetComponentInChildren<SpriteRenderer>();
            renderer.sprite = sprites[clipData.StartIndex];

            var frameCount = sprites.Length;
            clip.frameRate = frameCount / clipData.Duration;
            var ecb = new EditorCurveBinding();
            ecb.type = typeof(SpriteRenderer);
            ecb.path = SPRITE_PROPERTY_PATH;
            ecb.propertyName = SPRITE_PROPERTY_NAME;

            var frames = new ObjectReferenceKeyframe[frameCount];
            float t = 0;
            float step = 1 / clip.frameRate;
            for (int i = 0; i < frameCount; i++)
            {
                frames[i] = new ObjectReferenceKeyframe()
                {
                    time = t,
                    value = sprites[i]
                };
                t += step;
            }

            AnimationUtility.SetObjectReferenceCurve(clip, ecb, frames);

            clip.wrapMode = clipData.Loop ? WrapMode.Loop : WrapMode.Once;
            controller.AddMotion(clip);

            AssetDatabase.AddObjectToAsset(clip, controller);
        }
    }
}
