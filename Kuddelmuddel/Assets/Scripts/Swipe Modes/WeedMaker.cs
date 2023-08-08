using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WeedMaker : MonoBehaviour
{
    [SerializeField] public GameObject weedPrefab;
    [SerializeField] private Tilemap canvas;
    public Dictionary<Vector3Int, GameObject> weedLocations = new Dictionary<Vector3Int, GameObject>(); // cell, weed reference

    private TileGetter tg;

    void Start() {
        tg = GameObject.Find("Touch Manager").GetComponent<TileGetter>();
    }

    public void MakerUpdate() {
        if (Input.touchCount > 0){
            tg.TouchUpdate(canvas, Input.GetTouch(0).position);

            if (!(weedLocations.ContainsKey(tg.lastCell))){
                GameObject newWeed = Instantiate(weedPrefab, tg.lastWorldPt, Quaternion.identity);
                newWeed.transform.parent = UnityEngine.GameObject.Find("Above Ground").transform;
                newWeed.name = "Weed" + weedLocations.Count;

                print("Added weed at " + tg.lastCell);
                weedLocations.Add(tg.lastCell, newWeed);
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Began){
                print("A weed already exists at " + tg.lastCell);
            }
        }
    }
}
