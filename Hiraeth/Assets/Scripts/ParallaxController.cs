using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    public HorizontalDirection horizontalDirection;
    public FloatReference speedMultiplier;


    private List<ParallaxImage> images;

    private void Start()
    {
        InitController();
    }

    private void FixedUpdate()
    {
        if(images != null)
        {
            if (horizontalDirection != HorizontalDirection.Fix)
            {
                foreach (var item in images)
                {
                    item.MoveX(Time.deltaTime);
                }
            }
        }
    }

    private void InitController()
    {
        //List initialzed
        InitList();
        //scan for Images
        ScanForImages();
        //Init parallaxImage

        foreach(var item in images)
        {
            item.InitImage(speedMultiplier, horizontalDirection);
        }
    }

    private void InitList()
    {
        if (images == null) images = new List<ParallaxImage>();
        else
        {
            foreach (var item in images)
            {
                item.CleanUpImage();
            }
            images.Clear();
        }
    }

    private  void ScanForImages()
    {
        ParallaxImage pi;
        foreach(Transform child in transform)
        {
            if(child.gameObject.activeSelf)
            {
                pi = child.GetComponent<ParallaxImage>();
                if (pi != null) images.Add(pi);
            }
        }
    }
}

[System.Serializable]

public class FloatReference
{
    [Range(0.01f,5)]
    public float value = 1;
}

public enum HorizontalDirection
{
    Fix,
    Left,
    Right,
}