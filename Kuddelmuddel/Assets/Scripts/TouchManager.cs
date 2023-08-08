using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchManger : MonoBehaviour
{
    public Navigation nv;
    public WeedMaker wm;
    //public WeedEraser we;
    //public PinchZoom pz;

    [SerializeField] private int mode;

    void Start(){
        GameObject self = GameObject.Find("Touch Manager");
        nv = self.GetComponent<Navigation>();
        wm = self.GetComponent<WeedMaker>();
        //we = self.GetComponent<WeedEraser>();
        //pz = self.GetComponent<PinchZoom>();
    }

    void Update()
    {
        if(Input.touchCount > 0)
        {
            switch (mode){
                case 0:
                    nv.NavUpdate();
                case 1:
                    wm.MakerUpdate();
                // case 2:
                //     we.EraseUpdate();
                // case 3:
                //     pz.ZoomUpdate();
            }
        }
    }
}