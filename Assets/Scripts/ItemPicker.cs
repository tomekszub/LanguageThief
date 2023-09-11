using System;
using UnityEngine;

public class ItemPicker : MonoBehaviour
{
    public Action<string> OnItemPickedUp;

    Camera _camera;

    void Awake() => _camera = Camera.main;

    void Update()
    {
        if(Input.GetMouseButtonUp(0))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                ObjectItem item = hit.transform.GetComponentInParent<ObjectItem>();

                if (item != null)
                {
                    OnItemPickedUp?.Invoke(item.Name);
                    Destroy(hit.transform.gameObject);
                }
            }
        }
    }
}
