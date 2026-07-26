using UnityEngine;

#if ALLIN13DSHADER_BIRP && UNITY_POST_PROCESSING_STACK_V2
using UnityEngine.Rendering.PostProcessing;
#endif

namespace AllIn13DShader
{
	public class DemoCameraController : MonoBehaviour
	{
		public enum TransitionState
		{
			NONE = 0,
			MOVING = 1,
		}

		private Vector3 srcPosition;
		private Quaternion srcRotation;

		private Vector3 dstPosition;
		private Quaternion dstRotation;

		private AnimationCurve cameraCurve;

		private TransitionState transitionState;
		private float timer;
		private float transitionDuration;

		private Vector3 deltaMovement;

		public Transform target;
		public float lerpSpeed;

		[Header("Camera")]
		public Camera cam;

#if ALLIN13DSHADER_BIRP && UNITY_POST_PROCESSING_STACK_V2
		[Header("Postprocess")]
		public PostProcessLayer postProcessLayer;
		public PostProcessVolume postProcessVolume;
#endif

		public void Init(DemoSceneConfiguration demoSceneConfig)
		{
			this.cameraCurve = demoSceneConfig.cameraCurve;
			this.transitionState = TransitionState.NONE;
			this.transitionDuration = demoSceneConfig.cameraTransitionDuration;
		}

		public void DemoChanged(DemoElement demoElement)
		{
			transitionState = TransitionState.MOVING; 

			srcPosition = target.position;
			srcRotation = target.rotation;

			dstPosition = demoElement.transform.position;
			dstRotation = Quaternion.identity;

#if ALLIN13DSHADER_BIRP && UNITY_POST_PROCESSING_STACK_V2
			if (postProcessLayer != null)
			{
				postProcessLayer.enabled = demoElement.demoElementData.postProcessEnabled;
			}
			
			if(postProcessVolume != null)
			{
				postProcessVolume.enabled = demoElement.demoElementData.postProcessEnabled;
			}
#endif
		}

		public void GoTo(Transform tr)
		{
			transitionState = TransitionState.MOVING;

			dstPosition = tr.position;
			dstRotation = tr.rotation;

			srcPosition = target.position;
			srcRotation = target.rotation;
		}

		public Vector3 GetDeltaMovement()
		{
			return deltaMovement;
		}

		private void Update()
		{
			if(transitionState == TransitionState.MOVING)
			{
				Update_Moving();
			}
			else
			{
				deltaMovement = Vector3.zero;
			}
		}

		private void Update_Moving()
		{
			timer += Time.deltaTime;
			float t = timer / transitionDuration;
			float curveT = cameraCurve.Evaluate(t);

			Vector3 newPosition = Vector3.LerpUnclamped(srcPosition, dstPosition, curveT);
			deltaMovement = newPosition - target.transform.position;

			target.transform.position = newPosition;
			target.transform.rotation = Quaternion.SlerpUnclamped(srcRotation, dstRotation, curveT);

			if(t >= 1f)
			{
				transitionState = TransitionState.NONE;
				timer = 0f;
			}
		}
	}
}