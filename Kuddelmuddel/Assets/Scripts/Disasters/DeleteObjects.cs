using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteObjects : MonoBehaviour
{
    private List<GameObject> seedWeeds = new List<GameObject>();
    public bool enabled = false;
    [SerializeField] private float destroyDelay;
    //[SerializeField] private int weedsToDestroyPerCycle;
    private WeedHarvester wh;

    void Start() {
        wh = GameObject.Find("Touch Manager").GetComponent<WeedHarvester>();
    }

    public IEnumerator StartWeedDestruction() {
        
        // Disable growth during disaster
        PopulateSeedWeedsList();
        foreach (GameObject weed in seedWeeds) {
            weed.GetComponent<WeedData>().canSpread = false;
        }

        while (enabled){
            yield return new WaitForSeconds(destroyDelay);
            PopulateSeedWeedsList();
            int numWeedsToDestroyPerCycle = PlayerData.Instance.progression;
            numWeedsToDestroyPerCycle = (numWeedsToDestroyPerCycle < 1) ? 1 : numWeedsToDestroyPerCycle; // set =1 if less than one
            for (int i = 0; i < numWeedsToDestroyPerCycle; i++){
                if (seedWeeds.Count > 0){
                    int randomIndex = Random.Range(0, seedWeeds.Count);
                    wh.DestroyWeed(seedWeeds[randomIndex]);
                }
            }
        }

        // Enable growth again
        PopulateSeedWeedsList();
        foreach (GameObject weed in seedWeeds) {
            weed.GetComponent<WeedData>().canSpread = true;
            weed.GetComponent<WeedData>().StartSpread();
        }
    }

    private void PopulateSeedWeedsList()
    {
        seedWeeds.Clear();

        foreach (KeyValuePair<Vector3Int, GameObject> entry in WeedLocationManager.Instance.weedLocations) {
            if ((entry.Value.tag == "Weed" || entry.Value.tag == "Seed") && entry.Value.GetComponent<WeedData>().isDamagable)
            {
                seedWeeds.Add(entry.Value);
            }
        }
    }
}
