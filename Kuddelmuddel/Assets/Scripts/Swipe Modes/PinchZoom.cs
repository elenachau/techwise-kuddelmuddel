using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinchZoom : MonoBehaviour
{
    private float maxCamSize;
    private float minCamSize = 2;
    private float previousDistance;
    private Vector2 anchor;
    private bool anchored;
    private Camera cam;

    void Start() {
        cam = Camera.main; // cache camera
        UpdateCamera();

        previousDistance = 0;
        anchor = new Vector2(0,0);
    }

    public void UpdateCamera() {
        maxCamSize = PlayerData.Instance.xBounds + 3;
        // TODO: Tween camera aniamtion at MapFilled event
    }

    public void ZoomUpdate() {
        if (Input.GetMouseButtonDown(0)){
            previousDistance = Vector2.Distance(anchor, Input.mousePosition);;
        }
        if (Input.GetMouseButton(0)){
            float currentDistance = Vector2.Distance(anchor, Input.mousePosition); // distance from origin
            float newSize = cam.orthographicSize + (PlayerData.Instance.scrollSensitivity * (previousDistance - currentDistance));
            cam.orthographicSize = CheckCamZoom(Mathf.Abs(newSize));
            previousDistance = currentDistance;
        }
    }

    public void ScrollWheelUpdate() {
        float newSize = cam.orthographicSize - 2*(Input.GetAxis("Mouse ScrollWheel"));
        cam.orthographicSize = CheckCamZoom(Mathf.Abs(newSize));
    }

    private float CheckCamZoom(float oldSize){
        float newSize = oldSize;
        newSize = newSize < minCamSize ? minCamSize : newSize;
        newSize = newSize > maxCamSize ? maxCamSize : newSize;
        return newSize;
    }
}
