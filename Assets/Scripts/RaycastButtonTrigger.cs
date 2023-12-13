using UnityEngine;
using UnityEngine.EventSystems;
using Oculus;
using UnityEngine.UI;

public class RaycastToButton : MonoBehaviour
{
    [SerializeField] private Camera _cameraRig;
    [SerializeField] private float _maxDistance = 100f;

    private void Update()
    {
        if (Physics.Raycast(transform.position,transform.forward, out RaycastHit hit, _maxDistance))
        {
            if (hit.collider.TryGetComponent(out Button button))
            {
                button.onClick.Invoke();
            }
        }
    }
}
