using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchManager : MonoBehaviour
{
    private Navigation nv;
    private WeedPlanter wp;
    private WeedHarvester wh;
    private PinchZoom pz;
    private int mode;
    [SerializeField] private GameObject trail;

    void Start(){
        GameObject self = GameObject.Find("Touch Manager");
        nv = self.GetComponent<Navigation>();
        wp = self.GetComponent<WeedPlanter>();
        wh = self.GetComponent<WeedHarvester>();
        pz = self.GetComponent<PinchZoom>();
    }

    void Update()
    {
        // Handle touch
        if(Input.touchCount > 0)
        {
            TileGetter.Instance.TouchUpdate(Input.GetTouch(0).position);
            trail.SetActive(true);
            trail.transform.position = TileGetter.Instance.lastWorldPt;

            if(Input.GetTouch(0).phase == TouchPhase.Began){
                AudioManager.Instance.PlayUI();
            }

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