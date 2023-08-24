using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Powerup : MonoBehaviour
{
    public PowerupEffect powerupEffect;

    public void PowerUpAllWeeds()
    {   
        foreach (KeyValuePair<Vector3Int, GameObject> entry in WeedLocationManager.Instance.weedLocations) {
            if (entry.Value.tag == "Weed"){
                powerupEffect.ApplyEffect(entry.Value);
                StartCoroutine(DisablePowerupOnClick(entry.Value));
            }
        }
    }

    public IEnumerator DisablePowerupOnClick(GameObject target)
    {
        yield return new WaitForSeconds(powerupEffect.getDuration());
        powerupEffect.DisableEffect(target.gameObject);
    }
}
