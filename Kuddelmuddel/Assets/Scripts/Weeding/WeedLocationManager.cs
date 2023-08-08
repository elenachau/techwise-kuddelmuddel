using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WeedLocationManager : MonoBehaviour
{
    public Dictionary<Vector3Int, GameObject> weedLocations = new Dictionary<Vector3Int, GameObject>(); // cell, weed reference
    //public Dictionary<GameObject, WeedData> weedDatas = new Dictionary<GameObject, WeedData>();
    
}
