using GameLogic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UI
{
    public class SliderGameSpeed : SliderController
    {
        private Session session;

        private InputSystem_Actions inputActions;

        private InputAction decreaseAction;
        private InputAction increaseAction;

        private const float Increment = 0.1f;
        private const float IncrementationLockout = 0.2f;
        private float timeWhenLastIncremented = 0;
        // [SerializeField]
        // private UIInputHint decreaseInputHint;
        // [SerializeField]
        // private UIInputHint increaseInputHint;

        private void Start()
        {
            session = Session.Instance;
            inputActions = session.inputActions;
            
            decreaseAction = inputActions.Player.DecreaseGameSpeed;
            decreaseAction.Enable();

            increaseAction = inputActions.Player.IncreaseGameSpeed;
            increaseAction.Enable();
            
            valuePrefix = "Game Speed: x";

            slider.SetValueWithoutNotify(session.gameSpeed);

            SetValueText();
            OnValueChanged();

            session.resetEvent.AddListener(ResetSlider);
        }

        private void Update()
        {
            if (session.isPaused) return;

            //DecreaseGameSpeed();
            //IncreaseGameSpeed();
        }

        public override void OnValueChanged()
        {
            session.SetGameSpeed(slider.value);
            SetValueText();
        }

        protected override void ResetSlider()
        {
            slider.value = session.gameSpeed;
        }

        private void DecreaseGameSpeed()
        {
            if (!decreaseAction.IsPressed())
            {
                //decreaseInputHint.SetState(InputHintState.RELEASED);
                return;
            }

            //decreaseInputHint.SetState(InputHintState.PRESSED);

            if (Time.time - timeWhenLastIncremented < IncrementationLockout) return;

            timeWhenLastIncremented = Time.time;
            slider.value -= Increment;
        }

        private void IncreaseGameSpeed()
        {
            if (!increaseAction.IsPressed())
            {
                //increaseInputHint.SetState(InputHintState.RELEASED);
                return;
            }

            //increaseInputHint.SetState(InputHintState.PRESSED);

            if (Time.time - timeWhenLastIncremented < IncrementationLockout) return;

            timeWhenLastIncremented = Time.time;
            slider.value += Increment;
        }

        private void OnDisable()
        {
            decreaseAction.Disable();
            increaseAction.Disable();
        }
    }
}
