using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
	private enum Mode
	{
		LookAt,
		LookAtInverted,
		CameraForward,
		CameraForwardInverted,
	}

	[SerializeField] private Mode mode;

	private void LateUpdate()
	{
		switch (mode)
		{
			case Mode.LookAt:
				transform.rotation = Quaternion.LookRotation(Camera.main.transform.position - transform.position);
				break;
			case Mode.LookAtInverted:
				transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
				break;
			case Mode.CameraForward:
				transform.forward = Camera.main.transform.forward;
				break;
			case Mode.CameraForwardInverted:
				transform.forward = -Camera.main.transform.forward;
				break;
		}
	}
}
