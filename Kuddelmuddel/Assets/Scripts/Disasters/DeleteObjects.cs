using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteObjects : MonoBehaviour
{
    private List<GameObject> seedWeeds = new List<GameObject>();
    private float timeSinceLastDeletion = 0;

    void Update()
    {
        // Every 2 minutes, trigger the weed deletion process
        timeSinceLastDeletion += Time.deltaTime;
        if (timeSinceLastDeletion >= 120f) // 120 seconds = 2 minutes
        {
            timeSinceLastDeletion = 0f;
            DestroyRandomSeeds();
        }
    }

    private void DestroyRandomSeeds()
    {
        PopulateSeedWeedsList();
        int weedsToDeleteCount = Mathf.FloorToInt(seedWeeds.Count * 0.6f); // Calculate 60%
        int deletionCounter = 0;

        while (weedsToDeleteCount > 0)
        {
            int randomIndex = Random.Range(0, seedWeeds.Count);
            Destroy(seedWeeds[randomIndex]);
            seedWeeds.RemoveAt(randomIndex);
            weedsToDeleteCount--;

            deletionCounter++;

            // Ensure no more than 2 are deleted at once
            if (deletionCounter >= 2)
            {
                break;
            }
        }
    }

    private void PopulateSeedWeedsList()
    {
        seedWeeds.Clear();

        foreach (var entry in WeedLocationManager.Instance.weedLocations)
        {
            if (entry.Value.tag == "Seed")
            {
                seedWeeds.Add(entry.Value);
            }
        }
    }
}
