using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public long lastUpdated;

    public string LevelNum;
    public float currentHealth;
    public Vector2 playerPosition;

    // the values defined in this constructor will be the default values
    // the game starts with when there's no data to load
    public GameData() 
    {
        LevelNum = "Level1";
        this.currentHealth = 100;
        playerPosition = Vector2.zero;
    }
}