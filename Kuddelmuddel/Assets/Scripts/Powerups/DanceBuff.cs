using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Powerups/DancePartyBuff")]


public class DancePartyBuff : PowerupEffect
{

    [SerializeField] private float duration;
    [SerializeField] private int cost;
    [SerializeField] private float newSpreadRate;
    [SerializeField] private float newSpreadChance;
    [SerializeField] private float newGrowthRate;
    [SerializeField] private string text;
    [SerializeField] private RuntimeAnimatorController newAnimations;

    public override void ApplyEffect(GameObject target)
    {
        if (target.tag == "Seed" || target.tag == "Weed"){
            WeedData data = target.GetComponent<WeedData>();
            data.growthRate = newGrowthRate;
            data.spreadRate = newSpreadRate;
            data.spreadChance = newSpreadChance;
            //data.ChangeAnimatorController(newAnimations);
            data.StartSpread();
        }
    }

    public override void DisableEffect(GameObject target)
    {
        if (target.tag == "Seed" || target.tag == "Weed"){
            WeedData data = target.GetComponent<WeedData>();
            data.spreadRate = 5f;
            data.spreadChance = 0.5f;
            data.growthRate = 10f;
            //data.ChangeAnimatorController(data.defaultAnimController);
        }

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
    
}

