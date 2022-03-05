using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Editors.Spriteanimation
{
    public class PrefabController
    {
        const string k_SpriteAnimationAssetsPath = "Assets/Prefabs";

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

            root.AddComponent<Animator>();

            var view = new GameObject("View");
            view.transform.SetParent(root.transform);
            var sprite = view.AddComponent<SpriteRenderer>();

            PrefabUtility.ApplyPrefabInstance(root, InteractionMode.UserAction);
        }

        public void ApplySpriteAnimationChanges()
        {

        }
    }
}
