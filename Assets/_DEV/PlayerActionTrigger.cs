using Atomic.Elements;
using UnityEngine;

public sealed class PlayerActionTrigger : MonoBehaviour
{
    private const string PlayerTag = "Player";
    
    [SerializeReference] private IAction _action;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag(PlayerTag))
            _action.Invoke();
    }
}