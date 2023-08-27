using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [SerializeField] private Slider musicSlider, effectsSlider;
    [SerializeField] private AudioSource musicSource, effectsSource;

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

    public void ChangeMusicVolume() {
        musicSource.volume = musicSlider.value;
    }

    public void ChangeEffectsVolume() {
        effectsSource.volume = effectsSlider.value;
    }
}
