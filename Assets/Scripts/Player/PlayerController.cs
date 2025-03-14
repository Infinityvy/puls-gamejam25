using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    private Session session;

    public PlayerState state { get; private set; } = PlayerState.STANDING;

    private InputSystem_Actions inputActions;

    private InputAction lungeAction;

    private PlayerAnimator playerAnimator;
    private Rigidbody2D rb;

    [SerializeField]
    private Camera mainCamera;

    [SerializeField]
    private Transform prevDirection;


    #region Lunge Settings
    private float lungeSpeed = 30f;
    private Vector2 lungeDirection = Vector2.zero;
    #endregion

    void Awake()
    {
        Instance = this;

        session = Session.Instance;

        inputActions = session.inputActions;

        playerAnimator = GetComponent<PlayerAnimator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        RotateToCursor();
        Move();
    }

    private void Lunge(InputAction.CallbackContext context)
    {
        if (state == PlayerState.LUNGING) return;

        state = PlayerState.LUNGING;

        playerAnimator.SetState(state);

        rb.WakeUp();

        Vector3 cursorPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        lungeDirection = (cursorPos - transform.position).normalized;

        prevDirection.rotation = transform.rotation;

        if (!prevDirection.gameObject.activeSelf) prevDirection.gameObject.SetActive(true);
    }

    private void RotateToCursor()
    {
        if (state == PlayerState.LUNGING) return;

        Vector3 cursorPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        transform.rotation = Quaternion.LookRotation(Vector3.forward, cursorPos - transform.position);
    }

    private void Move()
    {
        if (state == PlayerState.STANDING) return;

        rb.linearVelocity = lungeDirection * lungeSpeed * session.gameSpeed;
    }

    private void OnEnable()
    {
        lungeAction = inputActions.Player.Lunge;
        lungeAction.Enable();
        lungeAction.performed += Lunge;
    }

    private void OnDisable()
    {
        lungeAction.Disable();
    }
}
