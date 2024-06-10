using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{

    public enum State
    {
        MainMenuScene,
        LoadingScene,
        GameScene
    }

    private static State targetScene;

    public static void Load(State _targetScene)
    {
        targetScene = _targetScene;
        SceneManager.LoadScene(State.LoadingScene.ToString());
    }

    public static void LoadTargetScene()
    {
        SceneManager.LoadScene(targetScene.ToString()); 
    }


}
