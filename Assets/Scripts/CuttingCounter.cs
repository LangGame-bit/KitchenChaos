using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
	[SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;

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

	public override void Cut(Player player)
	{
		// 切菜台上有东西 AND 它可以被切
		if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
		{
			KitchenObjectSO outputKitchenObject = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
			
			GetKitchenObject().DestroySelf();

			KitchenObject.SpawnKitchenObject(outputKitchenObject, this);
		}
	}


	/// <summary>
	/// 判断一个物品 是否可以切
	/// </summary>
	private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
	{
		foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
		{
			if (cuttingRecipeSO.input == inputKitchenObjectSO)
			{
				return true;
			}
		}
		return false;
	}

	/// <summary>
	/// 得到被切之后的物品
	/// </summary>
	private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
	{
		foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
		{
			if (cuttingRecipeSO.input == inputKitchenObjectSO)
			{
				return cuttingRecipeSO.output;
			}
		}
		return null;
	}
}
