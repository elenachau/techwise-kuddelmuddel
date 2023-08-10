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