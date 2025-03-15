using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerAnimator))]
public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    private Session session;

    public PlayerState state { get; private set; } = PlayerState.STANDING;

    private InputSystem_Actions inputActions;

    private InputAction lungeAction;

    private PlayerAnimator playerAnimator;

    [SerializeField]
    private UIInputHint lungeInputHint;

    [SerializeField]
    private Camera mainCamera;

    [SerializeField]
    private Transform prevDirection;

    private Vector3 startPos;

    #region Lunge Settings
    private Vector3 velocity = Vector3.zero;
    private float lungeSpeed = 30f;
    private float turnSpeed = 10f;
    private float weight = 3;
    private Vector2 lungeDirection = Vector2.zero;
    private Quaternion targetRotation = Quaternion.identity;
    #endregion

    void Awake()
    {
        Instance = this;

        session = Session.Instance;

        inputActions = session.inputActions;

        playerAnimator = GetComponent<PlayerAnimator>();
    }

    private void Start()
    {
        startPos = transform.position;

        session.resetEvent.AddListener(ResetPlayer);

        prevDirection.position = transform.position;
    }

    void Update()
    {
        RotateToCursor();
        Move();
    }

    private void Lunge(InputAction.CallbackContext context)
    {
        if (state == PlayerState.LUNGING) return;

        SetState(PlayerState.LUNGING);
        lungeInputHint.SetState(InputHintState.PRESSED);

        Vector3 cursorPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        lungeDirection = (cursorPos - transform.position).normalized;

        prevDirection.rotation = transform.rotation;
        targetRotation = transform.rotation;

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

        velocity = lungeDirection * lungeSpeed * session.gameSpeed;


        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime * session.gameSpeed);

        transform.position += velocity * Time.deltaTime;
    }

    public void UpdateDirection(Vector2 otherDirection)
    {
        lungeDirection = (lungeDirection * weight + otherDirection).normalized;
        targetRotation = Quaternion.LookRotation(Vector3.forward, lungeDirection);
    }

    private void SetState(PlayerState state)
    {
        this.state = state;

        playerAnimator.SetState(state);
    }

    private void ResetPlayer()
    {
        SetState(PlayerState.STANDING);
        lungeInputHint.SetState(InputHintState.RELEASED);

        transform.position = startPos;

        velocity = Vector3.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Bullet>(out Bullet bullet))
        {
            bullet.DestroyBullet();
            UpdateDirection(collision.transform.up);
        }
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
