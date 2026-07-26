#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif

using UnityEngine;
using UnityEngine.EventSystems;

namespace AllIn13DShader
{
    public class AllIn13dShaderDemoController : MonoBehaviour
    {
		public DemoExpositor CurrentExpositor
		{
			get
			{
				return demoExpositors[currentExpositorIndex];
			}
		}

		public DemoElement CurrentDemoElement
		{
			get
			{
				return CurrentExpositor.CurrentDemoElement;
			}
		}


		public DemoSceneConfiguration demoSceneConfig;
		public DemoUI demoUI;
		public Transform initPosition;

		[SerializeField] private SkyController skyController;
		[SerializeField] private AllIn1MainLightController mainLightController;

		[SerializeField] private DemoExpositor[] demoExpositors;
		[SerializeField] private GameObject expositorNameGo;

		[SerializeField] private DemoCameraController cameraController;

		[SerializeField] private GameObject environmentsParentGo;


		//private DemoElement currentDemoElement;
		private DemoElement previousDemoElement;

        [Space, Header("Start Index")]
        [SerializeField] private int startExpositorIndex;
        [SerializeField] private int startDemoIndex;

        [Space, Header("Event Systems")]
        [SerializeField] private EventSystem oldInputEventSystem;
        [SerializeField] private EventSystem newInputEventSystem;

        [Space, Header("Input Handlers")]
		[SerializeField] private InputHandler showDemoInfoHandler;
		
		[Space, Header("Scale Tweens")]
		[SerializeField] private AllIn1DemoScaleTween demoLabelScaleTween;
		[SerializeField] private AllIn1DemoScaleTween expositorLabelScaleTween;
		[SerializeField] private AllIn1DemoScaleTween infoPanelScaleTween;
		
		private int currentExpositorIndex;
        private Vector3 targetCameraPosition;

		private EnvironmentController currentEnvironmentController;
		private bool moveCurrentEnvironment;

        private void Start()
        {
			for(int i = 0; i < demoExpositors.Length; i++)
			{
				demoExpositors[i].Init(demoSceneConfig);
			}
			
			int childCount = environmentsParentGo.transform.childCount;
			for(int i = childCount - 1; i >= 0; i--) Destroy(environmentsParentGo.transform.GetChild(i).gameObject);

            EnableCorrectInputSystem();
            demoExpositors[startExpositorIndex].SetDemoExpositor(startDemoIndex);

			demoUI.Refresh(CurrentExpositor);

			previousDemoElement = null;

			skyController.Init();

			cameraController.Init(demoSceneConfig);
			
			DemoChanged();

			CurrentDemoElement.Show();

			expositorNameGo.SetActive(demoExpositors.Length > 1);
        }
        
        private void Update()
        {
			if (showDemoInfoHandler.IsKeyPressed())
			{
				demoUI.ShowOrHideDemoInfo();
			}
		}
        
        public void NavigateToNextExpositor()
        {
	        previousDemoElement = CurrentDemoElement;
	        Vector3 nextDemoPos = previousDemoElement.transform.position + cameraController.cam.transform.up * demoSceneConfig.distanceBetweenDemos;

	        CurrentDemoElement.Hide();
	        ChangeExpositor(1);

	        moveCurrentEnvironment = false;

	        CurrentDemoElement.transform.position = nextDemoPos;
	        CurrentDemoElement.Show();

	        expositorLabelScaleTween.ScaleDownTween();
	        infoPanelScaleTween.ScaleUpTween();
	        DemoChanged();
        }

        public void NavigateToPreviousExpositor()
        {
	        previousDemoElement = CurrentDemoElement;
	        Vector3 nextDemoPos = previousDemoElement.transform.position - cameraController.cam.transform.up * demoSceneConfig.distanceBetweenDemos;

	        CurrentDemoElement.Hide();
	        ChangeExpositor(-1);

	        moveCurrentEnvironment = false;

	        CurrentDemoElement.transform.position = nextDemoPos;
	        CurrentDemoElement.Show();

	        expositorLabelScaleTween.ScaleDownTween();
	        infoPanelScaleTween.ScaleUpTween();
	        DemoChanged();
        }
        
        public void NavigateToPreviousDemo()
        {
	        previousDemoElement = CurrentDemoElement;

	        DemoElement nextDemoElement = demoExpositors[currentExpositorIndex].GetPreviousDemoElement();
	        Vector3 nextDemoPos = GetNextDemoPos(previousDemoElement, nextDemoElement, -cameraController.cam.transform.right);

	        moveCurrentEnvironment = previousDemoElement.demoElementData.environment == nextDemoElement.demoElementData.environment;

	        demoExpositors[currentExpositorIndex].PreviousDemoElement(nextDemoPos);
	        demoLabelScaleTween.ScaleDownTween();
	        infoPanelScaleTween.ScaleUpTween();
	        DemoChanged();
        }

