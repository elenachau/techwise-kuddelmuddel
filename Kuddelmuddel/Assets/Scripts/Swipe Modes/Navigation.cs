using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Navigation : MonoBehaviour
{
    private Camera cam;
    private Vector3 oldPos;

    void Start() {
        cam = Camera.main;
    }

    public void NavUpdate() {
        if (Input.GetMouseButtonDown(0)){
            oldPos = Input.mousePosition;
            print("New Swipe");
        }

        if (Input.GetMouseButton(0)){
            Vector3 currentPos = Input.mousePosition;
            Vector3 dPos = currentPos - oldPos;
            print("current: " + currentPos + " / delta: "  + dPos + " / old: " + oldPos);

            Vector3 newPos = new Vector3(
                (-PlayerData.Instance.scrollSensitivity * dPos.x) + cam.transform.position.x,
                (-PlayerData.Instance.scrollSensitivity * dPos.y) + cam.transform.position.y,
                cam.transform.position.z
            );
            cam.transform.position = CheckBounds(newPos);
            oldPos = currentPos;
        }
    }
    
    Vector3 CheckBounds(Vector3 checkPos){
        Vector3 pos = checkPos;
        pos.x = pos.x >  PlayerData.Instance.xBounds ?  PlayerData.Instance.xBounds : pos.x; // Snap to bound if greater, else no change
        pos.x = pos.x < -PlayerData.Instance.xBounds ? -PlayerData.Instance.xBounds : pos.x;
        pos.y = pos.y >  PlayerData.Instance.yBounds ?  PlayerData.Instance.yBounds : pos.y;
        pos.y = pos.y < -PlayerData.Instance.yBounds ? -PlayerData.Instance.yBounds : pos.y;
        return pos;
    }
}
