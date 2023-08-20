using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WeedData : MonoBehaviour
{
    private TileGetter tg;
    private WeedPlanter wp;

    public int growthState = 0; //used to determine which animation to display?
    public float growthRate = 100;
    public float spreadRate = 5f;
    public float spreadChance = 0.75f;
    public int newWeedsPerSpread = 1;
    public int health = 100;
    public bool canGrow = true;
    public bool isGrown = false;
    public bool isGrowing = true;
    public bool isDamagable = true;
    public float totalGrowth = 0; //incremental growth 0 - 100. Grows 5% a second at growth rate of 100? 5 = 5%
    public Vector3Int location;

    void Start() {
        tg = GameObject.Find("Touch Manager").GetComponent<TileGetter>();
        wp = GameObject.Find("Touch Manager").GetComponent<WeedPlanter>();
    }

    public IEnumerator StartGrowthLoop() {

        while (canGrow) {
            yield return new WaitForSeconds(spreadRate);

            List <Vector3Int> freeCells = tg.GetSurroundingFreeCells(location);
            float spreadCheck = Random.Range(0f,1f);
            if (freeCells.Count > 0 && spreadCheck <= spreadChance){
                for (int i = 0; i < newWeedsPerSpread; i++) {
                    Vector3Int newCell = freeCells[Random.Range(0, freeCells.Count)];
                    wp.CreateWeed(newCell);
                }
            }
            canGrow = tg.GetSurroundingFreeCells(location).Count > 0;
        }
    }



}