        public void NavigateToNextDemo()
        {
	        previousDemoElement = CurrentDemoElement;

	        DemoElement nextDemoElement = demoExpositors[currentExpositorIndex].GetNextDemoElement();
	        Vector3 nextDemoPos = GetNextDemoPos(previousDemoElement, nextDemoElement, cameraController.cam.transform.right);

	        moveCurrentEnvironment = previousDemoElement.demoElementData.environment == nextDemoElement.demoElementData.environment;

	        demoExpositors[currentExpositorIndex].NextDemoElement(nextDemoPos);
	        demoLabelScaleTween.ScaleDownTween();
	        infoPanelScaleTween.ScaleUpTween();
	        DemoChanged();
        }
        

		private void LateUpdate()
		{
			if (moveCurrentEnvironment && currentEnvironmentController != null)
			{
				currentEnvironmentController.transform.position += cameraController.GetDeltaMovement();
			}
		}

		private void DemoChanged()
		{
			demoUI.Refresh(CurrentExpositor);

			mainLightController.DemoChanged(CurrentDemoElement.demoElementData);
			skyController.DemoChanged(CurrentDemoElement.demoElementData);

			cameraController.DemoChanged(CurrentDemoElement);

			CheckCurrentEnvironment();
		}

		private Vector3 GetNextDemoPos(DemoElement previousDemo, DemoElement nextDemo, Vector3 dir)
		{
			//Vector3 res = previousDemo.transform.position;

			//if(previousDemo.demoElementData.environment != nextDemo.demoElementData.environment)
			//{
			//	res += dir * demoSceneConfig.distanceBetweenDemos;
			//}

			Vector3 res = previousDemo.transform.position + dir * demoSceneConfig.distanceBetweenDemos;
			return res;
		}

		private void CheckCurrentEnvironment()
		{
			DemoEnvironment currentEnvironment = CurrentDemoElement.demoElementData.environment;

			DemoEnvironment previousEnvironment = previousDemoElement?.demoElementData.environment;

			if(currentEnvironment != previousEnvironment)
			{
				if (currentEnvironmentController != null)
				{
					currentEnvironmentController.Hide();
				}

				if(currentEnvironment != null)
				{
					ChangeEnvironment(currentEnvironment);
				}

				moveCurrentEnvironment = false;
			}
			else
			{
				moveCurrentEnvironment = true;
			}
		}

		private void ChangeEnvironment(DemoEnvironment environment)
		{
			GameObject newEnvironmentGo = Instantiate(environment.prefab);
			newEnvironmentGo.transform.parent = environmentsParentGo.transform;
			newEnvironmentGo.transform.position = CurrentDemoElement.transform.position;
			newEnvironmentGo.transform.rotation = Quaternion.identity;
			newEnvironmentGo.transform.localScale = Vector3.zero;


			currentEnvironmentController = newEnvironmentGo.GetComponent<EnvironmentController>();
			currentEnvironmentController.Init(demoSceneConfig);

			currentEnvironmentController.Show(CurrentDemoElement);
		}

#if UNITY_EDITOR
		[ContextMenu("Setup Demo Scene")]
		public void Setup()
		{
			RemoveOldElements();

			for(int i = 0; i < demoSceneConfig.expositors.Length; i++)
			{
				DemoExpositorData expositorData = demoSceneConfig.expositors[i];

				GameObject goExpositor = new GameObject($"Expositor {i + 1}");
				goExpositor.transform.parent = transform;
				goExpositor.transform.localPosition = new Vector3(0f, i * -25f, 0f);
				goExpositor.transform.localRotation = Quaternion.identity;

				DemoExpositor expositor = goExpositor.AddComponent<DemoExpositor>();
				expositor.Setup(expositorData, demoSceneConfig);

				ArrayUtility.Add(ref demoExpositors, expositor);
			}

			currentExpositorIndex = 0;

			EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
		}

		private void RemoveOldElements()
		{
			DemoExpositor[] expositorsToDestroy = transform.GetComponentsInChildren<DemoExpositor>();
			for(int i = 0; i < expositorsToDestroy.Length; i++)
			{
				DestroyImmediate(expositorsToDestroy[i].gameObject);
			}

			ArrayUtility.Clear(ref demoExpositors);
		}
#endif

		private void ChangeExpositor(int changeAmount)
        {
			currentExpositorIndex += changeAmount;
            if(currentExpositorIndex >= demoExpositors.Length) currentExpositorIndex = 0;
            if(currentExpositorIndex < 0) currentExpositorIndex = demoExpositors.Length - 1;
		}
		
		public int GetCurrentExpositorIndex() => currentExpositorIndex;
        
        private void EnableCorrectInputSystem()
        {
            bool newInputSystemEnabled = false;
#if ENABLE_INPUT_SYSTEM && !ENABLE_LEGACY_INPUT_MANAGER
            newInputSystemEnabled = true;
#endif
            oldInputEventSystem.gameObject.SetActive(!newInputSystemEnabled);
            newInputEventSystem.gameObject.SetActive(newInputSystemEnabled);
        }
    }
}