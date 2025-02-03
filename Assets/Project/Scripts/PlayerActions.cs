using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    [Header("Interact")]
    [SerializeField] private float distance;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1")) Interact();
    }

    private void Interact()
    {
        Vector2 _origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D _hit = Physics2D.Raycast(_origin, Vector2.zero);

        if (_hit)
        {
            GameObject _obj = _hit.collider.gameObject;

            if (Vector2.Distance(_obj.transform.position, gameObject.transform.position) <= distance)
            {
                if (_obj.TryGetComponent(out IInteractable _interactObj)) _interactObj.Interact();
            }
        }
    }
}