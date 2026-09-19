using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Player;

public class SelectedCounterVisual : MonoBehaviour
{
	[SerializeField] private ClearCounter clearCounter;
	[SerializeField] private GameObject visualGameObject;

	private void Start()
	{
		// 每个工作台都往Player里面的OnSelectedCounterChanged容器中添加事件监听
		// 由于工作台只有10个左右 所以不会有太大的性能开销
		Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
	}

	private void Player_OnSelectedCounterChanged(OnSelectedCounterChangedArgs args)
	{
		// 如果选中的工作台是自己 就显示选中的视觉效果
		if (args.clearCounter == clearCounter)
		{
			Show();
		}
		// 如果选中的工作台不是自己 就隐藏选中的视觉效果
		else
		{
			Hide();
		}
	}

	private void Show()
	{
		visualGameObject.SetActive(true);
	}

	private void Hide()
	{
		visualGameObject.SetActive(false);
	}
}
