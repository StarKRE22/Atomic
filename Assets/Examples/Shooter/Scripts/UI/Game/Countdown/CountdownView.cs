// using DG.Tweening;
// using TMPro;
// using UnityEngine;
//
// namespace BeginnerGame
// {
//     public sealed class CountdownView : MonoBehaviour
//     {
//         [SerializeField]
//         private TMP_Text _timeText;
//
//         [Header("Animation Settings")]
//         [SerializeField]
//         private Color _highlightColor = Color.red;
//       
//         [SerializeField]
//         private float _duration = 0.3f;
//
//         private Sequence _animationSequence;
//
//         public void SetTime(string value)
//         {
//             _timeText.text = value;
//             this.AnimateColor();
//         }
//
//         private void AnimateColor()
//         {
//             if (_animationSequence != null)
//                 return;
//
//             Color originalColor = _timeText.color;
//
//             _animationSequence = DOTween.Sequence()
//                 .Append(_timeText.DOColor(_highlightColor, _duration * 0.25f))
//                 .Append(_timeText.DOColor(originalColor, _duration * 0.75f))
//                 .SetEase(Ease.InOutQuad)
//                 .OnComplete(() => _animationSequence = null);
//         }
//     }
// }