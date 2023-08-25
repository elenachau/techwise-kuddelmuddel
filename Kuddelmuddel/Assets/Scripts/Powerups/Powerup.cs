using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Powerup : MonoBehaviour
{
    public PowerupEffect powerupEffect;
    public static Powerup Instance;
    private GameObject weedPrefab;

    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

    void Start() {
        weedPrefab = GameObject.Find("WeedObject");
    }

    public void PowerUpAllWeeds()
    {   
        foreach (KeyValuePair<Vector3Int, GameObject> entry in WeedLocationManager.Instance.weedLocations) {
            if (entry.Value.tag == "Weed" || entry.Value.tag == "Seed"){
                powerupEffect.ApplyEffect(entry.Value);
                StartCoroutine(DisablePowerupOnClick(entry.Value));
            }
        }

        // Apply buffs to Weed Prefab - all children (new seeds) will have buff
        powerupEffect.ApplyEffect(weedPrefab);
        StartCoroutine(DisablePowerupOnClick(weedPrefab));
    }

    public IEnumerator DisablePowerupOnClick(GameObject target)
    {
        yield return new WaitForSeconds(powerupEffect.getDuration());
        powerupEffect.DisableEffect(target.gameObject);
    }
}
