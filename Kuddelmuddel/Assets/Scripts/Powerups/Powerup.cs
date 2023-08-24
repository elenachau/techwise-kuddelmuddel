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

    public void PowerUpAllWeeds()
    {   
        print("Starting powerup");
        foreach (KeyValuePair<Vector3Int, GameObject> entry in wlm.weedLocations) {
            if (entry.Value.tag == "Weed"){
                powerupEffect.ApplyEffect(entry.Value);
                print("Applied effect");
                StartCoroutine(DisablePowerupOnClick(entry.Value));
            }
        }
    }

    public IEnumerator DisablePowerupOnClick(GameObject target)
    {
        yield return new WaitForSeconds(powerupEffect.getDuration());
        powerupEffect.DisableEffect(target.gameObject);
        print("Powerup disabled");
    }

    // public IEnumerator DisablePowerupCollision(Collider2D collision)
    // {
    //     yield return new WaitForSeconds(powerupEffect.getDuration());
    //     powerupEffect.DisableEffect(collision.gameObject);
    // }




}
