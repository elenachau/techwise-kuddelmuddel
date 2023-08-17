using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WeedHarvester : MonoBehaviour
{   
    [SerializeField] private Tilemap canvas;
    private WeedLocationManager wlm;
    private TileGetter tg;
    private PlayerData pd;
    private int sellTracker;

    void Start() {
        tg = GameObject.Find("Touch Manager").GetComponent<TileGetter>();
        wlm = GameObject.Find("Weed Location Manager").GetComponent<WeedLocationManager>();
        pd = GameObject.Find("Player").GetComponent<PlayerData>();
    }

    public void HarvesterUpdate() {
        if (Input.touchCount > 0){
            tg.TouchUpdate(canvas, Input.GetTouch(0).position);

            if (wlm.weedLocations.ContainsKey(tg.lastCell)){
                GameObject touchedObject = wlm.weedLocations[tg.lastCell];
                if (touchedObject.tag == "Weed"){
                    Destroy(touchedObject);
                    incWeedCount();
                }
                else if (touchedObject.tag == "Obstacle"){
                    int cost = touchedObject.GetComponent<ObstacleData>().cost;
                    if (pd.seedCount >= cost){
                        pd.seedCount -= cost;
                        Destroy(touchedObject);
                    }
                    else{
                        print("You don't have enough seeds to destroy that obstacle!");}
                }
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Began){
                print("A weed does not exist to destroy at " + tg.lastCell);
            }
        }
    }

    private void incWeedCount(){
        sellTracker += 1;
        if (sellTracker % pd.weedSellValue == 0){
            sellTracker = 0;
            pd.seedCount += 1;
        }

    }
}
