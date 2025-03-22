using NUnit.Framework;
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

    [SerializeField]
    private Transform bulletImpactPrefab;
    

    private Vector3 startPos;

    #region Lunge Settings
    private float lungeSpeed = 30f;
    private float turnSpeed = 10f;
    private float weight = 3;
    private Vector3 velocity = Vector3.zero;
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
        if (session.isPaused) return;

        RotateToCursor();
        Move();
    }

    private void OnLungeAction(InputAction.CallbackContext context) { Lunge(); }
    public void Lunge()
    {
        if (session.isPaused) return;
        if (state == PlayerState.LUNGING) return;

        SetState(PlayerState.LUNGING);
        lungeInputHint.SetState(InputHintState.PRESSED);

        Vector3 cursorPos = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        lungeDirection = (cursorPos - transform.position).normalized;

        prevDirection.rotation = transform.rotation;
        targetRotation = transform.rotation;

        if (!prevDirection.gameObject.activeSelf) prevDirection.gameObject.SetActive(true);
    }

    private void RotateToCursor()
    {
        if (state == PlayerState.LUNGING) return;

        Vector3 cursorPos = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());

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

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.TryGetComponent(out Bullet bullet))
        {
            Vector3 impactPoint = GetComponentInChildren<Collider2D>().ClosestPoint(collider.transform.position);
            Instantiate(bulletImpactPrefab, impactPoint, Quaternion.identity, transform);

            bullet.InstDestroyEffect();
            bullet.DestroyBullet();
            UpdateDirection(collider.transform.up * bullet.weight);
        }
    }

    private void OnEnable()
    {
        lungeAction = inputActions.Player.Lunge;
        lungeAction.Enable();
        lungeAction.performed += OnLungeAction;
    }

    private void OnDisable()
    {
        lungeAction.Disable();
    }
}
