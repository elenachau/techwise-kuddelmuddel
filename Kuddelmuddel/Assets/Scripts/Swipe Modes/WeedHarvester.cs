using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Events;
using TMPro;

public class WeedHarvester : MonoBehaviour
{   
    private WeedLocationManager wlm;
    private TileGetter tg;
    private PlayerData pd;
    private int sellTracker;
    public UnityEvent weedDestroyed;

    void Start() {
        tg = GameObject.Find("Touch Manager").GetComponent<TileGetter>();
        wlm = GameObject.Find("Weed Location Manager").GetComponent<WeedLocationManager>();
        pd = GameObject.Find("Player").GetComponent<PlayerData>();
    }

    public void HarvesterUpdate() {
        if (Input.touchCount > 0){
            tg.TouchUpdate(Input.GetTouch(0).position);

            if (wlm.weedLocations.ContainsKey(tg.lastCell)){
                GameObject touchedObject = wlm.weedLocations[tg.lastCell];

                if (touchedObject.tag == "Weed"){
                    DestroyWeed(touchedObject);
                }
                else if (touchedObject.tag == "Obstacle"){
                    int cost = touchedObject.GetComponent<ObstacleData>().cost;
                    if (pd.seedCount >= cost){
                        pd.seedCount -= cost;
                        Destroy(touchedObject);
                        wlm.weedLocations.Remove(tg.lastCell);
                        print("Destroyed obstacle at " + tg.lastCell);
                    }
                    else{
                        print("You don't have enough seeds to destroy that obstacle!");}
                }
            }
        }
    }

    private void incSeedCount(){
        sellTracker += 1;
        if (sellTracker % pd.weedSellValue == 0){
            sellTracker = 0;
            pd.seedCount += 1;
        }

    }

    private void DestroyWeed(GameObject weed){
        Destroy(weed);
        wlm.weedLocations.Remove(tg.lastCell);
        incSeedCount();
        pd.weedCount--;

        print("Harvested weed at " + tg.lastCell);
        weedDestroyed.Invoke();
    }
}