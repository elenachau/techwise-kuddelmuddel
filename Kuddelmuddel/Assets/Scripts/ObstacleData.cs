using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleData : MonoBehaviour
{
    [SerializeField] public int cost;
    public Vector3Int location;

    public bool isRemovable() {
        bool removable = true;

        if (PlayerData.Instance.seedCount < PlayerData.Instance.numObstaclesRemoved){
            removable = false;
            print("You don't have enough seeds to destroy that obstacle!");
        }

        else if (TileGetter.Instance.GetSurroundingObjectsOfTag(location, "Weed").Count == 0){
            removable = false;
            //print("You need to have a surrounding weed to remove that obstacle!");
        }

        return removable;
    }

    public void RemoveSelf() {
        PlayerData.Instance.AddSeeds(-PlayerData.Instance.numObstaclesRemoved);
        PlayerData.Instance.numObstaclesRemoved++;
        WeedLocationManager.Instance.weedLocations.Remove(location);
        foreach (GameObject sWeed in
                TileGetter.Instance.GetSurroundingObjectsOfTag(location, "Weed")){
            sWeed.GetComponent<WeedData>().StartSpread();
        }

        print("Destroyed obstacle at " + location);
        Destroy(this.gameObject);
    }
}
