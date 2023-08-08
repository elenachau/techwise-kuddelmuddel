using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WeedMaker : MonoBehaviour
{
    [SerializeField] public GameObject weedPrefab;
    [SerializeField] private Tilemap canvas;

    private TileGetter tg;

    void Start() {
        tg = GameObject.Find("Touch Manager").GetComponent<TileGetter>();
    }

    public void MakerUpdate() {
        if (Input.touchCount > 0){
            tg.TouchUpdate(canvas, Input.GetTouch(0).position);
            MakeWeed();
            //newWeed.transform.parent = UnityEngine.GameObject.Find("Above Ground").transform;
        }
    }

    public void MakeWeed(){
        GameObject newWeed = Instantiate(weedPrefab, tg.lastWorldPt, Quaternion.identity);
    }
}
