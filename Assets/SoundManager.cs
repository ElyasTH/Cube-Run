using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [Header("Sound Track")]
    [SerializeField] private AudioSource _soundTrackSource;
    
    [Header("Sound Effect")]
    [SerializeField] private AudioSource _soundEffectSource;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip changeLineSound;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip slideSound;

    private void Awake()
    {
        if (!instance) instance = this;
    }

    public void PLAY_DEATH_SOUND()
    {
        _soundEffectSource.clip = deathSound;
        _soundEffectSource.Play();
    }
    
    public void PLAY_CHANGE_LINE_SOUND()
    {
        _soundEffectSource.clip = changeLineSound;
        _soundEffectSource.Play();
    }
    
    public void PLAY_JUMP_SOUND()
    {
        _soundEffectSource.clip = jumpSound;
        _soundEffectSource.Play();
    }

    public void PLAY_SLIDE_SOUND()
    {
        _soundEffectSource.clip = slideSound;
        _soundEffectSource.Play();
    }

    public void STOP_MUSIC()
    {
        _soundTrackSource.Stop();
    }
}
