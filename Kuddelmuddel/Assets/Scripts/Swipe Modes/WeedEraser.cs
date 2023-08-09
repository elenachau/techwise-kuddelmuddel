using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WeedEraser : MonoBehaviour
{   
    [SerializeField] private Tilemap canvas;
    private WeedLocationManager wlm;
    private TileGetter tg;

    void Start() {
        tg = GameObject.Find("Touch Manager").GetComponent<TileGetter>();
        wlm = GameObject.Find("Weed Location Manager").GetComponent<WeedLocationManager>();
    }

    public void EraseUpdate() {
        if (Input.touchCount > 0){
            tg.TouchUpdate(canvas, Input.GetTouch(0).position);

            if (wlm.weedLocations.ContainsKey(tg.lastCell)){
                Destroy(wlm.weedLocations[tg.lastCell]);
                print("Destroyed weed at " + tg.lastCell);
                wlm.weedLocations.Remove(tg.lastCell);
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Began){
                print("A weed does not exist to destroy at " + tg.lastCell);
            }
        }
    }
}
