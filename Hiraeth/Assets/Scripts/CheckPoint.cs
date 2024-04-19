using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private LastCheckPoint gm;

    void Start() {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<LastCheckPoint>();

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            gm.lastCheckPointPos = transform.position;


        }
    }
}