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
        while (enabled){
            yield return new WaitForSeconds(destroyDelay);
            PopulateSeedWeedsList();
            for (int i = 0; i < PlayerData.Instance.progression / 3; i++){
                int randomIndex = Random.Range(0, seedWeeds.Count);
                wh.DestroyWeed(seedWeeds[randomIndex]);
            }
        }
    }

    private void PopulateSeedWeedsList()
    {
        seedWeeds.Clear();

        foreach (KeyValuePair<Vector3Int, GameObject> entry in WeedLocationManager.Instance.weedLocations) {
            if (entry.Value.tag == "Weed")
            {
                seedWeeds.Add(entry.Value);
            }
        }
    }
}
