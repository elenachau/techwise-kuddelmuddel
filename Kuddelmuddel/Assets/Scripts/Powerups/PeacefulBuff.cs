/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Powerups/WaterBuff")]
public class PeacefulBuff : PowerupEffect
{
    public float duration;


    public override void ApplyEffect(GameObject target)
    {
        // Apply water buff to target
        target.GetComponent<EnemyRate>().enemySpawnRate.value = 0;
        target.GetComponent<Health>().isDamageable.value = false;
        target.GetComponent<Health>().health.value = 100;
        StartCoroutine(DisablePowerup());
    }
    public override IEnumerator DisablePowerup()
    {
        yield return new WaitForSeconds(duration);
        target.GetComponent<EnemyRate>().enemySpawnRate.value = 1;
        target.GetComponent<Health>().isDamageable.value = true;
    }
}



*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/WaterBuff")]

public class PeacefulBuff : PowerupEffect
{

    public float duration = 0;
    public float growthRateMultiplier = 0;
    public float spreadRateMultiplier = 0;
    public string text;
    public bool isEnabled = false;

    public override void ApplyEffect(GameObject target)
    {

        isEnabled = true;

        target.GetComponent<WeedData>().isDamagable = false;


        // if (target.GetComponent<WeedData>().growthState < 10)
        // {
        //     target.GetComponent<WeedData>().canGrow = true;
        // }

        if (target.GetComponent<WeedData>().spreadRate < 100)
        {
            target.GetComponent<WeedData>().spreadRate = 100;
        }

    }

    public override void DisableEffect(GameObject target)
    {
        target.GetComponent<WeedData>().isDamagable = true;
    }

    public override float getDuration()
    {
        return duration;
    }

    public override string getText()
    {
        return text;
    }

}

