using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberIncrementer : MonoBehaviour
{

    public Text textComponent;
    private int currentValue = 0;
    //public int currentValue = 0;

    private float timer = 0f;
    private float interval = 30f;


    //public int CurrentValue => currentValue; //added


    private void Start()
    {
        if (textComponent == null)
        {
            Debug.LogError("Text component is not assigned. Please assign it in the Inspector.");
            return;
        }

        // Set the initial value of the text
        UpdateText();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        //print(timer += Time.deltaTime);

        if (timer >= interval)
        {
            currentValue += 30;
            UpdateText();

            timer = 0f;
        }
    }

    private void UpdateText()
    {
        // Update the Text component with the new value
        textComponent.text = "Population: " + currentValue;
    }
}
