using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinchZoom : MonoBehaviour
{
    Camera cam;
    Vector2 startPos;
    float zoomScale = 0.1f;
    float startingDistance = 0;

    void Start(){
        cam = Camera.main;
    }

    void Update()
    {
        if (Input.touchCount == 2){
            Touch anchor = Input.GetTouch(0);
            Touch mover = Input.GetTouch(1);

            if (mover.phase == TouchPhase.Began){
                startingDistance = Vector2.Distance(anchor.position, mover.position);
            }

            float currentDistance = Vector2.Distance(anchor.position, mover.position);
            float distDiff = currentDistance - startingDistance;
            cam.orthographicSize = cam.orthographicSize + (zoomScale * distDiff);
            Debug.Log(distDiff);
        }
    }
}
