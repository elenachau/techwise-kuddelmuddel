using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WeedLocationManager : MonoBehaviour
{
    public GameObject weedObject;
    private Vector3 vector3 = new Vector3(0f, 1f, 2f);
    [SerializeField]
    private Tilemap weedMap;


    [SerializeField]
    private Sprite[] weedSprites = new Sprite[2];
    public SpriteRenderer spriteRenderer;

    // 1 means basic weed, 2 means water weed
    private Dictionary<Vector3Int, int> weedLocations = new Dictionary<Vector3Int, int>();
    // Start is called before the first frame update
    void Start()
    {
        testPrintVector(vector3);   
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}
//public Grid grid; //  You can also use the Tilemap object
        public void Update() {
            //Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Vector3Int coordinate = weedMap.WorldToCell(mouseWorldPos);
            //Debug.Log(coordinate);
        }


    private static void testPrintVector(Vector3 v) {
        Debug.Log("Hello...!");
        Debug.Log(v);
    }
    
}
