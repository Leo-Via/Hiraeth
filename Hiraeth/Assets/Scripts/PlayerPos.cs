using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPos : MonoBehaviour
{
    private LastCheckPoint gm;
    private PlayerHealth hp;
    bool isTouching = false;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<LastCheckPoint>();
        hp = GetComponent<PlayerHealth>(); // Use GetComponent<PlayerHealth>() to get the PlayerHealth component attached to this GameObject
        transform.position = gm.lastCheckPointPos;
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
