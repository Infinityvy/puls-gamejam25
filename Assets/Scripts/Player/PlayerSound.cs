using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Audio;

public class PlayerSound : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent(out Bullet bullet))
        {
            DisposableAudioSource.Play("metal_impact_" + Random.Range(0, 3).ToString(), AudioGroup.SFX);
        }
    }
}
