using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Events;
using TMPro;

public class WeedPlanter : MonoBehaviour
{
    [SerializeField] public GameObject weedPrefab;
    [SerializeField] private Tilemap canvas;
    public UnityEvent weedCreated;
    
    private WeedLocationManager wlm;
    private TileGetter tg;
    private PlayerData pd;

    void Start() {
        tg = GameObject.Find("Touch Manager").GetComponent<TileGetter>();
        wlm = GameObject.Find("Weed Location Manager").GetComponent<WeedLocationManager>();
        pd = GameObject.Find("Player").GetComponent<PlayerData>();
    }

    public void PlanterUpdate() {
        if (Input.touchCount > 0){
            tg.TouchUpdate(Input.GetTouch(0).position);
            bool firstTouch = (Input.GetTouch(0).phase == TouchPhase.Began);

            if (!(wlm.weedLocations.ContainsKey(tg.lastCell))){
                if (wlm.tileLocations.ContainsKey(tg.lastCell)){
                    if (tg.GetSurroundingObjectsOfTag(tg.lastCell, "Weed").Count > 0 || wlm.GetNumWeeds() == 0){ // Is adjacent to a weed and is not the first weed placed
                        if (pd.seedCount > 0){
                            CreateWeed(tg.lastCell);
                            pd.seedCount--;
                            pd.weedCount++;
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
        newWeed.transform.parent = canvas.transform;
        newWeed.name = "Weed" + cell;
        newWeed.GetComponent<WeedData>().location = cell;

        wlm.weedLocations.Add(cell, newWeed);
        StartCoroutine(newWeed.GetComponent<WeedData>().GrowChild(canvas, tg));
        weedCreated.Invoke();
    }
}
