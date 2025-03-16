using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Audio;

public class PlayerSound : MonoBehaviour
{
    [SerializeField]
    private List<AudioClip> impactSounds;

    [SerializeField]
    private AudioMixerGroup mixerGroup;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent<Bullet>(out Bullet bullet))
        {
            AudioClip clip = impactSounds[Random.Range(0, impactSounds.Count)];
            DisposableAudioSource.Play(clip, mixerGroup);
        }
    }
}
