using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private SpeederMovement _speederMovement;
    private InputMaster _controlls;

    private void Awake()
    {
        _controlls = new InputMaster();
        _speederMovement = GetComponent<SpeederMovement>();
    }

    private void OnEnable()
    {
        _controlls.Player.Accelerate.performed += AccelerateOn;
        _controlls.Player.Accelerate.canceled += AccelerateOff;
        _controlls.Player.Accelerate.Enable();

        _controlls.Player.Break.performed += BreakOn;
        _controlls.Player.Break.canceled += BreakOff;
        _controlls.Player.Break.Enable();

        _controlls.Player.Turn.performed += TurnOn;
        _controlls.Player.Turn.canceled += TurnOff;
        _controlls.Player.Turn.Enable();
    }

    private void OnDisable()
    {
        _controlls.Player.Accelerate.performed -= AccelerateOn;
        _controlls.Player.Accelerate.canceled -= AccelerateOff;
        _controlls.Player.Accelerate.Disable();

        _controlls.Player.Break.performed -= BreakOn;
        _controlls.Player.Break.canceled -= BreakOff;
        _controlls.Player.Break.Disable();

        _controlls.Player.Turn.performed -= TurnOn;
        _controlls.Player.Turn.canceled -= TurnOff;
        _controlls.Player.Turn.Disable();
    }

    private void AccelerateOn(InputAction.CallbackContext context) =>_speederMovement.StartAccelerating();
    private void AccelerateOff(InputAction.CallbackContext context) => _speederMovement.StopAccelerating();
    private void TurnOn(InputAction.CallbackContext context) => _speederMovement.StartTurning(context.ReadValue<float>());
    private void TurnOff(InputAction.CallbackContext context) => _speederMovement.StopTurning();
    private void BreakOn(InputAction.CallbackContext context) => _speederMovement.StartBreaking();
    private void BreakOff(InputAction.CallbackContext context) => _speederMovement.StopBreaking();
}
