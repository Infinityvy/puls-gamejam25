using UnityEngine;
using UnityEngine.InputSystem;

public class SliderGameSpeed : SliderController
{
    private Session session;

    private InputSystem_Actions inputActions;

    private InputAction decreaseAction;
    private InputAction increaseAction;

    private float increment = 0.1f;
    private float incrementationLockout = 0.2f;
    private float timeWhenLastIncremented = 0;
    [SerializeField]
    private UIInputHint decreaseInputHint;
    [SerializeField]
    private UIInputHint increaseInputHint;

    private void Awake()
    {
        session = Session.Instance;

        inputActions = session.inputActions;
    }

    void Start()
    {
        valuePrefix = "Game Speed: x";

        slider.value = 0f;

        SetValueText();
        OnValueChanged();

        session.resetEvent.AddListener(ResetSlider);
    }

    private void Update()
    {
        if (session.isPaused) return;

        DecreaseGameSpeed();
        IncreaseGameSpeed();
    }

    public override void OnValueChanged()
    {
        session.SetGameSpeed(slider.value);
    }

    protected override void ResetSlider()
    {
        slider.value = 0f;
    }

    private void DecreaseGameSpeed()
    {
        if (!decreaseAction.IsPressed())
        {
            decreaseInputHint.SetState(InputHintState.RELEASED);
            return;
        }

        decreaseInputHint.SetState(InputHintState.PRESSED);

        if (Time.time - timeWhenLastIncremented < incrementationLockout) return;

        timeWhenLastIncremented = Time.time;
        slider.value -= increment;
    }

    private void IncreaseGameSpeed()
    {
        if (!increaseAction.IsPressed())
        {
            increaseInputHint.SetState(InputHintState.RELEASED);
            return;
        }

        increaseInputHint.SetState(InputHintState.PRESSED);

        if (Time.time - timeWhenLastIncremented < incrementationLockout) return;

        timeWhenLastIncremented = Time.time;
        slider.value += increment;
    }

    private void OnEnable()
    {
        decreaseAction = inputActions.Player.DecreaseGameSpeed;
        decreaseAction.Enable();

        increaseAction = inputActions.Player.IncreaseGameSpeed;
        increaseAction.Enable();
    }

    private void OnDisable()
    {
        decreaseAction.Disable();
        increaseAction.Disable();
    }
}
