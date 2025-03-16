using UnityEngine;
using System.Collections;

public class BulletDestroyEffect : MonoBehaviour
{
    private ParticleSystem ps;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();

        UpdateSpeed();

        Session.Instance.gameSpeedUpdatedEvent.AddListener(UpdateSpeed);
    }

    private void UpdateSpeed()
    {
        ParticleSystem.MainModule main = ps.main;
        main.simulationSpeed = Session.Instance.gameSpeed;
    }

    private void OnDestroy()
    {
        Session.Instance.gameSpeedUpdatedEvent.RemoveListener(UpdateSpeed);
    }
}