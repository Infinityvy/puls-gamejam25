using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static UnityEngine.Timeline.DirectorControlPlayable;

public class Session : MonoBehaviour
{
    public static Session Instance;
    public bool isPaused { get; protected set; } = false;
    public float gameSpeed { get; protected set; } = 0.01f;
    public UnityEvent gameSpeedUpdatedEvent;

    public int levelID = 0;
    private int totalLevelCount = 10;
    private float storedGameSpeed = 0;

    public InputSystem_Actions inputActions { get; protected set; }

    public UnityEvent resetEvent;


    private InputAction resetAction;
    [SerializeField]
    private UIInputHint resetInputHint;

    private InputAction proceedAction;

    private InputAction pauseAction;


    [SerializeField]
    private GameObject failScreen;
    [SerializeField]
    private GameObject successScreen;
    [SerializeField]
    private GameObject pauseScreen;

    public int activeBulletCount { get; private set; } = 0;
    public bool levelEnded { get; private set; } = false;
    public bool levelSuccessInvoked { get; private set; } = false;

    private void Awake()
    {
        Instance = this;

        inputActions = new InputSystem_Actions();

        gameSpeedUpdatedEvent = new UnityEvent();
        resetEvent = new UnityEvent();
    }

    private void Update()
    {
        if (isPaused) return;

        if (resetAction.IsPressed()) resetInputHint.SetState(InputHintState.PRESSED);
        else resetInputHint.SetState(InputHintState.RELEASED);
    }

    private void ResetLevel(InputAction.CallbackContext context)
    {
        if (isPaused) return;

        levelEnded = true;

        CustomInvoker.CancelInvoke(SucceedLevel);
        levelSuccessInvoked = false;
        proceedAction.Disable();

        gameSpeed = 0.01f;
        resetEvent.Invoke();

        failScreen.SetActive(false);
        successScreen.SetActive(false);
        activeBulletCount = 0;

        GetComponent<AudioSource>().Play();

        levelEnded = false;
    }

    private void LoadNextLevel(InputAction.CallbackContext context)
    {
        if (isPaused) return;

        proceedAction.Disable();

        SceneManager.LoadScene("Level" + ((levelID + 1) % totalLevelCount).ToString());
    }

    public void FailLevel()
    {
        if (levelEnded) return;

        failScreen.SetActive(true);
        levelEnded = true;
        levelSuccessInvoked = false;
        SetGameSpeed(0.01f);
    }

    public void InvokeSucceedLevel()
    {
        if (levelEnded) return;

        levelSuccessInvoked = true;
        CustomInvoker.Invoke(SucceedLevel, 0.25f);
    }

    private void SucceedLevel()
    {
        if (levelEnded) return;

        successScreen.SetActive(true);
        levelEnded = true;
        levelSuccessInvoked = false;
        SetGameSpeed(0.01f);

        proceedAction.Enable();
    }

    public void IncreaseBulletCount()
    {
        activeBulletCount++;
    }

    public void DecreseBulletCount()
    {
        activeBulletCount--;

        if(activeBulletCount <= 0 && !levelEnded)
        {
            InvokeSucceedLevel();
        }
    }

    public void SetGameSpeed(float gameSpeed)
    {
        this.gameSpeed = gameSpeed;
        gameSpeedUpdatedEvent.Invoke();
    }

    public void SetPaused(bool state)
    {
        pauseScreen.SetActive(state);
        isPaused = state;


        if (state)
        {
            storedGameSpeed = gameSpeed;
            SetGameSpeed(0);
        }
        else
        {

            SetGameSpeed(storedGameSpeed);
        }
    }

    public void TogglePaused(InputAction.CallbackContext context)
    {
        SetPaused(!isPaused);
    }

    private void OnEnable()
    {
        resetAction = inputActions.Player.Reset;
        resetAction.Enable();
        resetAction.performed += ResetLevel;

        proceedAction = inputActions.Player.Proceed;
        proceedAction.performed += LoadNextLevel;


        pauseAction = inputActions.Player.Pause;
        pauseAction.Enable();
        pauseAction.performed += TogglePaused;
    }

    private void OnDisable()
    {
        resetAction.Disable();
        proceedAction.Disable();
        pauseAction.Disable();
    }
}
