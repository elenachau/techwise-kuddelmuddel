using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField] public int progression; // 0-100%
    [SerializeField] public string playerName;
    [SerializeField] public int startingSeedCount;
    [SerializeField] public int numObstaclesToSpawn;
    [SerializeField] private TextUpdater weedText;
    [SerializeField] private TextUpdater seedText;
    public int xBounds;
    public int yBounds;
    public int seedCount;
    public int weedCount = 0;
    private GameObject tm;

    // User-defined settings (settings menu)
    [SerializeField] public float scrollSensitivity; // 0-1 scroll speed

    void Awake() {
        seedCount = startingSeedCount;
        numObstaclesToSpawn = progression / 2;
        SetBounds();
    }

    void Start() {
        tm = GameObject.Find("Touch Manager");
        GameObject.DontDestroyOnLoad(this.gameObject);
    }

    private void SetBounds() {
        xBounds = progression;
        yBounds = xBounds;
    }

    public void SetNextProgression() {
        progression *= 3;
        numObstaclesToSpawn = progression / 2;
        //seedCount += progression;
        SetBounds();
        tm.GetComponent<PinchZoom>().UpdateCamera();
    }
}