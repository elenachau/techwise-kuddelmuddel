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
                        obstacleRemoveButton.SetActive(true);
                        if (touchedObject == selectedObstacle) {
                            obstacleRemoveButton.SetActive(false);
                        }
                        Vector2 buttonPos = new Vector2(TileGetter.Instance.lastWorldPt.x, TileGetter.Instance.lastWorldPt.y + buttonYOffset);
                        obstacleRemoveButton.transform.position = buttonPos;
                        GameObject.Find("Seed Cost").GetComponent<TextUpdater>().UpdateText(PlayerData.Instance.numObstaclesRemoved.ToString());
                        selectedObstacle = touchedObject;
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

    public void DestroySelectedObstacle() {
        if (selectedObstacle != null){
            selectedObstacle.GetComponent<ObstacleData>().RemoveSelf();
            obstacleRemoveButton.SetActive(false);
            AudioManager.Instance.PlaySoundEffect(harvestSound);
        }
    }
}
