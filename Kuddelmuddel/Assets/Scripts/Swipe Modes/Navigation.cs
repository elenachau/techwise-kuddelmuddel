using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Navigation : MonoBehaviour
{
    private Camera cam;

    void Start() {
        cam = Camera.main;
    }

    public void NavUpdate() {
        if (Input.touchCount > 0){
            Vector2 dPos = Input.GetTouch(0).deltaPosition;

            Vector3 newPos = new Vector3(
                (-PlayerData.Instance.scrollSensitivity * dPos.x) + cam.transform.position.x,
                (-PlayerData.Instance.scrollSensitivity * dPos.y) + cam.transform.position.y,
                cam.transform.position.z
            );
            cam.transform.position = CheckBounds(newPos);
        }
    }
    
    Vector3 CheckBounds(Vector3 oldPos){
        Vector3 pos = oldPos;
        pos.x = pos.x >  PlayerData.Instance.xBounds ?  PlayerData.Instance.xBounds : pos.x; // Snap to bound if greater, else no change
        pos.x = pos.x < -PlayerData.Instance.xBounds ? -PlayerData.Instance.xBounds : pos.x;
        pos.y = pos.y >  PlayerData.Instance.yBounds ?  PlayerData.Instance.yBounds : pos.y;
        pos.y = pos.y < -PlayerData.Instance.yBounds ? -PlayerData.Instance.yBounds : pos.y;
        return pos;
    }
}
