using DG.Tweening;
using TMPro;
using UnityEngine;

namespace SampleGame
{
    public sealed class MoneyView : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _valueText;

        [SerializeField]
        private float _animationDuration = 0.5f;

        private int _currentValue;

        public void SetMoney(int newValue, string format)
        {
            DOTween.Kill(_valueText);

            DOTween.To(
                    () => _currentValue,
                    x =>
                    {
                        _currentValue = x;
                        _valueText.text = string.Format(format, _currentValue);
                    }, newValue, _animationDuration)
                .SetEase(Ease.OutQuad)
                .SetTarget(_valueText);
        }
    }
}