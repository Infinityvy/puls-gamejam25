using UnityEngine;

public class FakeShadow : MonoBehaviour
{
    [SerializeField]
    private Transform parent;

    private Vector3 shadowOffset = new Vector3(0.5f, 0.5f, 0);

    void Start()
    {
        
    }

    void Update()
    {
        transform.rotation = parent.rotation;
        transform.position = parent.position + shadowOffset;
    }
}
