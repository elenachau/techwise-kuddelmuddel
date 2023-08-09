using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WeedMaker : MonoBehaviour
{
    [SerializeField] public GameObject weedPrefab;
    [SerializeField] private Tilemap canvas;
    
    private WeedLocationManager wlm;
    private TileGetter tg;

    void Start() {
        tg = GameObject.Find("Touch Manager").GetComponent<TileGetter>();
        wlm = GameObject.Find("Weed Location Manager").GetComponent<WeedLocationManager>();
    }

    public void MakerUpdate() {
        if (Input.touchCount > 0){
            tg.TouchUpdate(canvas, Input.GetTouch(0).position);

            if (!(wlm.weedLocations.ContainsKey(tg.lastCell))){
                if (wlm.tileLocations.ContainsKey(tg.lastCell)){
                    GameObject newWeed = Instantiate(weedPrefab, canvas.CellToWorld(tg.lastCell), Quaternion.identity);
                    newWeed.transform.parent = UnityEngine.GameObject.Find("Above Ground").transform;
                    newWeed.name = "Weed" + tg.lastCell;
                    newWeed.GetComponent<WeedData>().testNum = wlm.weedLocations.Count;

                    print("Added weed at " + tg.lastCell);
                    wlm.weedLocations.Add(tg.lastCell, newWeed);
                }
                else{
                    print("There is no tile to place that weed!");
                }
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Began){
                print("A weed already exists at " + tg.lastCell);
            }
        }
    }
}
