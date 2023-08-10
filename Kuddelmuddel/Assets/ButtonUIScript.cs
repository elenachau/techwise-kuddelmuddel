using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ButtonUIScript : MonoBehaviour
{
    UIDocument buttonDoc;
    void onEnable() {
        buttonDoc = GetComponent<UIDocument>();
        Button uiButton = buttonDoc.rootVisualElement.Q("ParentUIElement").Q("LowerMenu").Q("button1") as Button;

        if (uiButton == null) {
            Debug.Log("Didn't find the button");
        }
        else {
            Debug.Log("Found the button");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
