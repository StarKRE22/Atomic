using DG.Tweening;
using UnityEngine;

namespace Game.UI
{
    public sealed class CharacterRenderer : MonoBehaviour
    {
        [SerializeField] private string _numericPropertyName;
        [SerializeField] private Renderer[] _hitRenderers;
        [SerializeField] private AnimationCurve _animationCurve;
        [SerializeField] private float _hitDuration = 0.3f;
        [SerializeField] private float _minValue;
        [SerializeField] private float _maxValue;

        private int _hitPropertyId;

        private void Awake()
        {
            _hitPropertyId = Shader.PropertyToID(_numericPropertyName);
        }

        public void PlayHit()
        {
            foreach (var renderer in _hitRenderers)
            {
                DOVirtual.Float(0f, 1f, _hitDuration, normalizedTime =>
                {
                    float curveValue = _animationCurve.Evaluate(normalizedTime);
                    float remappedValue = Mathf.Lerp(_minValue, _maxValue, curveValue);
                    Material[] materials = renderer.materials;
                    foreach (Material t in materials)
                        if (t != null) 
                            t.SetFloat(_hitPropertyId, remappedValue);
                });
            }
        }
    }
}