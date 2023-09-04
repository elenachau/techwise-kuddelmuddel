using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/InvincibilityBuff")]

public class InvincibilityBuff : PowerupEffect
{

    public float duration = 0;
    public float growthRateMultiplier = 0;
    public float spreadRateMultiplier = 0;
    public int cost;
    public string text;
    public bool isEnabled = false;

    public override void ApplyEffect(GameObject target)
    {
        if (target.tag == "Weed" || target.tag == "Seed"){
            target.GetComponent<WeedData>().isDamagable = false;
        }
    }

    public override void DisableEffect(GameObject target)
    {
        if (target.tag == "Weed" || target.tag == "Seed"){
            target.GetComponent<WeedData>().isDamagable = true;
        }
    }

    public override float getDuration()
    {
        return duration;
    }

    public override int getCost() {
        return cost;
    }

    public override string getText()
    {
        return text;
    }

}

