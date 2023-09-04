using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class Powerup : MonoBehaviour
{
    public PowerupEffect powerupEffect;
    [SerializeField] private TextUpdater textBox;
    [SerializeField] private GameObject weedPrefab;
    public static Powerup Instance;

    void Awake()
    {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

    public void PowerUpAllWeeds()
    {   
        foreach (GameObject obj in WeedLocationManager.Instance.weedLocations.Values.ToList()) {
            powerupEffect.ApplyEffect(obj);
            if (powerupEffect.getDuration() < 1) {
                StartCoroutine(DisablePowerupOnClick(obj));
            }
        }

        // Apply buffs to Weed Prefab - all children (new seeds) will have buff
        // powerupEffect.ApplyEffect(weedPrefab);
        // if (powerupEffect.getDuration() < 1) {
        //         StartCoroutine(DisablePowerupOnClick(weedPrefab));
        // }

        textBox.UpdateText(powerupEffect.getText());
    }

    public IEnumerator DisablePowerupOnClick(GameObject target)
    {
        yield return new WaitForSeconds(powerupEffect.getDuration());
        powerupEffect.DisableEffect(target.gameObject);
        textBox.UpdateText("");
    }
}
