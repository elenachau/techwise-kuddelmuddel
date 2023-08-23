using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WeedData : MonoBehaviour
{
    private TileGetter tg;
    private WeedPlanter wp;
    private MapManager mm;
    public Animator animator;
    [SerializeField] private Sprite seedSprite;
    [SerializeField] private Sprite weedSprite;

    public int growthState = 0; //used to determine which animation to display?
    public float growthRate = 10f;
    public float spreadRate = 5f; // seconds
    public float spreadChance = 0.75f;
    public int newWeedsPerSpread = 1;
    public int weedSellValue = 1; // every x sold gets 1 seed back
    public int health = 100;
    public bool canSpread = true;
    public bool isGrown = false;
    public bool isGrowing = false;
    public bool isDamagable = true;
    public float totalGrowth = 0; //incremental growth 0 - 100. Grows 5% a second at growth rate of 100? 5 = 5%
    public Vector3Int location;
    public Coroutine growCoroutine;
    public Coroutine spreadCoroutine;

    void Start() {
        tg = GameObject.Find("Touch Manager").GetComponent<TileGetter>();
        wp = GameObject.Find("Touch Manager").GetComponent<WeedPlanter>();
        mm = GameObject.Find("Map Manager").GetComponent<MapManager>();
    }

    public IEnumerator SpreadLoop() {
        print("Started spreading loop at " + location);
        while (canSpread) {
            yield return new WaitForSeconds(spreadRate);

            List <Vector3Int> freeCells = tg.GetSurroundingFreeCells(location);
            float spreadCheck = Random.Range(0f,1f);
            if (freeCells.Count > 0 && spreadCheck <= spreadChance){
                for (int i = 0; i < newWeedsPerSpread; i++) {
                    Vector3Int newCell = freeCells[Random.Range(0, freeCells.Count)];
                    wp.CreateWeed(newCell);
                }
            }
            canSpread = tg.GetSurroundingFreeCells(location).Count > 0;
        }
    }

    public IEnumerator GrowingStage() {
        isGrowing = true;
        yield return new WaitForSeconds(growthRate);
        if (this != null){
            GrowToNextStage();
        }
    }

    public void GrowToNextStage() {
        animator.Play("Weed_Idle");
        isGrowing = false;
        isGrown = true;
        weedSellValue = 2;
        this.gameObject.GetComponent<SpriteRenderer>().sprite = seedSprite;
        mm.CheckMap();
        spreadCoroutine = StartCoroutine(SpreadLoop());
    }

    public void StartGrowth(){
        growCoroutine = StartCoroutine(GrowingStage());
    }

    void OnDestroy() {
        if (spreadCoroutine != null){
            StopCoroutine(spreadCoroutine);
        }
        StopCoroutine(growCoroutine);
    }

}
