using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Session : MonoBehaviour
{
    public static Session Instance;
    public float gameSpeed = 0.01f;
    public int levelID = 0;
    private int totalLevelCount = 10;

    public InputSystem_Actions inputActions { get; protected set; }

    public UnityEvent resetEvent;


    private InputAction resetAction;
    private InputAction proceedAction;

    [SerializeField]
    private GameObject failScreen;
    [SerializeField]
    private GameObject successScreen;

    public int activeBulletCount { get; private set; } = 0;
    public bool levelEnded { get; private set; } = false;

    private void Awake()
    {
        Instance = this;

        inputActions = new InputSystem_Actions();

        resetEvent = new UnityEvent();
    }

    private void ResetLevel(InputAction.CallbackContext context)
    {
        gameSpeed = 0.01f;
        resetEvent.Invoke();
        failScreen.SetActive(false);
        successScreen.SetActive(false);
        activeBulletCount = 0;
        proceedAction.Disable();
        levelEnded = false;
    }

    private void LoadNextLevel(InputAction.CallbackContext context)
    {
        proceedAction.Disable();

        SceneManager.LoadScene("Level" + ((levelID + 1) % totalLevelCount).ToString());
    }

    public void FailLevel()
    {
        if (levelEnded) return;

        failScreen.SetActive(true);
        levelEnded = true;
    }

    public void SucceedLevel()
    {
        if (levelEnded) return;

        successScreen.SetActive(true);
        levelEnded = true;

        proceedAction.Enable();
    }

    public void IncreaseBulletCount()
    {
        activeBulletCount++;
    }

    public void DecreseBulletCount()
    {
        activeBulletCount--;

        if(activeBulletCount <= 0)
        {
            SucceedLevel();
        }
    }

    private void OnEnable()
    {
        resetAction = inputActions.Player.Reset;
        resetAction.Enable();
        resetAction.performed += ResetLevel;

        proceedAction = inputActions.Player.Proceed;
        proceedAction.performed += LoadNextLevel;
    }

    private void OnDisable()
    {
        resetAction.Disable();
    }
}
