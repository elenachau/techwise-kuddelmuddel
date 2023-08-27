using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [SerializeField] private Slider musicSlider, effectsSlider;
    [SerializeField] private AudioSource musicSource, effectsSource;

    // Saved sound effects
    [SerializeField] private AudioClip sfx_uiClick;
    [SerializeField] private List<AudioClip> sfxs_harvestSounds;
    [SerializeField] private List<AudioClip> sfxs_plantingSounds;
    [SerializeField] public AudioClip sfx_removedObstacle;
    [SerializeField] private AudioClip sfx_volumeTestSound;
    [SerializeField] private AudioClip music_baseBackground;
    [SerializeField] private AudioClip music_danceParty;

    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

    public void PlaySoundEffect(AudioClip clip) {
        effectsSource.PlayOneShot(clip);
    }

    public void PlayUI() {
        effectsSource.PlayOneShot(sfx_uiClick);
    }

    public void PlayPlantingSFX() {
        int choice = Random.Range(0, sfxs_plantingSounds.Count);
        effectsSource.PlayOneShot(sfxs_plantingSounds[choice]);
    }

    public void PlayHarvestSFX() {
        int choice = Random.Range(0, sfxs_harvestSounds.Count);
        effectsSource.PlayOneShot(sfxs_harvestSounds[choice]);
    }

    public void ChangeMusicVolume() {
        musicSource.volume = musicSlider.value;
    }

    public void ChangeEffectsVolume() {
        effectsSource.volume = effectsSlider.value;
    }
}
