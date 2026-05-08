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
    private bool _wasDragged; // متغیر جدید برای چک کردن اینکه آیا تاس واقعا چرخیده یا نه
    
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
            if(_rotationVelocity.magnitude > 10) _wasDragged = true; // تشخیص شروع چرخش
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

        if (_rotationVelocity.magnitude > 100f) // مقدار حساسیت را کمی کمتر کردم
        {
            transform.Rotate(Vector3.up, -_rotationVelocity.x * Time.deltaTime, Space.World);
            transform.Rotate(Vector3.right, _rotationVelocity.y * Time.deltaTime, Space.World);
            _rotationVelocity = Vector2.Lerp(_rotationVelocity, Vector2.zero, deceleration * Time.deltaTime);
        }
        // فقط اگر قبلاً در حال چرخش بوده و الان سرعتش کم شده، اسنپ کن
        else if (!_pressedObject && !_snapping && _wasDragged)
        {
            _snapping = true;
            _wasDragged = false; // بلافاصله غیرفعال کن تا دوباره اجرا نشود
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
        while (t < 1.0f) // تا یک ادامه بده برای دقت بیشتر
        {
            t += Time.deltaTime * 5f;  
            transform.rotation = Quaternion.Slerp(start, target, t);
            yield return null;
        }

        transform.rotation = target; // اطمینان از عدد دقیق
        _snapping = false;
        
        // حالا که کاملا ایستاد، ایونت را صدا بزن
        OnDiceStopped?.Invoke();
    }

    public int getFace()
{
    // ۱. تعریف جهت‌های محلی تاس
    // نکته: این جهت‌ها بستگی به مدل سه‌بعدی شما دارد. 
    // اگر عددها اشتباه بود، فقط شماره‌های جلوی هر جهت را در این لیست عوض کنید.
    var directions = new (Vector3 dir, int value)[]
    {
        (transform.up, 2),         // جهت بالای تاس (محلی)
        (-transform.up, 5),        // جهت پایین تاس
        (transform.right, 6),      // جهت راست تاس
        (-transform.right, 1),     // جهت چپ تاس
        (transform.forward, 3),    // جهت جلوی تاس
        (-transform.forward, 4)    // جهت عقب تاس
    };

    float maxDot = -2f; // کمترین مقدار ممکن برای Dot Product منفی یک است
    int faceValue = 0;

    // ۲. مقایسه هر جهت با جهت آسمان (Vector3.up)
    foreach (var face in directions)
    {
        // حاصل‌ضرب داخلی نشان می‌دهد دو بردار چقدر هم‌جهت هستند
        // اگر ۱ باشد یعنی کاملاً هم‌جهت، اگر ۰ باشد یعنی عمود، اگر ۱- یعنی برعکس
        float dot = Vector3.Dot(face.dir, Vector3.up);

        if (dot > maxDot)
        {
            maxDot = dot;
            faceValue = face.value;
        }
    }

    Debug.Log("Face Detected: " + faceValue);
    return faceValue;
}

}
