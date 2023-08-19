using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;

public class WeedPlanter : MonoBehaviour
{
    [SerializeField] public GameObject weedPrefab;
    [SerializeField] private Tilemap canvas;
    [SerializeField] public TextMeshProUGUI weedCountText;
    [SerializeField] public TextMeshProUGUI seedCountText;
    
    private WeedLocationManager wlm;
    private TileGetter tg;
    private PlayerData pd;
    private MapManager mm;

    void Start() {
        tg = GameObject.Find("Touch Manager").GetComponent<TileGetter>();
        wlm = GameObject.Find("Weed Location Manager").GetComponent<WeedLocationManager>();
        pd = GameObject.Find("Player").GetComponent<PlayerData>();
        mm = GameObject.Find("Map Manager").GetComponent<MapManager>();
    }

    public void PlanterUpdate() {
        if (Input.touchCount > 0){
            tg.TouchUpdate(canvas, Input.GetTouch(0).position);
            bool firstTouch = (Input.GetTouch(0).phase == TouchPhase.Began);

            if (!(wlm.weedLocations.ContainsKey(tg.lastCell))){
                if (wlm.tileLocations.ContainsKey(tg.lastCell)){
                    if (tg.GetSurroundingObjectsOfTag(tg.lastCell, "Weed").Count > 0 || wlm.GetNumWeeds() == 0){ // Is adjacent to a weed and is not the first weed placed
                        if (pd.seedCount > 0){
                            GameObject newWeed = Instantiate(weedPrefab, canvas.CellToWorld(tg.lastCell), Quaternion.identity);
                            newWeed.transform.parent = UnityEngine.GameObject.Find("Above Ground").transform;
                            newWeed.name = "Weed" + tg.lastCell;
                            newWeed.GetComponent<WeedData>().location = tg.lastCell;

                            pd.seedCount -= 1;
                            seedCountText.text = "" + pd.seedCount;
                            pd.weedCount += 1;
                            weedCountText.text = "" + pd.weedCount;

                            wlm.weedLocations.Add(tg.lastCell, newWeed);
                            mm.CheckMap();
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
}
