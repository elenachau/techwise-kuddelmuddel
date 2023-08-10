using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinchZoom : MonoBehaviour
{
    private float maxCamSize;
    private float minCamSize = 2;
    private PlayerData pd;
    private float previousDistance = 0;
    private Vector2 anchor;
    private bool anchored;

    void Start() {
        pd = GameObject.Find("Player").GetComponent<PlayerData>();
        maxCamSize = pd.xBounds;
    }

    public void ZoomUpdate()
    {
        if (Input.touchCount > 0){
            Touch touch = Input.GetTouch(0);

            if (anchored){
                if (touch.phase == TouchPhase.Moved){
                    float currentDistance = Vector2.Distance(anchor, touch.position);
                    float newSize = Camera.main.orthographicSize + (pd.scrollSensitivity * (previousDistance - currentDistance));
                    Camera.main.orthographicSize = CheckCamZoom(Mathf.Abs(newSize));
                    previousDistance = currentDistance;
                }

                if (touch.phase == TouchPhase.Ended){
                    anchored = false;
                }
            }

            else if (touch.phase == TouchPhase.Ended){
                anchor = touch.position;
                anchored = true;
                print("Anchored at " + anchor);
            }
        }
    }

    private float CheckCamZoom(float oldSize){
        float newSize = oldSize;
        newSize = newSize < minCamSize ? minCamSize : newSize;
        newSize = newSize > maxCamSize ? maxCamSize : newSize;
        return newSize;
    }
}
