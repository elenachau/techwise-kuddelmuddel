using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WeedHarvester : MonoBehaviour
{   
    private WeedLocationManager wlm;
    private TileGetter tg;
    private PlayerData pd;
    private int sellTracker;

    void Start() {
        sellTracker = 0;
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
                else if (touchedObject.tag == "Obstacle" && Input.GetTouch(0).phase == TouchPhase.Began){
                    touchedObject.GetComponent<ObstacleData>().RemoveSelf();
                }
            }
        }
    }

    private void incSeedCount(int weedSellValue){
        sellTracker += 1;
        print(sellTracker + ", " + weedSellValue);
        if (sellTracker % weedSellValue == 0){
            sellTracker -= weedSellValue;
            pd.AddSeeds(1);
        }

    }

    private void DestroyWeed(GameObject weed){
        incSeedCount(weed.GetComponent<WeedData>().weedSellValue);
        Destroy(weed);
        wlm.weedLocations.Remove(tg.lastCell);
        pd.AddWeeds(-1);

        print("Harvested weed at " + tg.lastCell);
    }
}
