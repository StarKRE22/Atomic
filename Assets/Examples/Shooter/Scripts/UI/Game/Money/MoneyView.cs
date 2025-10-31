// using DG.Tweening;
// using TMPro;
// using UnityEngine;
//
// namespace BeginnerGame
// {
//     public sealed class MoneyView : MonoBehaviour
//     {
//         [SerializeField]
//         private TMP_Text _valueText;
//
//         [Header("Animation")]
//         [SerializeField]
//         private float _bounceScale = 1.2f;
//
//         [SerializeField]
//         private float _bounceDuration = 0.2f;
//
//         private Tween _bounceTween;
//
//         public void SetMoney(string money)
//         {
//             _valueText.text = money;
//         }
//
//         public void ChangeMoney(string money)
//         {
//             this.SetMoney(money);
//             this.AnimateBounce();
//         }
//
//         private void AnimateBounce()
//         {
//             _bounceTween?.Kill();
//
//             Vector3 originalScale = _valueText.rectTransform.localScale;
//
//             _bounceTween = _valueText.rectTransform
//                 .DOScale(originalScale * _bounceScale, _bounceDuration)
//                 .SetEase(Ease.OutQuad)
//                 .OnComplete(() =>
//                 {
//                     _valueText.rectTransform
//                         .DOScale(originalScale, _bounceDuration)
//                         .SetEase(Ease.OutBounce);
//                 });
//         }
//     }
// }/