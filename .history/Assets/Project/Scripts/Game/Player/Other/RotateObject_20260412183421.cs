
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    private float rotationSpeed = 90000f;
    private float deceleration = 1f;

    private Vector2 _lastPos;
    private Vector2 _rotationVelocity;
    private bool _pressedObject;
    private bool _snapping;

    void Update()
    {
#if UNITY_EDITOR
        if (_pressedObject && Input.GetMouseButton(0))
        {
            Vector2 mousePos = Input.mousePosition;
            Vector2 delta = (mousePos - _lastPos) / Screen.width;
            _rotationVelocity = delta * rotationSpeed;
            _lastPos = mousePos;
        }
#else
        if (_pressedObject && Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                Vector2 delta = touch.deltaPosition / Screen.width;
                _rotationVelocity = delta * rotationSpeed;
            }
        }
#endif

        if (_rotationVelocity.magnitude > 0.7f)
        {
            transform.Rotate(Vector3.up, -_rotationVelocity.x * Time.deltaTime, Space.World);
            transform.Rotate(Vector3.right, _rotationVelocity.y * Time.deltaTime, Space.World);

            _rotationVelocity = Vector2.Lerp(_rotationVelocity, Vector2.zero, deceleration * Time.deltaTime);
        }
        else if (!_pressedObject && !_snapping)
        {
           
            _snapping = true;
            StartCoroutine(SnapToFace());
        }

        if (Input.GetMouseButtonUp(0))
            _pressedObject = false;
    }

    void OnMouseDown()
    {
        _pressedObject = true;
        _snapping = false; 
        _lastPos = Input.mousePosition;
    }

    // --- Snap Function ---
    System.Collections.IEnumerator SnapToFace()
    {
        Quaternion start = transform.rotation;

        Vector3 angles = transform.eulerAngles;

        float x = Mathf.Round(angles.x / 90f) * 90f;
        float y = Mathf.Round(angles.y / 90f) * 90f;
        float z = Mathf.Round(angles.z / 90f) * 90f;

        Quaternion target = Quaternion.Euler(x, y, z);

        float t = 0;
        while (t < 0.5f)
        {
            t += Time.deltaTime * 100f;  
            transform.rotation = Quaternion.Slerp(start, target, t);
            yield return null;
        }

        _snapping = false;
    }
}
