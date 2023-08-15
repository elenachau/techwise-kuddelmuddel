using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMaker : MonoBehaviour
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
        Debug.Log("Started tile maker");
        MakeRandomGrid();
        SpawnObstacles();
    }

    void MakeRandomGrid() { // random procedural generator
        for (int i = pd.xBounds; i > -pd.xBounds; i--){
            for (int j = 2*pd.yBounds; j > -2*pd.yBounds; j--){
                Vector3Int cell = new Vector3Int(i,j,0);

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

    void SpawnObstacles() {
        for (int i = 0; i < numObstaclesToSpawn; i++){
            int x = Random.Range(-pd.xBounds, pd.xBounds+1);
            int y = Random.Range(-2*pd.yBounds, 2*pd.yBounds+1);
            Vector3Int cell = new Vector3Int(x, y, 0);

            GameObject newObstacle = Instantiate(obstaclePrefab, canvas.CellToWorld(cell), Quaternion.identity);
            newObstacle.transform.parent = GameObject.Find("Above Ground").transform;
            newObstacle.name = "Obstacle (" + x + ", " + y + ", 0)";
            wlm.weedLocations.Add(cell, newObstacle);
        }
    }
}
