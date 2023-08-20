using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WeedData : MonoBehaviour
{
    public int growthState = 0; //used to determine which animation to display?
    public float growthRate = 100;
    public float spreadRate = 5f;
    public float spreadBufferTime = 5f; // seconds
    public float spreadChance = 0.75f;
    public int health = 100;
    public bool canGrow = true;
    public bool isGrown = false;
    public bool isGrowing = true;
    public bool isDamagable = true;
    public float totalGrowth = 0; //incremental growth 0 - 100. Grows 5% a second at growth rate of 100? 5 = 5%
    public Vector3Int location;

    public IEnumerator GrowChild(Tilemap canvas, TileGetter tg) {
        print("Started coroutine");

        yield return new WaitForSeconds(spreadBufferTime);

        while (canGrow) {

            List<Vector3Int> freeCells = tg.GetSurroundingFreeCells(location);
            print(freeCells.Count);
            float spreadCheck = Random.Range(0,1);
            print(spreadCheck + ", " + spreadChance);
            
            if (spreadCheck <= spreadChance){

                // Make weed
                GameObject newWeed = Instantiate(this.gameObject, canvas.CellToWorld(freeCells[0]), Quaternion.identity);
                newWeed.transform.parent = GameObject.Find("Above Ground").transform;
                newWeed.name = "Weed" + freeCells[0];
                newWeed.GetComponent<WeedData>().location = freeCells[0];
                freeCells.RemoveAt(0);

                //pd.weedCount += 1;
                //weedCountText.text = "" + pd.weedCount;

                //wlm.weedLocations.Add(tg.lastCell, newWeed);
                //mm.CheckMap();
            }

            canGrow = freeCells.Count > 0;
            print(canGrow);

            yield return new WaitForSeconds(spreadRate);
        }
        print("ended roiutine");
    }

}
