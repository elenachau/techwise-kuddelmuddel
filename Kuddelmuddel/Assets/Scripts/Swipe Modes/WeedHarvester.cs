using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WeedHarvester : MonoBehaviour
{   
    private int sellTracker;
    [SerializeField] private AudioClip harvestSound;

    void Start() {
        sellTracker = 0;
    }

    public void HarvesterUpdate() {
        if (Input.touchCount > 0){
            TileGetter.Instance.TouchUpdate(Input.GetTouch(0).position);

            if (WeedLocationManager.Instance.weedLocations.ContainsKey(TileGetter.Instance.lastCell)){
                GameObject touchedObject = WeedLocationManager.Instance.weedLocations[TileGetter.Instance.lastCell];

                if (touchedObject.tag == "Weed"){
                    DestroyWeed(touchedObject);
                    AudioManager.Instance.PlaySoundEffect(harvestSound);
                }
                else if (touchedObject.tag == "Obstacle" && Input.GetTouch(0).phase == TouchPhase.Began){
                    touchedObject.GetComponent<ObstacleData>().RemoveSelf();
                    AudioManager.Instance.PlaySoundEffect(harvestSound);
                }

                else if (touchedObject.tag == "Seed") {
                    // TODO: Make seed tag
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
        WeedLocationManager.Instance.weedLocations.Remove(TileGetter.Instance.lastCell);
        PlayerData.Instance.AddWeeds(-1);

        print("Harvested weed at " + TileGetter.Instance.lastCell);
    }
}
