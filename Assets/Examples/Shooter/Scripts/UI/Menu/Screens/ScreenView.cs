using UnityEngine;

namespace ShooterGame.UI
{
    public class ScreenView : MonoBehaviour
    {
        public virtual void Show()
        {
            this.gameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            this.gameObject.SetActive(false);
        }
    }
}