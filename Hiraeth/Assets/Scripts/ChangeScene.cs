using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public GameData data;
    public void StartClicked(string sceneName)
    { 
        DataPersistenceManager.instance.NewGame();

        SceneManager.LoadSceneAsync("Level1");
    }

     public void LoadClicked() 
    {
        // save the game anytime before loading a new scene
        DataPersistenceManager.instance.SaveGame();
        // load the next scene - which will in turn load the game because of 
        // OnSceneLoaded() in the DataPersistenceManager
        Debug.Log(data.LevelNum);
        SceneManager.LoadSceneAsync(data.LevelNum);
    }

    public void HomeClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
