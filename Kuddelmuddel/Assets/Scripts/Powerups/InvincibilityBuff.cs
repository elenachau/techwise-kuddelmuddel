using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/InvincibilityBuff")]

public class InvincibilityBuff : PowerupEffect
{

    [SerializeField] private float duration;
    [SerializeField] private int cost;
    [SerializeField] private string text;
    [SerializeField] private bool affectsPrefab;


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

    public override bool getAffectsPrefab() {
        return affectsPrefab;
    }

}

