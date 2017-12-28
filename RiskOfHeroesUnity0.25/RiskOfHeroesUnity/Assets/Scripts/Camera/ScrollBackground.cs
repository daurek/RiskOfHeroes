using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackground : MonoBehaviour {

    [SerializeField] private bool scrolling;
    [SerializeField] private bool paralax;
    [SerializeField] private float paralaxSpeed;

    private float backgroundSize;
    private Transform[] backgroundSprites;
    private int leftIndex;
    private int rightIndex;
    private float viewZone = 10;
    private Transform camTransform;
    private float lastCameraX;

    
    private void Start()
    {
        backgroundSize = transform.GetChild(1).position.x - transform.GetChild(0).position.x;
        camTransform = Camera.main.transform;
        lastCameraX = Camera.main.transform.position.x;
        backgroundSprites = new Transform[transform.childCount];
        for (int i = 0; i < backgroundSprites.Length; i++)
        {
            backgroundSprites[i] = transform.GetChild(i);
        }

        leftIndex = 0;
        rightIndex = backgroundSprites.Length - 1;
    }

    private void Update()
    {
        if (paralax)
        {
            float deltaX = camTransform.position.x - lastCameraX;
            transform.position += Vector3.right * (deltaX * paralaxSpeed);
            lastCameraX = camTransform.position.x;
        }

        if (scrolling)
        {
            if (camTransform.position.x < backgroundSprites[leftIndex].position.x + viewZone)
            {
                ScrollLeft();
            }

            if (camTransform.position.x > backgroundSprites[rightIndex].position.x - viewZone)
            {
                ScrollRight();
            }
        }
       
    }

    private void ScrollLeft()
    {
        int lastRight = rightIndex;
        backgroundSprites[rightIndex].position = new Vector3(backgroundSprites[leftIndex].position.x - backgroundSize, backgroundSprites[leftIndex].position.y, 0);
        leftIndex = rightIndex;
        rightIndex--;
        if (rightIndex < 0) rightIndex = backgroundSprites.Length - 1;
    }

    private void ScrollRight()
    {
        int lastLeft = leftIndex;
        backgroundSprites[leftIndex].position = new Vector3(backgroundSprites[rightIndex].position.x + backgroundSize, backgroundSprites[rightIndex].position.y, 0);
        rightIndex = leftIndex;
        leftIndex++;
        if (leftIndex == backgroundSprites.Length) leftIndex = 0;
    }
}
