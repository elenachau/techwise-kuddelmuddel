using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField] public int progression; // 0-100%
    [SerializeField] public string playerName;
    public int xBounds;
    public int yBounds;

    // User-defined settings (settings menu)
    [SerializeField] public float scrollSensitivity; // 0-1 scroll speed

    void Awake()
    {
        setBounds();
    }

    private void setBounds() {
        xBounds = 5 + progression / 2;
        yBounds = xBounds / 2;
    }
}