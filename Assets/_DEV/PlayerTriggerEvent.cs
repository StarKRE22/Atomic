using Atomic.Elements;
using UnityEngine;

public class PlayerTriggerEvent : MonoBehaviour
{
    [SerializeReference]
    public IAction _eventActions;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            // Invoke all actions configured in the inspector
            _eventActions.Invoke();
        }
    }
}

