using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WeedHarvester : MonoBehaviour
{   
    [SerializeField] private float seedReturnChance;
    [SerializeField] private AudioClip harvestSound;

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
                    if (touchedObject.GetComponent<ObstacleData>().isRemovable()){
                        touchedObject.GetComponent<ObstacleData>().RemoveSelf();
                        AudioManager.Instance.PlaySoundEffect(harvestSound);
                    }
                }

                else if (touchedObject.tag == "Seed") {
                    DestroySeed(touchedObject);
                }
            }
        }
    }

    private void DestroyWeed(GameObject weed){
        Destroy(weed);
        WeedLocationManager.Instance.weedLocations.Remove(TileGetter.Instance.lastCell);
        PlayerData.Instance.AddWeeds(-1);
        if(Random.Range(0,1) < seedReturnChance){
            PlayerData.Instance.AddSeeds(1);
        }
        foreach (GameObject sWeed in
                TileGetter.Instance.GetSurroundingObjectsOfTag(TileGetter.Instance.lastCell, "Weed")){
            sWeed.GetComponent<WeedData>().StartSpread();
        }
        print("Harvested weed at " + TileGetter.Instance.lastCell);
    }

    private void DestroySeed(GameObject seed){
        Destroy(seed);
        WeedLocationManager.Instance.weedLocations.Remove(TileGetter.Instance.lastCell);
        PlayerData.Instance.AddSeeds(1);
        foreach (GameObject sWeed in
                TileGetter.Instance.GetSurroundingObjectsOfTag(TileGetter.Instance.lastCell, "Weed")){
            sWeed.GetComponent<WeedData>().StartSpread();
        }
        print("Harvested seed at " + TileGetter.Instance.lastCell);
    }
}
