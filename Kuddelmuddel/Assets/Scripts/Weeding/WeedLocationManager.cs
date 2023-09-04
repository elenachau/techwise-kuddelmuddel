using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WeedLocationManager : MonoBehaviour
{
    public static WeedLocationManager Instance;
    public Dictionary<Vector3Int, GameObject> weedLocations = new Dictionary<Vector3Int, GameObject>(); // cell, weed reference
    public Dictionary<Vector3Int, GameObject> tileLocations = new Dictionary<Vector3Int, GameObject>();
    
    public int getWeedObjectCount() {
        int count = 0;
        foreach (KeyValuePair<Vector3Int, GameObject> entry in WeedLocationManager.Instance.weedLocations) {
            if (entry.Value.tag == "Weed" || entry.Value.tag == "Seed") {
                count++;
            }
        }
        return count;
    }

    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }
}
