using UnityEngine;
using System.Collections;

public class RotateObject : MonoBehaviour
{
    private float rotationSpeed = 90000f;
    private float deceleration = 0.5f;

    private Vector2 _lastPos;
    private Vector2 _rotationVelocity;
    private bool _pressedObject;
    private bool _snapping;
    private bool _wasDragged; 
    
    private float x, y, z;

    public System.Action OnDiceStopped;

    void Update()
    {
#if UNITY_EDITOR
        if (_pressedObject && Input.GetMouseButton(0))
        {
            Vector2 mousePos = Input.mousePosition;
            Vector2 delta = (mousePos - _lastPos) / Screen.width;
            _rotationVelocity = delta * rotationSpeed;
            _lastPos = mousePos;
            if(_rotationVelocity.magnitude > 10) _wasDragged = true; 
        }
#else
        if (_pressedObject && Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                Vector2 delta = touch.deltaPosition / Screen.width;
                _rotationVelocity = delta * rotationSpeed;
                if(_rotationVelocity.magnitude > 10) _wasDragged = true;
            }
        }
#endif

        if (_rotationVelocity.magnitude > 100f) 
        {
            transform.Rotate(Vector3.up, -_rotationVelocity.x * Time.deltaTime, Space.World);
            transform.Rotate(Vector3.right, _rotationVelocity.y * Time.deltaTime, Space.World);
            _rotationVelocity = Vector2.Lerp(_rotationVelocity, Vector2.zero, deceleration * Time.deltaTime);
        }
        else if (!_pressedObject && !_snapping && _wasDragged)
        {
            _snapping = true;
            _wasDragged = false; 
            StartCoroutine(SnapToFace());
        }

        if (Input.GetMouseButtonUp(0))
            _pressedObject = false;
    }

    void OnMouseDown()
    {
        _pressedObject = true;
        _snapping = false; 
        _wasDragged = false;
        _lastPos = Input.mousePosition;
    }

    IEnumerator SnapToFace()
    {
        Quaternion start = transform.rotation;
        Vector3 angles = transform.eulerAngles;

        x = Mathf.Round(angles.x / 90f) * 90f;
        y = Mathf.Round(angles.y / 90f) * 90f;
        z = Mathf.Round(angles.z / 90f) * 90f;

        Quaternion target = Quaternion.Euler(x, y, z);

        float t = 0;
        while (t < 1.0f) 
        {
            t += Time.deltaTime * 5f;  
            transform.rotation = Quaternion.Slerp(start, target, t);
            yield return null;
        }

        transform.rotation = target; 
        _snapping = false;
        
        OnDiceStopped?.Invoke();
    }

    public int getFace()
    {
        Vector3[] localAxes = new Vector3[]
        {
            transform.up,       // 0: +Y
            -transform.up,      // 1: -Y
            transform.right,    // 2: +X
            -transform.right,   // 3: -X
            transform.forward,  // 4: +Z
            -transform.forward  // 5: -Z
        };

        string[] axisNames = { "+Y (Up)", "-Y (Down)", "+X (Right)", "-X (Left)", "+Z (Forward)", "-Z (Back)" };

        Vector3 directionToCamera = (Camera.main.transform.position - transform.position).normalized;

        float maxDot = -2f; 
        int bestAxisIndex = 0;

        for (int i = 0; i < 6; i++)
        {
            float dot = Vector3.Dot(localAxes[i], directionToCamera);
            
            if (dot > maxDot)
            {
                maxDot = dot;
                bestAxisIndex = i;
            }
        }

        Debug.Log($"<color=yellow> {axisNames[bestAxisIndex]} (: {bestAxisIndex})</color>");

        int diceValue = 0;

        switch (bestAxisIndex)
        {
            case 0: 
                diceValue = 5;
                break;
            case 1: 
                diceValue = 2; 
                break;
            case 2: 
                diceValue = 4; 
                break;
            case 3: 
                diceValue = 3; 
                break;
            case 4: 
                diceValue = 1; 
                break;
            case 5: 
                diceValue = 6; 
                break;
        }

        Debug.Log("<color=green> " + diceValue + "</color>");
        return diceValue;
    }
}


