using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
	[SerializeField] private KitchenObjectSO kitchenObjectSO;

	private IKitchenObjectParent kitchenObjectParent;

	public KitchenObjectSO GetKitchenObjectSO()
	{
		return kitchenObjectSO;
	}

	/// <summary>
	/// 设置物体所属的工作台
	/// </summary>
	public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent)
	{
		if (this.kitchenObjectParent != null)
		{
			this.kitchenObjectParent.ClearKitchenObject(); // 清理原父对象
		}
		this.kitchenObjectParent = kitchenObjectParent;

		if (kitchenObjectParent.HasKitchenObject())
		{
			Debug.LogError("kitchenObjectParent上已经有物体了！");
		}

		kitchenObjectParent.SetKitchenObject(this);

		transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform(); // 设置父对象
		transform.localPosition = Vector3.zero; // 设置相对父对象坐标
	}

	/// <summary>
	/// 获得该物体所属的工作台
	/// </summary>
	public IKitchenObjectParent GetKitchenObjectParent()
	{
		return kitchenObjectParent;
	}

	public void DestroySelf()
	{
		GetKitchenObjectParent().ClearKitchenObject();
		Destroy(gameObject);
	}

	public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent kitchenObjectParent)
	{
		Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);

		KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();

		kitchenObject.SetKitchenObjectParent(kitchenObjectParent);

		return kitchenObject;
	}
}
