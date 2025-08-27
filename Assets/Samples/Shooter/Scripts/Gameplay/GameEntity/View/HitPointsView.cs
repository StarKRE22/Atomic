using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace ShooterGame.Gameplay
{
    public sealed class HitPointsView : MonoBehaviour
    {
        [SerializeField]
        private Image _progress;

        [SerializeField]
        private Text _text;

        [SerializeField]
        private float _showTime = 2;

        public void SetText(string text)
        {
            _text.text = text;
        }

        public void SetProgress(float progress)
        {
            _progress.fillAmount = progress;
        }

        public void SetColor(Color color)
        {
            _progress.color = color;
        }

        public void Show()
        {
            this.gameObject.SetActive(true);
            this.StartCoroutine(this.HideWithDelay());
        }

        public void Hide()
        {
            this.gameObject.SetActive(false);
        }

        private IEnumerator HideWithDelay()
        {
            yield return new WaitForSeconds(_showTime);
            this.gameObject.SetActive(false);
        }
    }
}