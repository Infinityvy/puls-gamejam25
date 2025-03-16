using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BulletImpactEffect : MonoBehaviour
{
    [SerializeField]
    private List<ParticleSystem> particleSystems;

    void Start()
    {
        Session.Instance.gameSpeedUpdatedEvent.AddListener(UpdateSpeed);
        Session.Instance.resetEvent.AddListener(DestroyThis);

        UpdateSpeed();
    }


    private void UpdateSpeed()
    {
        float gameSpeed = Session.Instance.gameSpeed;

        for (int i = 0; i < particleSystems.Count; i++)
        {
            ParticleSystem.MainModule module = particleSystems[i].main;
            module.simulationSpeed = gameSpeed;
        }
    }

    private void DestroyThis()
    {
        Session.Instance.gameSpeedUpdatedEvent.RemoveListener(UpdateSpeed);
        Session.Instance.resetEvent.RemoveListener(DestroyThis);

        Destroy(gameObject);
    }
}