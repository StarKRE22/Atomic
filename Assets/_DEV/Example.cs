using Atomic.Elements;
using UnityEngine;

public class Example : MonoBehaviour
{
    [SerializeField]
    private AnimationEvents _animEvents;

    private void OnEnable()
    {
        _animEvents.Subscribe("Hello", OnHello);
        _animEvents.OnEvent += OnAnimationEvent;
    }

    private void OnDisable()
    {
        _animEvents.Unsubscribe("Hello", OnHello);
        _animEvents.OnEvent -= OnAnimationEvent;
    }

    private void OnHello() => Debug.Log("Hello!");
    
    private void OnAnimationEvent(string evt) => Debug.Log($"Event triggered: {evt}");
}