using UnityEngine;

public class Session : MonoBehaviour
{
    public static Session Instance;
    public float gameSpeed = 0f;

    public InputSystem_Actions inputActions { get; protected set; }

    private void Awake()
    {
        Instance = this;

        inputActions = new InputSystem_Actions();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
