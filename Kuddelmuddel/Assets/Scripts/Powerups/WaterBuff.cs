/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Powerups/WaterBuff")]
public class WaterBuff : PowerupEffect
{
    public float duration;
    public float growthRateMultiplier;
    public bool isEnabled = false;
    public override void ApplyEffect(GameObject target)
    {


        isEnabled = true;
        target.GetComponent<GrowthRate>().growthRate.value *= growthRateMultiplier;
        StartCoroutine(DisablePowerup());
        if (isEnabled == false)
        {
            target.GetComponent<GrowthRate>().growthRate.value /= growthRateMultiplier;
        }
    }
    public override IEnumerator DisablePowerup()
    {
        yield return new WaitForSeconds(duration);
        isEnabled = false;
    }
}



*/