using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
	public override void Interact(Player player)
	{
		// 工作台上没有物品
		if (!HasKitchenObject())
		{
			// 玩家携带的有物品
			if (player.HasKitchenObject())
			{
				// 把玩家携带的物品放在工作台上
				player.GetKitchenObject().SetKitchenObjectParent(this);
			}
			// 玩家没有携带物品
			else { }
		}
		// 工作台上有物品
		else
		{
			// 玩家携带的有物品
			if (player.HasKitchenObject()) { }
			// 玩家没有携带物品
			else
			{
				GetKitchenObject().SetKitchenObjectParent(player);
			}
		}
	}
}
