using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileGetter : MonoBehaviour
{   
    public TileBase lastTile;
    public Vector3Int lastCell;
    public Vector2 lastWorldPt;
    public GameObject lastWeed;

    private WeedLocationManager wlm;

    void Start() {
        wlm = GameObject.Find("Weed Location Manager").GetComponent<WeedLocationManager>();
    }

    public void TouchUpdate(Tilemap tilemap, Vector3 screenPos){ // Call this within touch if statement
        lastWorldPt = Camera.main.ScreenToWorldPoint(screenPos);
        lastCell = tilemap.WorldToCell(lastWorldPt);
        lastTile = tilemap.GetTile(lastCell);

        if(wlm.weedLocations.ContainsKey(lastCell)){
            lastWeed = wlm.weedLocations[lastCell];
        }
    }
}
