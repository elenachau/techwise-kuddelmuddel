using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileGetter : MonoBehaviour
{   
    public static TileGetter Instance;
    public TileBase lastTile;
    public Vector3Int lastCell;
    public Vector2 lastWorldPt;
    public GameObject lastWeed;
    [SerializeField] public Tilemap terrain;
    [SerializeField] public Tilemap aboveGround;

    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

    public void TouchUpdate(Vector3 screenPos){ // Call this within touch if statement
        lastWorldPt = Camera.main.ScreenToWorldPoint(screenPos);
        lastCell = terrain.WorldToCell(lastWorldPt);
        lastTile = terrain.GetTile(lastCell);

        if(WeedLocationManager.Instance.weedLocations.ContainsKey(lastCell)){
            lastWeed = WeedLocationManager.Instance.weedLocations[lastCell];
        }
    }

    public List<Vector3Int> GetSurroundingCells(Vector3Int cell) {
        List<Vector3Int> surrounding = new List<Vector3Int>();
        for (int i = -1; i < 2; i++) {
            for (int j = -1; j < 2; j++) {
                Vector3Int surroundingCell = new Vector3Int(cell.x + i, cell.y + j, cell.z);
                surrounding.Add(surroundingCell);
            }
        }
        surrounding.Remove(cell);
        return surrounding;
    }

    public List<GameObject> GetSurroundingObjectsOfTag(Vector3Int cell, string tag = "any") {
        List<GameObject> surroundingObjects = new List<GameObject>();
        surroundingObjects.Clear();
        List<Vector3Int> surroundingCells = GetSurroundingCells(cell);

        foreach (Vector3Int sCell in surroundingCells) {
            if(WeedLocationManager.Instance.weedLocations.ContainsKey(sCell)){
                GameObject sObject = WeedLocationManager.Instance.weedLocations[sCell];
                if (tag != "any"){
                    if (sObject.tag == tag){
                        surroundingObjects.Add(sObject);
                    }
                }
            }
        }

        return surroundingObjects;
    }

    public List<Vector3Int> GetSurroundingFreeCells(Vector3Int cell) {
        List<Vector3Int> surroundingCells = new List<Vector3Int>();
        for (int i = -1; i < 2; i++) {
            for (int j = -1; j < 2; j++) {
                Vector3Int sCell = new Vector3Int(cell.x + i, cell.y + j, cell.z);
                if ( WeedLocationManager.Instance.tileLocations.ContainsKey(sCell)
                    && !WeedLocationManager.Instance.weedLocations.ContainsKey(sCell) ){
                    surroundingCells.Add(sCell);
                }
            }
        }

        return surroundingCells;
    }
}
