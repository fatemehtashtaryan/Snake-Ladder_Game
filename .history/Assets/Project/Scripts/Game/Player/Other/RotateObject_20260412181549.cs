using UnityEngine;

public class RotateObject : MonoBehaviour
{
    private float rotationSpeed = 900f; // sensitivity
    private float deceleration = 400f; // how fast it slows down

    private Vector2 _lastPos;
    private Vector2 _rotationVelocity;
    private bool _pressedObject;

    void Update()
    {
#if UNITY_EDITOR
        if (_pressedObject && Input.GetMouseButton(0))
        {
            Vector2 mousePos = Input.mousePosition;
            Vector2 delta = (mousePos - _lastPos) / Screen.width; // normalized
            _rotationVelocity = delta * rotationSpeed; // store current speed
            _lastPos = mousePos;
        }
#else
        if (_pressedObject && Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                Vector2 delta = touch.deltaPosition / Screen.width; // normalize
                _rotationVelocity = delta * rotationSpeed;
            }
        }
#endif

        // Apply rotation based on velocity
        transform.Rotate(Vector3.up, -_rotationVelocity.x * Time.deltaTime, Space.World);
        transform.Rotate(Vector3.right, _rotationVelocity.y * Time.deltaTime, Space.World);

        // Decelerate over time
        _rotationVelocity = Vector2.Lerp(_rotationVelocity, Vector2.zero, deceleration * Time.deltaTime);

        // Release check
        if (Input.GetMouseButtonUp(0))
            _pressedObject = false;
    }

    void OnMouseDown()
    {
        _pressedObject = true;
        _lastPos = Input.mousePosition;
    }
}
