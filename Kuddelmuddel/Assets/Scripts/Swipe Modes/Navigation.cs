using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Navigation : MonoBehaviour
{
    private PlayerData pd;
    private Camera cam;

    void Start() {
        pd = GameObject.Find("Player").GetComponent<PlayerData>();
        cam = Camera.main;
    }

    public void NavUpdate() {
        if (Input.touchCount > 0){
            Vector2 dPos = Input.GetTouch(0).deltaPosition;

            Vector3 newPos = new Vector3(
                (-pd.scrollSensitivity * dPos.x) + cam.transform.position.x,
                (-pd.scrollSensitivity * dPos.y) + cam.transform.position.y,
                cam.transform.position.z
            );
            cam.transform.position = CheckBounds(newPos);
        }
    }
    
    Vector3 CheckBounds(Vector3 oldPos){
        Vector3 pos = oldPos;
        pos.x = pos.x >  pd.xBounds ?  pd.xBounds : pos.x; // Snap to bound if greater, else no change
        pos.x = pos.x < -pd.xBounds ? -pd.xBounds : pos.x;
        pos.y = pos.y >  pd.yBounds ?  pd.yBounds : pos.y;
        pos.y = pos.y < -pd.yBounds ? -pd.yBounds : pos.y;
        return pos;
    }
}
