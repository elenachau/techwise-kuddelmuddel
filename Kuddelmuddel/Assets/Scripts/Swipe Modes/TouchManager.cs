using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class TouchManger : MonoBehaviour
{
    private Navigation nv;
    private WeedMaker wm;
    private WeedEraser we;
    private PinchZoom pz;
    private TileGetter tg;

    [SerializeField] private int mode;
    [SerializeField] private GameObject trail;
    [SerializeField] private Tilemap tilemap;

    void Start(){
        GameObject self = GameObject.Find("Touch Manager");
        nv = self.GetComponent<Navigation>();
        wm = self.GetComponent<WeedMaker>();
        we = self.GetComponent<WeedEraser>();
        pz = self.GetComponent<PinchZoom>();
        tg = self.GetComponent<TileGetter>();
    }

    void Update()
    {
        // DEBUG: Change modes
        if(Input.GetKeyDown("1")){
            mode = 3;
            Debug.Log("Mode: Eraser"); }
        else if(Input.GetKeyDown("2")){
            mode = 2;
            Debug.Log("Mode: Maker"); }
        else if(Input.GetKeyDown("3")){
            mode = 1;
            Debug.Log("Mode: Zoom"); }
        else if(Input.GetKeyDown("4")){
            mode = 1;
            Debug.Log("Mode: Navigate"); }

        else if(Input.touchCount > 0)
        {
            tg.TouchUpdate(tilemap, Input.GetTouch(0).position);
            trail.SetActive(true);
            trail.transform.position = tg.lastWorldPt;

            switch (mode){
                case 0:
                    nv.NavUpdate();
                    break;
                case 1:
                    pz.ZoomUpdate();
                    break;
                case 2:
                    wm.MakerUpdate();
                    break;
                case 3:
                    we.EraseUpdate();
                       break;
            }
        }
    }
}