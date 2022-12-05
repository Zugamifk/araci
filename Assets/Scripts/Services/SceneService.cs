using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Scenes
{
    const string BOOT_SCENE_NAME = "Game";

    [RuntimeInitializeOnLoadMethod]
    public static void LoadGameScene()
    {
        if(!SceneManager.GetSceneByName(BOOT_SCENE_NAME).isLoaded)
        {
            SceneManager.LoadSceneAsync(BOOT_SCENE_NAME, LoadSceneMode.Additive);
        }
    }

    public static void ReloadGame()
    {
        // unload all scene, load boot scene
    }
}
