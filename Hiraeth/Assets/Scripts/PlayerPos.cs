using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPos : MonoBehaviour, IDataPersistence
{
    private LastCheckPoint gm;
    private PlayerHealth hp;
    bool isTouching = false;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<LastCheckPoint>();
        hp = GetComponent<PlayerHealth>();
        transform.position = gm.lastCheckPointPos;
    }
 
	public void LoadData(GameData data)
	{
		this.gm.lastCheckPointPos = data.playerPosition;
	}

	public void SaveData(GameData data)
	{
		data.playerPosition = this.gm.lastCheckPointPos;
	}


    // Update is called once per frame
    void Update()
    {
        if (hp.currentHealth <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
