using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

namespace SpriteAnimation
{
    internal class SpriteAnimationBuilder
    {
        public SpriteAnimationData CreateNewAnimationData(string name, string path)
        {
            // create prefab
            var go = new GameObject(name);
            var assetPath = Path.Combine(path, name + ".prefab");
            assetPath = AssetDatabase.GenerateUniqueAssetPath(assetPath);
            var prefab = PrefabUtility.SaveAsPrefabAssetAndConnect(go, assetPath, InteractionMode.UserAction);

            // add components
            var viewRoot = new GameObject("View");
            var anim = viewRoot.AddComponent<Animator>();
            var spriteRoot = new GameObject("Sprite");
            var renderer = spriteRoot.AddComponent<SpriteRenderer>();
            spriteRoot.transform.SetParent(viewRoot.transform);
            viewRoot.transform.SetParent(go.transform);

            // configure animator controller
            var controller = new AnimatorController();
            controller.name = "Animator";
            AssetDatabase.AddObjectToAsset(controller, prefab);
            anim.runtimeAnimatorController = controller;

            // configure data
            var data = ScriptableObject.CreateInstance<SpriteAnimationData>();
            data.name = "Data";
            data.Name = name;
            AssetDatabase.AddObjectToAsset(data, prefab);
            data.Prefab = prefab;
            data.Animator = anim;

            // cleanup
            PrefabUtility.ApplyPrefabInstance(go, InteractionMode.UserAction);
            GameObject.DestroyImmediate(go);

            return data;
        }

    }
}
