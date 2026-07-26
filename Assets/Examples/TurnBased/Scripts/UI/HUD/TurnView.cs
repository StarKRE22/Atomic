using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Game.UI
{
    public sealed class TurnView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _caption;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Color _playerTurnColor;
        [SerializeField] private Color _enemyTurnColor;

        public async UniTaskVoid AnimatePlayerTurn()
        {
            this.SetPlayerTurn(true);
            this.SetCaption("your turn!");
            
            gameObject.SetActive(true);
            await this.Enable().AsyncWaitForCompletion();
            await UniTask.Delay(TimeSpan.FromSeconds(0.75f));
            await this.Disable().AsyncWaitForCompletion();
        }

        public async UniTask AnimateEnemyTurn()
        {
            this.SetPlayerTurn(false);
            this.SetCaption("enemy turn!");
            gameObject.SetActive(true);
            await this.Enable().AsyncWaitForCompletion();
            await UniTask.Delay(TimeSpan.FromSeconds(0.75f));
            await this.Disable().AsyncWaitForCompletion();
        }
        
        private void SetPlayerTurn(bool value)
        {
            _caption.color = value ? _playerTurnColor: _enemyTurnColor;
        }

        private void SetCaption(string caption)
        {
            _caption.text = caption;
        }

        private Tween Enable()
        {
            _canvasGroup.alpha = 0f;
            return DOVirtual.Float(0f, 1f, 1f, value => _canvasGroup.alpha = value)
                .SetSpeedBased(true);
        }

        private Tween Disable()
        {
            return DOVirtual.Float(_canvasGroup.alpha, 0f, 1f, value => _canvasGroup.alpha = value)
                .SetSpeedBased(true)
                .OnComplete(() => gameObject.SetActive(false));
        }
    }
}