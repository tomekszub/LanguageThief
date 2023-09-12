using UnityEngine;
using UnityEngine.Events;

public class TriggerInteractionInvoker : MonoBehaviour
{
    [SerializeField] bool _SingleUse = true;
    [SerializeField] UnityEvent _OnEnter;

    void OnTriggerEnter(Collider _)
    {
        _OnEnter?.Invoke();

        if(_SingleUse)
            _OnEnter = null;
    }
}
