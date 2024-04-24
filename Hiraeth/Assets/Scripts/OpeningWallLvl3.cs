using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class OpeningWallLvl3 : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject play;
    GameObject[] enemies;

    GameObject w;
    TilemapRenderer ren;
    TilemapCollider2D c;
    CompositeCollider2D coll;



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

        if(enemies.Length == 0){
            ren.enabled = false;
            c.enabled = false;
            coll.enabled = false;
        }
    }
}
