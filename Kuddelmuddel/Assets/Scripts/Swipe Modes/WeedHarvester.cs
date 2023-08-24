using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WeedHarvester : MonoBehaviour
{   
    private TileGetter tg;
    private int sellTracker;
    [SerializeField] private AudioClip harvestSound;

    void Start() {
        sellTracker = 0;
        tg = GameObject.Find("Touch Manager").GetComponent<TileGetter>();
    }

    public void HarvesterUpdate() {
        if (Input.touchCount > 0){
            tg.TouchUpdate(Input.GetTouch(0).position);

            if (WeedLocationManager.Instance.weedLocations.ContainsKey(tg.lastCell)){
                GameObject touchedObject = WeedLocationManager.Instance.weedLocations[tg.lastCell];

                if (touchedObject.tag == "Weed"){
                    DestroyWeed(touchedObject);
                    AudioManager.Instance.PlaySoundEffect(harvestSound);
                }
                else if (touchedObject.tag == "Obstacle" && Input.GetTouch(0).phase == TouchPhase.Began){
                    touchedObject.GetComponent<ObstacleData>().RemoveSelf();
                    AudioManager.Instance.PlaySoundEffect(harvestSound);
                }
            }
        }
    }

    private void incSeedCount(int weedSellValue){
        sellTracker += 1;
        print(sellTracker + ", " + weedSellValue);
        if (sellTracker % weedSellValue == 0){
            sellTracker -= weedSellValue;
            PlayerData.Instance.AddSeeds(1);
        }

    }

    private void DestroyWeed(GameObject weed){
        incSeedCount(weed.GetComponent<WeedData>().weedSellValue);
        Destroy(weed);
        WeedLocationManager.Instance.weedLocations.Remove(tg.lastCell);
        PlayerData.Instance.AddWeeds(-1);

        print("Harvested weed at " + tg.lastCell);
    }
}
