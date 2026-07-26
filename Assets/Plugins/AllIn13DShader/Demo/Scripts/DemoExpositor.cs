#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;

namespace AllIn13DShader
{
    public class DemoExpositor : MonoBehaviour
    {
		public DemoExpositorData expositorData;
        [SerializeField] private DemoElement[] demoElements;
        
        private int currentDemoIndex;

		public DemoElement CurrentDemoElement => demoElements[currentDemoIndex];
		public int CurrentDemoElementIndex => currentDemoIndex;

		public void Init(DemoSceneConfiguration demoSceneConfig)
		{
			for (int i = 0; i < demoElements.Length; i++)
			{
				demoElements[i].Init(demoSceneConfig, i);
			}
		}

        public void SetDemoExpositor(int newIndex)
        {
            currentDemoIndex = newIndex;
        }

		public DemoElement GetNextDemoElement()
		{
			int nextIndex = currentDemoIndex + 1;
			if (nextIndex >= demoElements.Length)
			{
				nextIndex = 0;
			}

			DemoElement res = demoElements[nextIndex];
			return res;
		}

		public DemoElement GetPreviousDemoElement()
		{
			int nextIndex = currentDemoIndex - 1;
			if (nextIndex < 0)
			{
				nextIndex = demoElements.Length - 1;
			}

			DemoElement res = demoElements[nextIndex];
			return res;
		}

		public void NextDemoElement(Vector3 demoPos)
        {
			CurrentDemoElement.Hide();

			currentDemoIndex++;
			if (currentDemoIndex >= demoElements.Length)
			{
				currentDemoIndex = 0;
			}

			CurrentDemoElement.transform.position = demoPos;
			CurrentDemoElement.Show();
        }
        
        public void PreviousDemoElement(Vector3 demoPos)
        {
			CurrentDemoElement.Hide();

			currentDemoIndex--;
			if (currentDemoIndex < 0)
			{
				currentDemoIndex = demoElements.Length - 1;
			}

			CurrentDemoElement.transform.position = demoPos;
			CurrentDemoElement.Show();
		}
        
		public void SetElementDemoIndex(int demoIndex)
		{
			currentDemoIndex = Mathf.Min(demoIndex, demoElements.Length - 1);
		}

		public DemoElement FindNearestElement(Vector3 referencePosition, out int demoIndex)
		{
			DemoElement res = null;

			float minSqrDistance = float.MaxValue;
			demoIndex = 0;

			Vector3 referencePositionWithoutY = referencePosition;
			referencePositionWithoutY.y = 0f;


			for (int i = 0; i < demoElements.Length; i++)
			{
				Vector3 demoElementWithoutY = demoElements[i].transform.position;
				demoElementWithoutY.y = 0f;

				float sqrDistance = (referencePositionWithoutY - demoElementWithoutY).sqrMagnitude;
				if (sqrDistance <= minSqrDistance)
				{
					minSqrDistance = sqrDistance;
					res = demoElements[i];
					demoIndex = i;
				}
			}

			if(CurrentDemoElement.demoElementData.environment == res.demoElementData.environment &&
				res.demoElementData.environment != null)
			{
				res = CurrentDemoElement;
				demoIndex = CurrentDemoElement.demoIndex;
			}

			return res;
		}


#if UNITY_EDITOR
		public void Setup(DemoExpositorData expositorData, DemoSceneConfiguration demoSceneConfig)
		{
			this.expositorData = expositorData;

			if (demoElements != null)
			{
				ArrayUtility.Clear(ref demoElements);
			}
			else
			{
				demoElements = new DemoElement[0];
			}

			DemoEnvironment lastEnvironment = null;
			Vector3 lastLocalPosition = new Vector3(-demoSceneConfig.distanceBetweenDemos, 0f, 0f);
			for (int i = 0; i < expositorData.demos.Length; i++)
			{
				DemoElementData demoElementData = expositorData.demos[i];
				GameObject demoElementGo = (GameObject)PrefabUtility.InstantiatePrefab(demoElementData.prefab, transform);

				lastLocalPosition.x += demoSceneConfig.distanceBetweenDemos;
				lastEnvironment = demoElementData.environment;

				demoElementGo.transform.localPosition = lastLocalPosition;
				demoElementGo.transform.localRotation = Quaternion.identity;
				Debug.LogError($"{gameObject.name} - {demoElementGo.name} at {demoElementGo.transform.localPosition} -t:{Time.time}", demoElementGo);

				DemoElement demoElement = demoElementGo.GetComponent<DemoElement>();

				ArrayUtility.Add(ref demoElements, demoElement);
			}

			currentDemoIndex = 0;
		}
#endif
	}
}