using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
	private static GameInput instance;
	public static GameInput Instance => instance;

	private PlayerInputActions playerInputActions;

	public UnityAction OnInteractAction;

	private void Awake()
	{
		instance = this;
		playerInputActions = new PlayerInputActions();
		playerInputActions.Player.Enable();
		playerInputActions.Player.Interact.performed += Interact_performed;
	}

	private void Interact_performed(InputAction.CallbackContext obj)
	{
		OnInteractAction?.Invoke();
	}

	public Vector2 GetMovementVectorNormalized()
	{
		Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
		inputVector = inputVector.normalized;
		return inputVector;
	}
}
