using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleData : MonoBehaviour
{
    [SerializeField] public int cost = 2;
    public Vector3Int location;

    public bool isRemovable() {
        bool removable = true;

        if (PlayerData.Instance.seedCount < cost){
            removable = false;
            print("You don't have enough seeds to destroy that obstacle!");
        }

        else if (TileGetter.Instance.GetSurroundingObjectsOfTag(location, "Weed").Count == 0){
            removable = false;
            print("You need to have a surrounding weed to remove that obstacle!");
        }

        return removable;
    }

    public void RemoveSelf() {
        PlayerData.Instance.AddSeeds(-cost);
        WeedLocationManager.Instance.weedLocations.Remove(TileGetter.Instance.lastCell);
        foreach (GameObject sWeed in
                TileGetter.Instance.GetSurroundingObjectsOfTag(TileGetter.Instance.lastCell, "Weed")){
            sWeed.GetComponent<WeedData>().StartSpread();
        }

        print("Destroyed obstacle at " + TileGetter.Instance.lastCell);
        Destroy(this.gameObject);
    }
}
