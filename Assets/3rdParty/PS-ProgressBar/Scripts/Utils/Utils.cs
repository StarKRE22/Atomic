using UnityEngine;
using System.Collections;

namespace PlayfulSystems {
	public static class Utils {

		public static float EaseSinInOut(float lerp, float start, float change) { 
			return -change / 2 * (Mathf.Cos(Mathf.PI * lerp) - 1) + start; 
		}
	}
}