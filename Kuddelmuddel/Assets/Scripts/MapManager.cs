using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    private PlayerData pd;
    private WeedLocationManager wlm;
    private GameObject tilePrefab;
    private GameObject obstaclePrefab;
    [SerializeField] private Tilemap canvas;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private int numObstaclesToSpawn;

    void Start() {
        pd = GameObject.Find("Player").GetComponent<PlayerData>();
        wlm = GameObject.Find("Weed Location Manager").GetComponent<WeedLocationManager>();
        tilePrefab = GameObject.Find("TileObject");
        obstaclePrefab = GameObject.Find("Obstacle");
        MakeRandomGrid();
        SpawnObstacles(numObstaclesToSpawn);
    }

    void MakeRandomGrid() { // random procedural generator
        for (int i = pd.xBounds; i > -pd.xBounds; i--){
            for (int j = pd.yBounds; j > -pd.yBounds; j--){
                Vector3Int cell = new Vector3Int(i,j,0);

                if (!wlm.tileLocations.ContainsKey(cell)){
                    GameObject newTile = Instantiate(tilePrefab, canvas.CellToWorld(cell), Quaternion.identity);
                    newTile.transform.parent = GameObject.Find("Terrain").transform;
                    newTile.name = "Tile (" + i + ", " + j + ", 0)";
                    wlm.tileLocations.Add(cell, newTile);
                    
                    // Random sprite
                    int choice = Random.Range(0,2);
                    newTile.GetComponent<SpriteRenderer>().sprite = sprites[choice];
                }

            }
        }
    }

    void SpawnObstacles(int count) {
        while (count > 0) {
            int x = Random.Range(-pd.xBounds+1, pd.xBounds);
            int y = Random.Range(-pd.yBounds+1, pd.yBounds);
            Vector3Int cell = new Vector3Int(x, y, 0);

            if (!wlm.weedLocations.ContainsKey(cell)) {
                GameObject newObstacle = Instantiate(obstaclePrefab, canvas.CellToWorld(cell), Quaternion.identity);
                newObstacle.transform.parent = GameObject.Find("Above Ground").transform;
                newObstacle.name = "Obstacle (" + x + ", " + y + ", 0)";
                newObstacle.GetComponent<ObstacleData>().location = cell;
                wlm.weedLocations.Add(cell, newObstacle);
                count--;
            }
        }
    }

    public void CheckMap() {
        // Progress map if all weeds placed
        bool isMapFilled = true;
        for (int i = pd.xBounds; i > -pd.xBounds; i--){
            for (int j = pd.yBounds; j > -pd.yBounds; j--){

                Vector3Int cell = new Vector3Int(i,j,0);
                if (!wlm.weedLocations.ContainsKey(cell)){
                    isMapFilled = false;
                }
                else if(wlm.weedLocations[cell].tag != "Weed"){
                    isMapFilled = false;
                }
            }
        }

        if (isMapFilled){
            print("You win the level!");
            pd.progression += 2;
            pd.SetProgression();
            MakeRandomGrid();
            SpawnObstacles(3);
        }
    }
}
