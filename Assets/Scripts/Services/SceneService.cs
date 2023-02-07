using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenes : IService
{
    const string BOOT_SCENE_NAME = "Game";
    const string HUD_SCENE_NAME = "HUD";

    [RuntimeInitializeOnLoadMethod]
    public static void LoadGameScene()
    {
        if(!SceneManager.GetSceneByName(BOOT_SCENE_NAME).isLoaded)
        {
            SceneManager.LoadSceneAsync(BOOT_SCENE_NAME, LoadSceneMode.Additive);
        }

        if (!SceneManager.GetSceneByName(HUD_SCENE_NAME).isLoaded)
        {
            SceneManager.LoadSceneAsync(HUD_SCENE_NAME, LoadSceneMode.Additive);
        }
    }

    public void ReloadGame()
    {
        // unload all scene, load boot scene
    }
}
