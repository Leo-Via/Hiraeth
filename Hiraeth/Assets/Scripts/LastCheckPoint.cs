using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LastCheckPoint : MonoBehaviour, IDataPersistence
{
    public static LastCheckPoint instance { get; private set; }
    public Vector2 lastCheckPointPos;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
        
    }

    public void LoadData(GameData data)
	{
		this.lastCheckPointPos = data.playerPosition;
        return;
	}

	public void SaveData(GameData data)
	{
		data.playerPosition = this.lastCheckPointPos;
        Debug.Log(data.playerPosition);

        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            Debug.Log("Level Name Stopped");
        }

        else
        {
            data.LevelNum = SceneManager.GetActiveScene().name;
            Debug.Log("Level Name Saved");
            Debug.Log(data.LevelNum);
        }
        return;
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
