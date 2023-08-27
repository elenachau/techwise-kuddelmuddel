using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance;

    [SerializeField] public int progression; // 0-100%
    [SerializeField] public string playerName;
    [SerializeField] public int startingSeedCount;
    [SerializeField] public int numObstaclesToSpawn;
    [SerializeField] public int minXBounds;
    [SerializeField] public int minYBounds;
    [SerializeField] private TextUpdater weedText;
    [SerializeField] private TextUpdater seedText;
    public int xBounds;
    public int yBounds;
    public int seedCount;
    public int weedCount;
    private GameObject tm;

    // User-defined settings (settings menu)
    [SerializeField] public float scrollSensitivity; // 0-1 scroll speed

    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
        seedCount = startingSeedCount;
        xBounds = minXBounds;
        yBounds = minYBounds;
    }

    void Start() {
        tm = GameObject.Find("Touch Manager");
        GameObject.DontDestroyOnLoad(this.gameObject);
        weedCount = 0;
    }

    private void SetBounds() {
        xBounds = progression;
        yBounds = xBounds;
    }

    public void SetNextProgression() {
        progression *= 3;
        numObstaclesToSpawn = 0;
        //numObstaclesToSpawn = progression / 2;
        AddSeeds(progression);
        SetBounds();
        tm.GetComponent<PinchZoom>().UpdateCamera();
    }

    public void AddWeeds(int amount) {
        weedCount += amount;
        if (weedCount < 0) {
            weedCount = 0;
        }
        weedText.UpdateWeedCount();
    }

    public void AddSeeds(int amount) {
        seedCount += amount;
        if (seedCount < 0) {
            seedCount = 0;
        }
        seedText.UpdateSeedCount();

    }
}