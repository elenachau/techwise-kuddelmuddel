using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public PowerupEffect powerupEffect;

    public void OnTriggerEnter2D(Collider2D collision) {
        Destroy(gameObject);
        powerupEffect.ApplyEffect(collision.gameObject);
       
        //int duration = powerupEffect.collision.gameObject.duration;
        StartCoroutine(DisablePowerup(collision));
    }


    public IEnumerator DisablePowerup(Collider2D collision)
    {
        yield return new WaitForSeconds(powerupEffect.getDuration());
        powerupEffect.DisableEffect(collision.gameObject);
    }
}
