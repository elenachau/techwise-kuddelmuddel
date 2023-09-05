using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Powerups/GrowthBuff")]


public class GrowthBuff : PowerupEffect
{

    [SerializeField] private float duration;
    [SerializeField] private int cost;
    [SerializeField] private string text;
    [SerializeField] private bool affectsPrefab;


    public override void ApplyEffect(GameObject target)
    {
        if (target.tag == "Seed"){
            WeedData data = target.GetComponent<WeedData>();
            data.GrowToNextStage();
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
        return cost;
    }

    public override string getText() {
        return text;
    }

    public override bool getAffectsPrefab() {
        return affectsPrefab;
    }
    
}

