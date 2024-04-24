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

        if (w == null)
        {
            Debug.LogError("Opening Wall game object not found!");
            return;
        }

        ren = w.GetComponent<TilemapRenderer>();
        c = w.GetComponent<TilemapCollider2D>();
        coll = w.GetComponent<CompositeCollider2D>();

        if (ren == null)
        {
            Debug.LogError("TilemapRenderer component not found on the Opening Wall game object!");
        }

        if (c == null)
        {
            Debug.LogError("TilemapCollider2D component not found on the Opening Wall game object!");
        }

        if (coll == null)
        {
            Debug.LogError("CompositeCollider2D component not found on the Opening Wall game object!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Debug.Log("Number of enemies found: " + enemies.Length);

        if (enemies.Length == 0)
        {
            Debug.Log("No enemies found. Opening the wall...");

            if (ren != null)
            {
                ren.enabled = false;
            }
            else
            {
                Debug.LogError("TilemapRenderer component is null!");
            }

            if (c != null)
            {
                c.enabled = false;
            }
            else
            {
                Debug.LogError("TilemapCollider2D component is null!");
            }

            if (coll != null)
            {
                coll.enabled = false;
            }
            else
            {
                Debug.LogError("CompositeCollider2D component is null!");
            }
        }

    }
}
