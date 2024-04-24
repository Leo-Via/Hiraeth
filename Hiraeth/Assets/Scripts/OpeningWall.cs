using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class OpeningWall : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject play;
    GameObject[] enemies;

    GameObject w;
    TilemapRenderer ren;
    TilemapCollider2D c;
    CompositeCollider2D coll;

    int numEnemies;


    void Start()
    {
        play = GameObject.FindGameObjectWithTag("Player");
        w = GameObject.FindGameObjectWithTag("OpeningWall");
        ren = w.GetComponent<TilemapRenderer>();
        c = w.GetComponent<TilemapCollider2D>();
        coll = w.GetComponent<CompositeCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        numEnemies = 0;

        for(int i = 0; i < enemies.Length; i++){
            if(enemies[i].GetComponent<Enemy1>().getIsAlive() == true){
                numEnemies++;
            }
        }

        if(numEnemies  == 0){
            ren.enabled = false;
            c.enabled = false;
            coll.enabled = false;
        }
    }
}
