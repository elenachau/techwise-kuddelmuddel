using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Powerups/DancePartyBuff")]


public class DancePartyBuff : PowerupEffect
{

    [SerializeField] private float duration = 10f;
    [SerializeField] private float newSpreadRate = 0.5f;
    [SerializeField] private float newSpreadChance = 1f;
    [SerializeField] private float newGrowthRate = 1f;
    [SerializeField] private string text;
    [SerializeField] private RuntimeAnimatorController newAnimations;

    public override void ApplyEffect(GameObject target)
    {
        WeedData data = target.GetComponent<WeedData>();
        data.growthRate = newGrowthRate;
        data.spreadRate = newSpreadRate;
        data.spreadChance = newSpreadChance;
        //data.ChangeAnimatorController(newAnimations);
        data.StartSpread();

    }

    public override void DisableEffect(GameObject target)
    {
        WeedData data = target.GetComponent<WeedData>();
        data.spreadRate = 5f;
        data.spreadChance = 0.5f;
        data.growthRate = 10f;
        //data.ChangeAnimatorController(data.defaultAnimController);

    }

    public override float getDuration() { 
        return duration;
    }

    public override string getText() {
        return text;
    }
    
}

