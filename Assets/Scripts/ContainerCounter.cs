using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ContainerCounter : BaseCounter
{
	public UnityAction OnPlayerGrabbedObject;
	[SerializeField] private KitchenObjectSO kitchenObjectSO;
	
	public override void Interact(Player player)
	{
		// 实例化物品并给玩家
		if (!HasKitchenObject())
		{
			Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
			kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(player);

			OnPlayerGrabbedObject?.Invoke();
		}
	}
}
