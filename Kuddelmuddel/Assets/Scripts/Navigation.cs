using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Navigation : MonoBehaviour
{
    Camera cam;
    float xBounds = 5f;
    float yBounds = 5f;
    float speedScale = 0.01f; // 0-1 scroll speed
    int invScroll = 1; // 1 = opposite movement from swipe, 0 = normal

    void Start(){
        cam = Camera.main;
    }

    void Update()
    {
        if (Input.touchCount > 0){ // Ignores multiple fingers tapped
            Vector2 dPos = Input.GetTouch(0).deltaPosition;
            Vector3 newPos = new Vector3(
                -invScroll * (speedScale * dPos.x) + cam.transform.position.x,
                -invScroll * (speedScale * dPos.y) + cam.transform.position.y,
                cam.transform.position.z
            );
            CheckBounds(newPos);
            cam.transform.position = newPos;
        }
    }

    void CheckBounds(Vector3 pos){
        pos.x = pos.x >  xBounds ?  xBounds : pos.x; // Snap to bound if greater, else no change
        pos.x = pos.x < -xBounds ? -xBounds : pos.x;
        pos.y = pos.y >  yBounds ?  yBounds : pos.y;
        pos.y = pos.y < -yBounds ? -yBounds : pos.y;
    }
}
