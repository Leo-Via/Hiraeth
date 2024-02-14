using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxImage : MonoBehaviour
{
    public float speedX = 0;
    public int spawnCount = 2;

    private Transfrom[] controlledTransforms;
    private float imageWidth;


    public void CleanUpImage() { 
        if(controlledTransforms != null) {
            for (int i = 0; i < controlledTransforms.Length; i++) {
                Destroy(controlledTransforms[i].gameObject);
            }
        }
    }

    public void InitImage() {
        controlledTransforms = new Transform[spawnCount + 1];
        controlledTransforms[0] = transform;

        imageWidth = GetComponent<SpriteRenderer>().bounds.size.x;

        for(int i = 1; i < controlledTransforms.Length; i++){
            controlledTransforms[i] = PrepareCopyAt(controlledTransforms.position.x + imageWidth * i);
        }
    }

    private Transform PrepareCopyAt(float posX) {
        GameObject go = Instantiate(gameObject, new Vector3(posX, transform.position.y, transform.position.z), Quaternion.identity, transform.parent);
        Destroy(go.GetComponent<ParallaxImage>());

        return go.transform;

    }
}
