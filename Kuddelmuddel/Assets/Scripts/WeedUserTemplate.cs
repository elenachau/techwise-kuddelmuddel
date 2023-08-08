using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class WeedUserTemplate : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap; // Drag AboveGround tilemap here in inspector
    private WeedLocationManager wlm;
    private TileGetter tg;

    void Start(){
        tg  = GameObject.Find("Touch Manager").GetComponent<TileGetter>();
        wlm = GameObject.Find("Weed Location Manager").GetComponent<WeedLocationManager>();
    }

    void Update()
    {
        if (Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);

            tg.TouchUpdate(tilemap, touch.position);

            if (wlm.weedLocations.ContainsKey(tg.lastCell) && touch.phase == TouchPhase.Began) {
                GameObject clickedWeed = wlm.weedLocations[tg.lastCell];
                WeedData data = clickedWeed.GetComponent<WeedData>();

                // // Read from WeedData.cs
                print("You touched weed #" + data.testNum + ". myString = " + data.testString);
                
                // Write to WeedData.cs
                data.testString = "Touched";

                // WeedData is used for custom properties (age, type of weed)
                // GameObject properties (location, sprite) modifiable as well
            }
        }
    }
}