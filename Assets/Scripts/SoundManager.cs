using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("Sound Track")]
    [SerializeField] private AudioSource _soundTrackAudioSource;

    [Header("Sound Effect")]
    [SerializeField] private AudioSource _soundEffectAudioSource;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip changeLineSound;
    [SerializeField] private AudioClip slideSound;
    [SerializeField] private AudioClip deathSound;

    public void PLAY_JUMP_SOUND()
    {
        _soundEffectAudioSource.clip = jumpSound;
        _soundEffectAudioSource.Play();
    }
    public void PLAY_CHANGE_LINE_SOUND()
    {
        _soundEffectAudioSource.clip = changeLineSound;
        _soundEffectAudioSource.Play();
    }
    public void PLAY_SLIDE_SOUND()
    {
        _soundEffectAudioSource.clip = slideSound;
        _soundEffectAudioSource.Play();
    }
    public void PLAY_DEATH_SOUND()
    {
        _soundEffectAudioSource.clip = deathSound;
        _soundEffectAudioSource.Play();
    }
    public void STOP_MUSIC()
    {
        _soundTrackAudioSource.Stop();
    }
}
