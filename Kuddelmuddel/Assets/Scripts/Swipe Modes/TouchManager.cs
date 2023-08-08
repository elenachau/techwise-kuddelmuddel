using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class TouchManger : MonoBehaviour
{
    private Navigation nv;
    private WeedMaker wm;
    //public WeedEraser we;
    private PinchZoom pz;
    private TileGetter tg;

    [SerializeField] private int mode;
    [SerializeField] private GameObject trail;
    [SerializeField] private Tilemap tilemap;

    void Start(){
        GameObject self = GameObject.Find("Touch Manager");
        nv = self.GetComponent<Navigation>();
        wm = self.GetComponent<WeedMaker>();
        //we = self.GetComponent<WeedEraser>();
        pz = self.GetComponent<PinchZoom>();
        tg = self.GetComponent<TileGetter>();
    }

    void Update()
    {
        if(Input.touchCount > 0)
        {
            tg.TouchUpdate(tilemap, Input.GetTouch(0).position);
            trail.SetActive(true);
            trail.transform.position = tg.lastWorldPt;

            switch (mode){
                case 0:
                    nv.NavUpdate();
                    break;
                case 1:
                    wm.MakerUpdate();
                    break;
                // case 3:
                //     we.EraseUpdate();
                //        break;
                case 2:
                    pz.ZoomUpdate();
                    break;
            }
        }
    }
}