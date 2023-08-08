using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WeedObject : MonoBehaviour
{
    public GameObject weedObject;
    private TileGetter tg;
    [SerializeField]
    private Tilemap canvas;
    // Start is called before the first frame update
    void Start()
    {
        tg = GameObject.Find("Map Manager").GetComponent<TileGetter>();
    }

    // Update is called once per frame

    void MakerUpdate() {
        if (Input.touchCount > 0){
            tg.TouchUpdate(canvas, Input.GetTouch(0).position);
            MakeWeed();
            //newWeed.transform.parent = UnityEngine.GameObject.Find("Above Ground").transform;
        }
    }

    public void MakeWeed(){
        GameObject newWeed = Instantiate(weedObject, tg.lastWorldPt, Quaternion.identity);
    }
}
