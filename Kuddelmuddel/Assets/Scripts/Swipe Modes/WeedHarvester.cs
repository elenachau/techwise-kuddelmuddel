using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using TMPro;

public class WeedHarvester : MonoBehaviour
{   
    [SerializeField] private float seedReturnChance;
    [SerializeField] private AudioClip harvestSound;
    [SerializeField] private GameObject obstacleRemoveButton;
    [SerializeField] private int buttonYOffset;
    private GameObject selectedObstacle;

    public void HarvesterUpdate() {
        if (Input.GetMouseButton(0)){
            TileGetter.Instance.TouchUpdate(Input.mousePosition);

            if (WeedLocationManager.Instance.weedLocations.ContainsKey(TileGetter.Instance.lastCell)){
                GameObject touchedObject = WeedLocationManager.Instance.weedLocations[TileGetter.Instance.lastCell];

                if (touchedObject.tag == "Weed"){
                    DestroyWeed(touchedObject);
                    PlayerData.Instance.AddSeeds(1);
                    AudioManager.Instance.PlayHarvestSFX();
                }
                else if (touchedObject.tag == "Obstacle" && Input.GetMouseButtonDown(0)){
                    if (touchedObject == selectedObstacle) {
                        obstacleRemoveButton.SetActive(!obstacleRemoveButton.activeInHierarchy); // toggle
                    }
                    else {
                        obstacleRemoveButton.SetActive(true);
                    }

                    if (obstacleRemoveButton.activeInHierarchy){
                        Vector2 buttonPos = new Vector2(TileGetter.Instance.lastWorldPt.x, TileGetter.Instance.lastWorldPt.y + buttonYOffset);
                        obstacleRemoveButton.transform.position = buttonPos;
                        GameObject.Find("Seed Cost").GetComponent<TextUpdater>().UpdateText(PlayerData.Instance.numObstaclesRemoved.ToString());
                        selectedObstacle = touchedObject;
                    }
                }

                else if (touchedObject.tag == "Seed") {
                    DestroySeed(touchedObject);
                    if(Random.Range(0,1) < seedReturnChance){
                        PlayerData.Instance.AddSeeds(1);
                    }
                    AudioManager.Instance.PlaySoundEffect(AudioManager.Instance.sfx_removedObstacle);
                }
            }
        }
    }

    public void DestroyWeed(GameObject weed){
        Destroy(weed);
        Vector3Int location = weed.GetComponent<WeedData>().location;
        WeedLocationManager.Instance.weedLocations.Remove(location);
        PlayerData.Instance.AddWeeds(-1);
        foreach (GameObject sWeed in
                TileGetter.Instance.GetSurroundingObjectsOfTag(location, "Weed")){
            sWeed.GetComponent<WeedData>().StartSpread();
        }

        // Check if player has no seeds or weeds; prohibit softlock
        if (WeedLocationManager.Instance.getWeedObjectCount() + PlayerData.Instance.seedCount < 1) {
            PlayerData.Instance.AddSeeds(1);
        }

        print("Destroyed weed at " + location);
    }

    private void DestroySeed(GameObject seed){
        Destroy(seed);
        Vector3Int location = seed.GetComponent<WeedData>().location;
        WeedLocationManager.Instance.weedLocations.Remove(location);
        foreach (GameObject sWeed in
                TileGetter.Instance.GetSurroundingObjectsOfTag(location, "Weed")){
            sWeed.GetComponent<WeedData>().StartSpread();
        }

        // Check if player has no seeds or weeds; prohibit softlock
        if (WeedLocationManager.Instance.getWeedObjectCount() + PlayerData.Instance.seedCount < 1) {
            PlayerData.Instance.AddSeeds(1);
        }

        print("Destroyed seed at " + location);
    }

    public void DestroySelectedObstacle() {
        if (selectedObstacle != null && selectedObstacle.GetComponent<ObstacleData>().isRemovable()){
            selectedObstacle.GetComponent<ObstacleData>().RemoveSelf();
            obstacleRemoveButton.SetActive(false);
            AudioManager.Instance.PlaySoundEffect(harvestSound);
        }
    }
}
