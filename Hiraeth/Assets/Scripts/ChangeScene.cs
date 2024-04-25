using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour, IDataPersistence
{

    /*[SerializeField] private Button Play;

    [SerializeField] private Button Continue;



    private void Start()
    {
        if (!DataPersistenceManager.instance.HasGameData())
        {
            Continue.interactable = false;
        }
    }*/

    String Level;
    float Health;
    public Vector2 Position;


    public void LoadData(GameData data)
    {
        Level = data.LevelNum;
        Health = data.currentHealth;
        Position = data.playerPosition;
    }

    public void SaveData(GameData data)
    {
        return;
    }
    
    public void StartClicked(string sceneName)
    { 
        DataPersistenceManager.instance.NewGame();
        DataPersistenceManager.instance.SaveGame();
        SceneManager.LoadSceneAsync("Level1");
    }

     public void LoadClicked() 
    {
        // save the game anytime before loading a new scene
        Debug.Log(Level);
        DataPersistenceManager.instance.LoadGame();
        Debug.Log(Level);
        // load the next scene - which will in turn load the game because of 
        // OnSceneLoaded() in the DataPersistenceManager
        Debug.Log(Level);
        SceneManager.LoadSceneAsync(Level);
    }

    public void HomeClicked()
    {
        DataPersistenceManager.instance.SaveGame();
        SceneManager.LoadScene("MainMenu");
    }
}
