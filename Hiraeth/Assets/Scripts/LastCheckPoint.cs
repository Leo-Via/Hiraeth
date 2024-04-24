using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LastCheckPoint : MonoBehaviour, IDataPersistence
{
    private static LastCheckPoint instance;
    public Vector2 lastCheckPointPos;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else {
            Destroy(gameObject);
        }
        
    }

    public void LoadData(GameData data)
	{
		this.lastCheckPointPos = data.playerPosition;
	}

	public void SaveData(GameData data)
	{
		data.playerPosition = this.lastCheckPointPos;
        data.LevelNum = SceneManager.GetActiveScene().name;
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
