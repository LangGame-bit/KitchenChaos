using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour, IKitchenObjectParent
{
	[SerializeField] private KitchenObjectSO kitchenObjectSO;
	[SerializeField] private Transform counterTopPoint;

	private KitchenObject kitchenObject;

	public void Interact(Player player)
	{
		// 如果工作台上没有物品 就生成kitchenObjectSO.prefab
		if (kitchenObject == null)
		{
			Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);
			kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this);
		}
		// 工作台上有物品
		else
		{
			// 把物品给玩家
			kitchenObject.SetKitchenObjectParent(player);
		}
	}

	public Transform GetKitchenObjectFollowTransform()
	{
		return counterTopPoint;
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
