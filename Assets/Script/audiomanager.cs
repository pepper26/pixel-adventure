using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audiomanager : MonoBehaviour
{
    [Header("-----------audio source----------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("-----------SFX source------------")]
    public AudioClip background;
    public AudioClip Collect;
    public AudioClip CheckPoint;
    public AudioClip death;
    public AudioClip finish;
    public AudioClip jump;
    public AudioClip killenermy;
    public AudioClip trampoline;
    public AudioClip movingplatform;
    public AudioClip fallplaform;

    void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

}
