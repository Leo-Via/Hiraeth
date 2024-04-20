using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint1 : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject player;
    GameObject[] enemies;

    GameObject checkPoint;



    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        checkPoint = GameObject.FindGameObjectWithTag("Checkpoint");

    }

    // Update is called once per frame
    void Update()
    {
        if(enemies.Length == 0){
            checkPoint.SetActive(false);
        }
    }
}
