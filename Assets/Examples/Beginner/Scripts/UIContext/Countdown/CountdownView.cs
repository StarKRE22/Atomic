using DG.Tweening;
using TMPro;
using UnityEngine;

namespace BeginnerGame
{
    public sealed class CountdownView : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _timeText;

        private Sequence _animationSequence;

        public void SetTime(string value)
        {
            _timeText.text = value;

            _animationSequence?.Kill();

            _animationSequence = DOTween.Sequence()
                .Append(_timeText.transform.DOScale(1.4f, 0.15f).SetEase(Ease.OutBack))
                .Append(_timeText.transform.DOScale(1f, 0.15f).SetEase(Ease.InBack));
        }
    }
}