using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField] public int progression; // 0-100%
    [SerializeField] public string playerName;
    [SerializeField] public int weedSellValue; // every x sold gets 1 seed back
    [SerializeField] public int startingSeedCount;
    public int xBounds;
    public int yBounds;
    public int seedCount;
    private GameObject tm;

    // User-defined settings (settings menu)
    [SerializeField] public float scrollSensitivity; // 0-1 scroll speed

    void Awake() {
        SetProgression();
        DontDestroyOnLoad(GameObject.Find("Player"));
    }

    void Start() {
        tm = GameObject.Find("Touch Manager");
    }

    private void SetBounds() {
        xBounds = progression;
        yBounds = xBounds;
    }

    public void SetProgression() {
        seedCount = startingSeedCount;
        SetBounds();
        //tm.GetComponent<PinchZoom>().UpdateCamera();
    }
}