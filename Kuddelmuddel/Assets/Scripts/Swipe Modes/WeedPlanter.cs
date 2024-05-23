using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WeedPlanter : MonoBehaviour
{
    [SerializeField] public GameObject weedPrefab;

    public void PlanterUpdate() {
        if (Input.GetMouseButton(0)){
            TileGetter.Instance.TouchUpdate(Input.mousePosition);
            bool firstTouch = (Input.GetMouseButtonDown(0));

            if (!(WeedLocationManager.Instance.weedLocations.ContainsKey(TileGetter.Instance.lastCell))){ // object exists on this tile
                if (WeedLocationManager.Instance.tileLocations.ContainsKey(TileGetter.Instance.lastCell)){ // tile doesn't exist
                    if (TileGetter.Instance.GetSurroundingObjectsOfTag(TileGetter.Instance.lastCell, "Weed").Count 
                        + TileGetter.Instance.GetSurroundingObjectsOfTag(TileGetter.Instance.lastCell, "Seed").Count > 0
                        || PlayerData.Instance.weedCount == 0){ // Is adjacent to a weed/seed and is not the first weed placed
                        if (PlayerData.Instance.seedCount > 0){
                            CreateWeed(TileGetter.Instance.lastCell);
                            PlayerData.Instance.AddSeeds(-1);
                        }
                        else if (firstTouch){
                            print("You don't have enough seeds to plant a weed!");
                        }
                    }
                    else if (firstTouch){
                        print("That cell is not adjacent to a weed!");
                    }
                }
            }
        }
    }

    public void CreateWeed(Vector3Int cell) {
        GameObject newWeed = Instantiate(weedPrefab, TileGetter.Instance.terrain.CellToWorld(cell), Quaternion.identity);
        newWeed.transform.parent = TileGetter.Instance.aboveGround.transform;
        newWeed.name = "Weed " + cell;
        newWeed.GetComponent<WeedData>().location = cell;

        WeedLocationManager.Instance.weedLocations.Add(cell, newWeed);
        AudioManager.Instance.PlayPlantingSFX();
        newWeed.GetComponent<WeedData>().StartGrowth();
    }
}
