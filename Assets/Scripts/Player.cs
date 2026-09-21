using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour, IKitchenObjectParent
{
	public static Player Instance {  get; private set; }

	public class OnSelectedCounterChangedArgs
	{
		public ClearCounter clearCounter;

		public OnSelectedCounterChangedArgs(ClearCounter clearCounter)
		{
			this.clearCounter = clearCounter;
		}
	}

	[Tooltip("移动速度")]
    [SerializeField] private float moveSpeed;

	// 角度平滑用的速度记录 SmoothDampAngle会自己更新
	private float currentVelocity;

	// 是否正在走路
	private bool isWalking;
	public bool IsWalking() => isWalking;

	// 面前的工作台
	private ClearCounter selectedCounter;

	// 当选中的工作台改变时 要做的事情
	public UnityAction<OnSelectedCounterChangedArgs> OnSelectedCounterChanged;

	private KitchenObject kitchenObject;
	[SerializeField] private Transform kitchenObjectHoldPoint;

	private void Awake()
	{
		if (Instance != null)
		{
			Debug.LogError("场景上存在多个玩家");
		}
		Instance = this;
	}

	private void Start()
	{
		// 添加按下交互键的事件调用
		GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
	}

	private void Update()
	{
		HandleMovement();
		HandleInteraction();
	}

	private void GameInput_OnInteractAction()
	{
		if (selectedCounter != null)
		{
			selectedCounter.Interact(this);
		}
	}

	/// <summary>
	/// 处理移动逻辑
	/// </summary>
	private void HandleMovement()
	{
		Vector2 inputVector = GameInput.Instance.GetMovementVectorNormalized();

		Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

		float playerHeight = 2;
		float playerRadius = 0.7f;
		float moveDistance = moveSpeed * Time.deltaTime;
		bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

		if (!canMove) //不能朝这个方向移动
		{
			//尝试只朝X轴移动
			Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
			canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);
			if (canMove)
			{
				moveDir = moveDirX;
			}
			else //不能朝X轴移动
			{
				//尝试只朝Z轴移动
				Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
				canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);
				if (canMove)
				{
					moveDir = moveDirZ;
				}
			}
		}
		if (canMove)
		{
			transform.Translate(moveDir * moveDistance, Space.World);
		}

		if (inputVector != Vector2.zero)
		{
			isWalking = true;
			float targetAngle = Mathf.Atan2(inputVector.x, inputVector.y) * Mathf.Rad2Deg;
			float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currentVelocity, 0.1f);
			transform.rotation = Quaternion.Euler(transform.eulerAngles.x, angle, transform.eulerAngles.z);
		}
		else
		{
			isWalking = false;
			currentVelocity = 0;
		}
	}

	/// <summary>
	/// 处理交互逻辑
	/// </summary>
	private void HandleInteraction()
	{
		float interactDistance = 2f; //可以交互的距离
		if (Physics.Raycast(transform.position, transform.forward, out RaycastHit raycastHit, interactDistance))
		{
			// 面前有物体 而且是工作台
			if (raycastHit.transform.TryGetComponent<ClearCounter>(out ClearCounter clearCounter))
			{
				if (clearCounter != selectedCounter)
				{
					SetSelectedCounter(clearCounter);
				}
			}
			else //面前物体没有ClearCounter脚本 说明不是工作台
			{
				SetSelectedCounter(null);
			}
		}
		else //面前没有物体
		{
			SetSelectedCounter(null);
		}
	}

	/// <summary>
	/// 设置选中的工作台
	/// </summary>
	private void SetSelectedCounter(ClearCounter selectedCounter)
	{
		this.selectedCounter = selectedCounter;
		// 调用选中工作台改变时的事件
		OnSelectedCounterChanged?.Invoke(new OnSelectedCounterChangedArgs(selectedCounter));
	}

	public Transform GetKitchenObjectFollowTransform()
	{
		return kitchenObjectHoldPoint;
	}

	public void SetKitchenObject(KitchenObject kitchenObject)
	{
		this.kitchenObject = kitchenObject;
	}

	public KitchenObject GetKitchenObject()
	{
		return kitchenObject;
	}

	public void ClearKitchenObject()
	{
		kitchenObject = null;
	}

	public bool HasKitchenObject()
	{
		return kitchenObject != null;
	}
}
