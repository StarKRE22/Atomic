using TMPro;
using UnityEngine;

namespace AllIn13DShader
{
	public class DemoUI : MonoBehaviour
	{
		public AllIn13dShaderDemoController demoController;
		public TMP_Text txtExpositorName, txtDemoName, txtDemoViewing;
		public AllIn1DemoScaleTween viewingTween;

		public DemoInfoUI demoInfoUI;

		public void Refresh(DemoExpositor currentExpositor)
		{
			Refresh(currentExpositor.expositorData, currentExpositor.CurrentDemoElement.demoElementData);
		}

		private void Refresh(DemoExpositorData expositorData, DemoElementData demoElementData)
		{
			int currentExpositorIndex = demoController.GetCurrentExpositorIndex() + 1;
			int currentDemoElementIndex = demoController.CurrentExpositor.CurrentDemoElementIndex + 1;
			txtExpositorName.text = $"{currentExpositorIndex}. {expositorData.expositorName}";
			txtDemoName.text = $"{currentDemoElementIndex}. {demoElementData.demoName}";

			demoInfoUI.DemoChanged(demoElementData);
			
			txtDemoViewing.text = $"Viewing: {currentExpositorIndex} - {currentDemoElementIndex}";
			viewingTween.ScaleUpTween();
		}

		public void ShowOrHideDemoInfo()
		{
			demoInfoUI.ShowHideToggle();
		}
	}
}