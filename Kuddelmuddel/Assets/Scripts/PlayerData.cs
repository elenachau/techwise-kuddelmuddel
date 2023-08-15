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

    // User-defined settings (settings menu)
    [SerializeField] public float scrollSensitivity; // 0-1 scroll speed

    void Awake()
    {
        setBounds();
        seedCount = startingSeedCount;
    }

    private void setBounds() {
        xBounds = 5 + progression / 2;
        yBounds = xBounds / 2;
    }
}