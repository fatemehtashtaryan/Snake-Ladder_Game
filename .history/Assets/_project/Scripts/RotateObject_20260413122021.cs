
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    private float rotationSpeed = 90000f;
    private float deceleration = 0.5f;

    private Vector2 _lastPos;
    private Vector2 _rotationVelocity;
    private bool _pressedObject;
    private bool _snapping;
    private float x;
    private float y;
    private float z;

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

        if (_rotationVelocity.magnitude > 1000f)
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

        x = Mathf.Round(angles.x / 90f) * 90f;
        y = Mathf.Round(angles.y / 90f) * 90f;
        z = Mathf.Round(angles.z / 90f) * 90f;

        Quaternion target = Quaternion.Euler(x, y, z);

        float t = 0;
        while (t < 0.5f)
        {
            t += Time.deltaTime * 10f;  
            transform.rotation = Quaternion.Slerp(start, target, t);
            yield return null;
        }

        _snapping = false;
        OnDiceStopped?.Invoke();
    }
    public int getFace(){

        int h = 0;
        if(x==0f && y == 270f && z==0f){
            h= 1;
        }
        else if(x==9f && y == 180f && z==0f){
            h= 2;
        }else if(x==0f && y == 180f && (z ==0f || z ==180f)){
            h= 3;
        }else if(x==0f && y == 0f && z==0f){
            h= 4;
        }else if(x==0f && y == 90f && z==90f){
            h= 5;
        }else if(x==0f && y == 90f && z==0f){
            h= 6;
        }else if(x==0f && y == -90f && z==0f){
            h= 1;
        }
        else if(x==-90f && y == 0f && z==90f){
            h= 6;
        }
        Debug.Log(x );
        Debug.Log(y );
        Debug.Log(z );
        return h;
    }
    void OnMouseEnter()
    {
        Debug.Log("Mouse entered object");
    }
}
