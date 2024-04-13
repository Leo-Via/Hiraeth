using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    GameObject player;
    public Transform dest;
    public bool isStart;

    public float time;

    public bool isTeleporting;
    public float minDist = 0.2f;
    public float distance;

    public BoxCollider2D boxCollider;
    SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        sr = GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>();
        boxCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(transform.position.x < 0){
            isStart = true;
        }
        else{
            isStart = false;
        }

        if(!isStart){
            dest = GameObject.FindGameObjectWithTag("TeleporterStart").GetComponent<Transform>();
        }
        else{
            dest = GameObject.FindGameObjectWithTag("TeleporterEnd").GetComponent<Transform>();
        }

        if(Vector2.Distance(player.transform.position, GameObject.FindGameObjectWithTag("TeleporterEnd").transform.position) == 0.0  || Vector2.Distance(player.transform.position, GameObject.FindGameObjectWithTag("TeleporterStart").transform.position) == 0.0){
            isTeleporting = true;
        }
        else{
            isTeleporting = false;
        }

        if(other.CompareTag("Player") && !isTeleporting){

            //At the START teleporter
            if(Vector2.Distance(dest.transform.position, GameObject.FindGameObjectWithTag("TeleporterEnd").transform.position) == 0.0){
                if(Vector2.Distance(player.transform.position, transform.position) > minDist){
                    player.SetActive(false);
                    player.transform.position = new Vector3(dest.transform.position.x - 3, dest.transform.position.y, dest.transform.position.z);
                    player.SetActive(true);
                }
            }
            //At the END teleporter
            else{
                if(Vector2.Distance(player.transform.position, transform.position) > minDist){
                    player.SetActive(false);
                    player.transform.position = new Vector3(dest.transform.position.x + 3, dest.transform.position.y, dest.transform.position.z);
                    player.SetActive(true);
                }
            }
        }
    }
}
