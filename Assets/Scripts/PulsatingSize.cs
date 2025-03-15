using UnityEngine;

public class PulsatingSize : MonoBehaviour
{
    public float pulseSpeed = 1f;
    public float amplitude = 0.1f;

    void Start()
    {
        
    }

    void Update()
    {
        transform.localScale = Vector3.one * (Mathf.Sin(Time.time * pulseSpeed) * amplitude + 1);
    }
}
