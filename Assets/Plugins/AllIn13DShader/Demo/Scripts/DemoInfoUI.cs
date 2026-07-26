using System;
using TMPro;
using UnityEngine;

namespace AllIn13DShader
{
	public class DemoInfoUI : MonoBehaviour
	{
		[SerializeField] private RectTransform offScreenT, initialT;
		[SerializeField] private float smoothingAmount;
		[SerializeField] private TMP_Text txtDemoInfo;
		
		private RectTransform myT, currentTargetT;
		private bool isClosed;

		private void Start()
		{
			myT = transform as RectTransform;
			CopyRectTransform(myT, initialT);
			currentTargetT = offScreenT;
			myT.position = currentTargetT.position;
			isClosed = true;
		}

		private void Update()
		{
			if(currentTargetT == null) return;
			myT.position = Vector3.Lerp(myT.position, currentTargetT.position, smoothingAmount * Time.deltaTime);
		}

		public void DemoChanged(DemoElementData demoElementdata)
		{
			txtDemoInfo.text = demoElementdata.info;
		}
		
		public void ShowHideToggle()
		{
			isClosed = !isClosed;
			currentTargetT = isClosed ? offScreenT : initialT;
		}

		private void CopyRectTransform(RectTransform source, RectTransform target)
		{
			if(source == null || target == null)
			{
				Debug.LogError("Source or target RectTransform is null");
				return;
			}
    
			// Copy anchoring
			target.anchorMin = source.anchorMin;
			target.anchorMax = source.anchorMax;
			target.pivot = source.pivot;
    
			// Copy positioning
			target.anchoredPosition = source.anchoredPosition;
			target.anchoredPosition3D = source.anchoredPosition3D;
			target.sizeDelta = source.sizeDelta;
    
			// Copy rotation and scale
			target.rotation = source.rotation;
			target.localRotation = source.localRotation;
			target.localScale = source.localScale;
    
			// Copy offset values
			target.offsetMin = source.offsetMin;
			target.offsetMax = source.offsetMax;
		}
	}
}