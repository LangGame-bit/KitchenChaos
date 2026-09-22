using UnityEngine;
using UnityEngine.UI;
using static CuttingCounter;

public class ProgressBarUI : MonoBehaviour
{
	[SerializeField] private CuttingCounter cuttingCounter;
	[SerializeField] private Image imgBar;

	private void Start()
	{
		cuttingCounter.OnProgressChanged += CuttingCounter_OnProgressChanged;
		Hide();
	}

	private void CuttingCounter_OnProgressChanged(OnProgressChangedEventArgs args)
	{
		imgBar.fillAmount = args.progressNormalized;
		if(imgBar.fillAmount == 0 || imgBar.fillAmount == 1)
		{
			Hide();
		}
		else
		{
			Show();
		}
	}

	private void Show()
	{
		gameObject.SetActive(true);
	}

	private void Hide()
	{
		gameObject.SetActive(false);
	}
}
