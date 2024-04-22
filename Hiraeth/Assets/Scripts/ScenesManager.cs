using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        print("Trigger!");


        if (other.tag =="Player" && SceneManager.GetActiveScene().name == "Level1")
        {
            SceneManager.LoadSceneAsync("Level2");
        }

        if (other.tag == "Player" && SceneManager.GetActiveScene().name == "Level2")
        {
            SceneManager.LoadSceneAsync("Level3");
        }
    }

}
