using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trail : MonoBehaviour
{
    [SerializeField]
    private GameObject trail;

    void Update()
    {
        if (Input.touchCount > 0){ // Ignores multiple fingers tapped
            Touch touch = Input.GetTouch(0);
            trail.SetActive(true);
            trail.transform.position = ConvScreenCoordsToWorld(touch.position);
        }
    }

    Vector3 ConvScreenCoordsToWorld(Vector3 position){
        Camera cam = Camera.main;
        position.z = cam.nearClipPlane; // casts with suitable Z-axis
        return cam.ScreenToWorldPoint(position);
    }
}
