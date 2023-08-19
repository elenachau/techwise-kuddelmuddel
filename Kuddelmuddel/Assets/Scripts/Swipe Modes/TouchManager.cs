using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class TouchManager : MonoBehaviour
{
    private Navigation nv;
    private WeedPlanter wp;
    private WeedHarvester wh;
    private PinchZoom pz;
    private TileGetter tg;
    private WeedLocationManager wlm;
    private int mode;
    [SerializeField] private GameObject trail;
    [SerializeField] private Tilemap tilemap;

    void Start(){
        GameObject self = GameObject.Find("Touch Manager");
        nv = self.GetComponent<Navigation>();
        wp = self.GetComponent<WeedPlanter>();
        wh = self.GetComponent<WeedHarvester>();
        pz = self.GetComponent<PinchZoom>();
        tg = self.GetComponent<TileGetter>();
        wlm = GameObject.Find("Weed Location Manager").GetComponent<WeedLocationManager>();
    }

    void Update()
    {
        // Handle touch
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
                    pz.ZoomUpdate();
                    break;
                case 2:
                    wp.PlanterUpdate();
                    break;
                case 3:
                    wh.HarvesterUpdate();
                       break;
            }
        }
    }

    public void SetModeNavigate() {
        mode = 0;
        Debug.Log("Mode: Navigate");
    }

    public void SetModeZoom() {
        mode = 1;
        Debug.Log("Mode: Zoom");
    }

    public void SetModePlant() {
        mode = 2;
        Debug.Log("Mode: Plant");
    }

    public void SetModeHarvest() {
        mode = 3;
        Debug.Log("Mode: Harvest");
    }
}