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
		// 如果玩家没有携带物品
		if (!player.HasKitchenObject())
		{
			KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);

			OnPlayerGrabbedObject?.Invoke();
		}
	}
}
