using UnityEngine;
using UnityEngine.Animations;

namespace Game.UI
{
    public sealed class SelectedMarkerView : MonoBehaviour
    {
        [SerializeField] private PositionConstraint _positionConstraint;

        public void Show(Transform target)
        {
            gameObject.SetActive(true);
            SetTarget(target);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            SetTarget(null);
        }

        private void SetTarget(Transform target)
        {
            _positionConstraint.SetSource(0, new ConstraintSource()
            {
                sourceTransform = target,
                weight = 1f
            });
        }
    }
}