using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleData : MonoBehaviour
{
    private WeedLocationManager wlm;

    void Start() {
        wlm = GameObject.Find("Weed Location Manager").GetComponent<WeedLocationManager>();
    }

    [SerializeField] public int cost = 2;
    public Vector3Int location;

    private void OnDestroy() {
        wlm.weedLocations.Remove(location);
        print("Destroyed obstacle at " + location);
    }
}
