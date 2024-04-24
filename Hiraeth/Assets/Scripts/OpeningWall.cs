using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class OpeningWall : MonoBehaviour
{
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

    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Debug.Log("Number of enemies found: " + enemies.Length);

        if (enemies.Length == 0)
        {
            Debug.Log("No enemies found. Opening the wall...");
            ren.enabled = false;
            c.enabled = false;
            coll.enabled = false;
        }
    }
}