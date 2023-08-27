using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WeedData : MonoBehaviour
{
    private WeedPlanter wp;
    private MapManager mm;
    public Animator animator;
    [SerializeField] public RuntimeAnimatorController defaultAnimController;
    [SerializeField] private Sprite seedSprite;
    [SerializeField] private Sprite weedSprite;

    public int growthState = 0; //used to determine which animation to display?
    public float growthRate = 10f;
    public float spreadRate = 5f; // seconds
    public float spreadChance = 0.50f;
    public int newWeedsPerSpread = 1;
    public bool canSpread = true;
    public bool isGrown = false;
    public bool isGrowing = false;
    public bool isDamagable = true;
    public float totalGrowth = 0; //incremental growth 0 - 100. Grows 5% a second at growth rate of 100? 5 = 5%
    public Vector3Int location;
    public Coroutine growCoroutine;
    public Coroutine spreadCoroutine;

    void Start() {
        wp = GameObject.Find("Touch Manager").GetComponent<WeedPlanter>();
        mm = GameObject.Find("Map Manager").GetComponent<MapManager>();
    }

    public IEnumerator SpreadLoop() {
        //print("Started spreading loop at " + location);
        do {
            yield return new WaitForSeconds(spreadRate);
            CheckSpreadable();
            List <Vector3Int> freeCells = TileGetter.Instance.GetSurroundingFreeCells(location);
            float spreadCheck = Random.Range(0f,1f);
            if (canSpread && spreadCheck <= spreadChance){
                animator.SetTrigger("GrowTrigger");
                for (int i = 0; i < newWeedsPerSpread; i++) {
                    Vector3Int newCell = freeCells[Random.Range(0, freeCells.Count)];
                    wp.CreateWeed(newCell);
                }
            }
            animator.SetTrigger("ReturnIdle");
        } while (canSpread);
    }

    public void CheckSpreadable() {
        canSpread = (TileGetter.Instance.GetSurroundingFreeCells(location).Count > 0);
    }

    public void ChangeAnimatorController(RuntimeAnimatorController controller){
        animator.runtimeAnimatorController = controller;
    }

    public IEnumerator GrowingStage() {
        isGrowing = true;
        yield return new WaitForSeconds(growthRate);
        if (this != null){
            GrowToNextStage();
        }
    }

    public void GrowToNextStage() {
        animator.Play("Weed_sprout");
        animator.SetTrigger("GrowTrigger");
        AudioManager.Instance.PlayHarvestSFX();
        isGrowing = false;
        isGrown = true;
        this.gameObject.GetComponent<SpriteRenderer>().sprite = seedSprite;
        this.gameObject.tag = "Weed";
        PlayerData.Instance.AddWeeds(1);
        mm.CheckMap();
        spreadCoroutine = StartCoroutine(SpreadLoop());
    }

    public void StartGrowth(){
        growCoroutine = StartCoroutine(GrowingStage());
    }
    
    public void StartSpread(){
        spreadCoroutine = StartCoroutine(SpreadLoop());
    }

    void OnDestroy() {
        if (spreadCoroutine != null){
            StopCoroutine(spreadCoroutine);
        }
        StopCoroutine(growCoroutine);
    }

}
