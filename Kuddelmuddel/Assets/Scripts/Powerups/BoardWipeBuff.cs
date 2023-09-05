using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/BoardWipeBuff")]

public class BoardWipeBuff : PowerupEffect
{

    [SerializeField] private float duration;
    [SerializeField] private int cost;
    [SerializeField] private string text;
    [SerializeField] private bool affectsPrefab;

    public override void ApplyEffect(GameObject target)
    {
        if (target.tag == "Obstacle"){
            target.GetComponent<ObstacleData>().RemoveSelf();
        }
    }

    public override void DisableEffect(GameObject target)
    {
        // One-time instant use
    }

    public override float getDuration() { 
        return duration;
    }

    public override int getCost() {
        int removalCost = 0;
        int count = 0;

        foreach (KeyValuePair<Vector3Int, GameObject> entry in WeedLocationManager.Instance.weedLocations) {
            if (entry.Value.tag == "Obstacle") {
                removalCost += PlayerData.Instance.numObstaclesRemoved + count;
                count++;
            }
        }
        if (removalCost == 0){
            return 0; // no upfront fee
        }
        return cost + removalCost;
    }

    public override string getText() {
        return text;
    }

    public override bool getAffectsPrefab() {
        return affectsPrefab;
    }
    
}

