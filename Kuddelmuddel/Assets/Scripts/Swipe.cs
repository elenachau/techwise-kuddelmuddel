using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Swipe : MonoBehaviour
{
    private Touch touch;
    private Vector2 startPos, endPos;
    private string direction;
    [SerializeField]
    private GameObject trail;
    private Coroutine coroutine;

    void Start(){
        Debug.Log("Started");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0){
            
            touch = Input.GetTouch(0);
            Debug.Log(touch.position);

            if(touch.phase == TouchPhase.Began){
                startPos = touch.position;

                // Turn on and set initial trail position
                trail.SetActive(true);
                trail.transform.position = startPos;
                
                //coroutine = StartCoroutine(Trail(startPos));
            }

            else if(touch.phase == TouchPhase.Moved){
                //trail.transform.position = touch.position;
            }

            else if(touch.phase == TouchPhase.Ended){
                endPos = touch.position;

                // Turn off and reset trail position
                trail.SetActive(false);
                ZeroOutTrail();

                // Debug - log direction of swipe
                GetDirection();

                //StopCoroutine(coroutine);
                
            }
        }
    }

    void GetDirection(){
        float x = endPos.x - startPos.x;
        float y = endPos.y - startPos.y;

        if(Mathf.Abs(x) == 0 && Mathf.Abs(y) == 0){
            direction = "Tapped";
        }
        else if (Mathf.Abs(x) > Mathf.Abs(y)){
            direction = x > 0 ? "Right" : "Left";
        }
        else{
            direction = y > 0 ? "Up" : "Down";
        }
        Debug.Log(direction);
    }

    void ZeroOutTrail(){
        trail.transform.position = new Vector2(0f, 0f);
    }

    private IEnumerator Trail(Vector2 pos){
        while(true){
            //trail.transform.position = pos;
            yield return null;
        }
    }
}
