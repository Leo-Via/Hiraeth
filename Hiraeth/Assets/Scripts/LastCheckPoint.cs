using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastCheckPoint : MonoBehaviour
{
    private static LastCheckPoint instance;
    public Vector2 lastCheckPointPos;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else {
            Destroy(gameObject);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
