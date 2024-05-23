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
        if (powerupEffect.getText() == "DANCE PARTY!!!") {
            AudioManager.Instance.PlayDanceParty();
        }

        if (powerupEffect.getAffectsPrefab()) {
            powerupEffect.ApplyEffect(weedPrefab);
        }

        foreach (GameObject obj in WeedLocationManager.Instance.weedLocations.Values.ToList()) {
            powerupEffect.ApplyEffect(obj);
        }

        if (powerupEffect.getDuration() > 1) {
            StartCoroutine(DisablePowerup());
        }

        textBox.UpdateText(powerupEffect.getText());
    }

    public IEnumerator DisablePowerup()
    {
        yield return new WaitForSeconds(powerupEffect.getDuration());
        if (powerupEffect.getAffectsPrefab()) {
            powerupEffect.DisableEffect(weedPrefab);
        }
        foreach (GameObject obj in WeedLocationManager.Instance.weedLocations.Values.ToList()) {
            powerupEffect.DisableEffect(obj);
        }
        textBox.UpdateText("");
    }
}
