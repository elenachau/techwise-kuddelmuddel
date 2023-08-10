using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Powerup : MonoBehaviour
{
    private WeedLocationManager wlm;

    public PowerupEffect powerupEffect;

    void Start()
    {
        wlm = GameObject.Find("Weed Location Manager").GetComponent<WeedLocationManager>();
    }
    // public void OnTriggerEnter2D(Collider2D collision)
    // {
    //     Destroy(gameObject);
    //     powerupEffect.ApplyEffect(collision.gameObject);


    //     StartCoroutine(DisablePowerup(collision));
    // }

    public void PowerUpAllWeeds()
    {   
        foreach (KeyValuePair<Vector3Int, GameObject> entry in wlm.weedLocations) {
            powerupEffect.ApplyEffect(entry.Value);
            StartCoroutine(DisablePowerupOnClick(entry.Value));
        }
    }

    public IEnumerator DisablePowerupOnClick(GameObject target)
    {
        yield return new WaitForSeconds(powerupEffect.getDuration());
        powerupEffect.DisableEffect(target.gameObject);
    }

    // public IEnumerator DisablePowerupCollision(Collider2D collision)
    // {
    //     yield return new WaitForSeconds(powerupEffect.getDuration());
    //     powerupEffect.DisableEffect(collision.gameObject);
    // }




}
