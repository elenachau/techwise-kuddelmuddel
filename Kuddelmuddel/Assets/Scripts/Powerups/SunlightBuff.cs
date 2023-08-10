using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Powerups/SunlightBuff")]


public class SunlightBuff : PowerupEffect
{


    public float duration = 0;
    public float growthRateMultiplier = 0;
    public float spreadRateMultiplier = 0;
    public bool isEnabled = false;

    public override void ApplyEffect(GameObject target)
    {
        //GameStats stats = target.GetComponent<GameStats>();

        isEnabled = true;
        
        target.GetComponent<WeedData>().growthRate *= growthRateMultiplier;
        target.GetComponent<WeedData>().spreadRate *= spreadRateMultiplier;
    }

    public override void DisableEffect(GameObject target)
    {

        target.GetComponent<WeedData>().growthRate /= growthRateMultiplier;
        target.GetComponent<WeedData>().spreadRate /= spreadRateMultiplier;
        
    }

    public override float getDuration() { 
        return duration;
    }
    
}

