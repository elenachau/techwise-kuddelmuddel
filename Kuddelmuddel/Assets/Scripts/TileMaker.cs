using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMaker : MonoBehaviour
{
    private PlayerData pd;
    private WeedLocationManager wlm;
    private GameObject tilePrefab;
    [SerializeField] private Tilemap canvas;
    [SerializeField] private Sprite[] sprites;

    void Start() {
        pd = GameObject.Find("Player").GetComponent<PlayerData>();
        wlm = GameObject.Find("Weed Location Manager").GetComponent<WeedLocationManager>();
        tilePrefab = GameObject.Find("TileObject");
        MakeRandomGrid();
    }

    void MakeRandomGrid() { // random procedural generator
        for (int i = pd.xBounds; i > -pd.xBounds; i--){
            for (int j = 2*pd.yBounds; j > -2*pd.yBounds; j--){
                Vector3Int cell = new Vector3Int(i,j,0);
                GameObject newTile = Instantiate(tilePrefab, canvas.CellToWorld(cell), Quaternion.identity);
                int choice = Random.Range(0,2);
                newTile.GetComponent<SpriteRenderer>().sprite = sprites[choice];
                newTile.transform.parent = UnityEngine.GameObject.Find("Terrain").transform;
                newTile.name = "Tile (" + i + ", " + j + ", 0)";
                wlm.tileLocations.Add(cell, newTile);
            }
        }
    }
}
