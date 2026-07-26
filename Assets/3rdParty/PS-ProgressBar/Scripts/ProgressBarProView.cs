using UnityEngine;
using System.Collections;

namespace PlayfulSystems.ProgressBar { 
    public abstract class ProgressBarProView : MonoBehaviour {
        /// <summary>
        /// Method is called when the value on the bar is changed
        /// </summary>
        public virtual void NewChangeStarted(float currentValue, float targetValue) { }

        /// <summary>
        /// Sets the color of the bar, triggered by ProgressBar.SetBarColor(Color color)
        /// </summary>
        public virtual void SetBarColor(Color color) { }

        /// <summary>
        /// Checks if updating the view is allowed
        /// </summary>
        public virtual bool CanUpdateView(float currentValue, float targetValue) {
            return isActiveAndEnabled;
        }

        /// <summary>
        /// Updates the view with the changed value
        /// </summary>
		public abstract void UpdateView(float currentValue, float targetValue);

#if UNITY_EDITOR
        /// <summary>
        /// Comfort feature: Automatically assigns itself to a parent ProgressBarControl
        /// </summary>
        protected virtual void Reset() {
			var control = GetComponentInParent<global::ProgressBarPro>();

			if (control != null)
				control.AddView(this);

			Debug.Log("Adding to parent control " + control);
		}
#endif
    }
}