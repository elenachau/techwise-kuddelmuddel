using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WeedPlanter : MonoBehaviour
{
    [SerializeField] public GameObject weedPrefab;
    [SerializeField] private Tilemap canvas;
    
    private WeedLocationManager wlm;
    private TileGetter tg;
    private Transform parentTilemap;

    void Start() {
        tg = GameObject.Find("Touch Manager").GetComponent<TileGetter>();
        wlm = GameObject.Find("Weed Location Manager").GetComponent<WeedLocationManager>();
        parentTilemap = GameObject.Find("Above Ground").transform;
    }

    public void PlanterUpdate() {
        if (Input.touchCount > 0){
            tg.TouchUpdate(Input.GetTouch(0).position);
            bool firstTouch = (Input.GetTouch(0).phase == TouchPhase.Began);

            if (!(wlm.weedLocations.ContainsKey(tg.lastCell))){
                if (wlm.tileLocations.ContainsKey(tg.lastCell)){
                    if (tg.GetSurroundingObjectsOfTag(tg.lastCell, "Weed").Count > 0 || wlm.GetNumWeeds() == 0){ // Is adjacent to a weed and is not the first weed placed
                        if (PlayerData.Instance.seedCount > 0){
                            CreateWeed(tg.lastCell);
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
                else if (firstTouch){
                    print("There is no tile to place that weed!");
                }
            }
            else if (firstTouch){
                print("That tile is occupied! " + tg.lastCell);
            }
        }
    }

    public void CreateWeed(Vector3Int cell) {
        GameObject newWeed = Instantiate(weedPrefab, canvas.CellToWorld(cell), Quaternion.identity);
        newWeed.transform.parent = parentTilemap;
        newWeed.name = "Weed " + cell;
        newWeed.GetComponent<WeedData>().location = cell;

        wlm.weedLocations.Add(cell, newWeed);
        PlayerData.Instance.AddWeeds(1);
        newWeed.GetComponent<WeedData>().StartGrowth();
    }
}
