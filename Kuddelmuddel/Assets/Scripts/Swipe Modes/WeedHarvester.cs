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
        sellTracker = 0;
        tg = GameObject.Find("Touch Manager").GetComponent<TileGetter>();
        wlm = GameObject.Find("Weed Location Manager").GetComponent<WeedLocationManager>();
        pd = GameObject.Find("Player").GetComponent<PlayerData>();
        weedDestroyed.Invoke(); // Updates weed/seed text count (TextUpdater listeners)
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
                    DestroyObstacle(touchedObject);
                }
            }
        }
    }

    private void incSeedCount(int weedSellValue){
        sellTracker += 1;
        print(sellTracker + ", " + weedSellValue);
        if (sellTracker % weedSellValue == 0){
            sellTracker -= weedSellValue;
            pd.seedCount += 1;
        }

    }

    private void DestroyWeed(GameObject weed){
        incSeedCount(weed.GetComponent<WeedData>().weedSellValue);
        Destroy(weed);
        wlm.weedLocations.Remove(tg.lastCell);
        pd.weedCount--;

        print("Harvested weed at " + tg.lastCell);
        weedDestroyed.Invoke();
    }

    private void DestroyObstacle(GameObject obstacle){
        int cost = obstacle.GetComponent<ObstacleData>().cost;
        if (pd.seedCount >= cost){
            pd.seedCount -= cost;
            Destroy(obstacle);
            wlm.weedLocations.Remove(tg.lastCell);
            print("Destroyed obstacle at " + tg.lastCell);
            weedDestroyed.Invoke();
        }
        else{
            print("You don't have enough seeds to destroy that obstacle!");
        }

    }
}
