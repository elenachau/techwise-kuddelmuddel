using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleData : MonoBehaviour
{
    [SerializeField] public int cost = 2;
    public Vector3Int location;
    private WeedLocationManager wlm;
    private TileGetter tg;

    void Start() {
        wlm = GameObject.Find("Weed Location Manager").GetComponent<WeedLocationManager>();
        tg = GameObject.Find("Touch Manager").GetComponent<TileGetter>();
    }

    public bool isRemovable() {
        bool removable = true;

        if (PlayerData.Instance.seedCount < cost){
            removable = false;
            print("You don't have enough seeds to destroy that obstacle!");
        }

        else if (tg.GetSurroundingObjectsOfTag(location, "Weed").Count == 0){
            removable = false;
            print("You need to have a surrounding weed to remove that obstacle!");
        }

        return removable;
    }

    public void RemoveSelf() {
        if (isRemovable()){
            PlayerData.Instance.AddSeeds(-cost);
            wlm.weedLocations.Remove(tg.lastCell);
            print("Destroyed obstacle at " + tg.lastCell);
            Destroy(this.gameObject);
        }
    }
}
