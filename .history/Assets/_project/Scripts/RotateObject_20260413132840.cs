using UnityEngine;

public class RotateObject : MonoBehaviour
{
    private float rotationSpeed = 90000f;
    private float deceleration = 0.5f;

    private Vector2 _lastPos;
    private Vector2 _rotationVelocity;
    private bool _pressedObject;
    private bool _snapping;
    private float x, y, z; // اینها باید در سطح کلاس باشند چون در SnapToFace مقداردهی می‌شوند و در getFace استفاده می‌شوند

    public System.Action OnDiceStopped;
    private bool isRotating = false; // برای کنترل شروع و پایان چرخش

    void Update()
    {
        // شرایط اولیه برای شروع چرخش با کلیک/لمس
        if (_pressedObject && Input.GetMouseButton(0) && !isRotating)
        {
            isRotating = true; // شروع چرخش
            _snapping = false; // اطمینان از غیرفعال بودن اسنپینگ در حین چرخش
            _rotationVelocity = Vector2.zero; // ریست کردن سرعت در هر بار کلیک جدید
        }

#if UNITY_EDITOR
        if (_pressedObject && Input.GetMouseButton(0) && isRotating)
        {
            Vector2 mousePos = Input.mousePosition;
            Vector2 delta = (mousePos - _lastPos) / Screen.width;
            _rotationVelocity = delta * rotationSpeed;
            _lastPos = mousePos;
        }
#else
        if (_pressedObject && Input.touchCount > 0 && isRotating)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                Vector2 delta = touch.deltaPosition / Screen.width;
                _rotationVelocity = delta * rotationSpeed;
            }
        }
#endif

        // اگر سرعت چرخش هنوز قابل توجه است، تاس را بچرخان
        if (_rotationVelocity.magnitude > 1000f && isRotating)
        {
            transform.Rotate(Vector3.up, -_rotationVelocity.x * Time.deltaTime, Space.World);
            transform.Rotate(Vector3.right, _rotationVelocity.y * Time.deltaTime, Space.World);

            _rotationVelocity = Vector2.Lerp(_rotationVelocity, Vector2.zero, deceleration * Time.deltaTime);
        }
        // اگر تاس در حال چرخش بود ولی سرعتش خیلی کم شده و هنوز متوقف نشده
        else if (isRotating && _rotationVelocity.magnitude <= 1000f && !_snapping)
        {
            // سرعت به اندازه کافی کم شده، حالا شروع به اسنپ کردن کن
            _snapping = true;
            StartCoroutine(SnapToFace());
        }

        // اگر موس را رها کردیم، باید متوقف کنیم
        if (Input.GetMouseButtonUp(0))
        {
            _pressedObject = false;
            // اگر در حین چرخش موس را رها کردیم، اجازه بده به سمت توقف برود
            if (isRotating && !_snapping)
            {
                 _snapping = true; // فعال کردن اسنپینگ برای توقف
                 StartCoroutine(SnapToFace());
            }
        }
#if UNITY_EDITOR
        else if (Input.GetMouseButtonUp(0)) // برای اطمینان بیشتر در صورت استفاده از Editor
        {
             _pressedObject = false;
        }
#endif
    }

    void OnMouseDown()
    {
        // فقط اگر تاس در حال چرخش نیست، اجازه شروع چرخش جدید بده
        if (!isRotating)
        {
            _pressedObject = true;
            _lastPos = Input.mousePosition;
            // reset values for new roll
            _rotationVelocity = Vector2.zero; 
            isRotating = false; // اطمینان از اینکه در وضعیت آماده به چرخش است
            _snapping = false;
        }
    }

    // --- Snap Function ---
    System.Collections.IEnumerator SnapToFace()
    {
        // اطمینان حاصل کن که فقط یک بار در هر توقف صدا زده شود
        if (_snapping)
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

            _snapping = false; // اسنپینگ تمام شد
            isRotating = false; // دیگر در حال چرخش نیست
            OnDiceStopped?.Invoke(); // حالا که تاس کاملا متوقف شده، رویداد را صدا بزن
        }
    }

    public int getFace()
    {
        //Debug.Log($"x: {x}, y: {y}, z: {z}"); // برای دیباگ کردن مقادیر
        
        // مقادیر x, y, z باید به دقت بررسی شوند
        // فرض می‌کنیم که زاویه 90 درجه با محور Y رو به بالا است و 270 درجه رو به پایین
        // این منطق ممکن است نیاز به تنظیم دقیق تر بر اساس orientation تاس شما داشته باشد

        // مقادیر پیش‌فرض ممکن است با توجه به نحوه قرارگیری تاس در Unity کمی متفاوت باشند.
        // بهتر است یک بار تاس را بچرخانید و مقادیر x,y,z را لاگ بگیرید تا ببینید دقیقاً کدام مقادیر به کدام وجه تعلق دارند.
        // مثال: اگر رو به بالا 6 است، و رو به پایین 1 است.

        // این مثال بر اساس یک فرض رایج است:
        if (Mathf.Approximately(x, 0f) && Mathf.Approximately(y, 270f) && Mathf.Approximately(z, 0f)) return 1; // فرض: 1 رو به بالا
        if (Mathf.Approximately(x, 90f) && Mathf.Approximately(y, 180f) && Mathf.Approximately(z, 0f)) return 2; // فرض: 2 رو به جلو
        if (Mathf.Approximately(x, 0f) && Mathf.Approximately(y, 180f) && (Mathf.Approximately(z, 0f) || Mathf.Approximately(z, 180f))) return 3; // فرض: 3 رو به راست
        if (Mathf.Approximately(x, 0f) && Mathf.Approximately(y, 0f) && Mathf.Approximately(z, 0f)) return 4; // فرض: 4 رو به عقب
        if (Mathf.Approximately(x, 0f) && Mathf.Approximately(y, 90f) && Mathf.Approximately(z, 90f)) return 5; // فرض: 5 رو به چپ
        if (Mathf.Approximately(x, 0f) && Mathf.Approximately(y, 90f) && Mathf.Approximately(z, 0f)) return 6; // فرض: 6 رو به پایین

        // اگر هیچ کدام از موارد بالا نبود، یا منطق getFace نیاز به تنظیم دارد یا مقادیر x,y,z کمی متفاوت از انتظارات هستند
        // Debug.LogWarning("Could not determine dice face. Check angles: x=" + x + ", y=" + y + ", z=" + z);
        return 0; // یا یک مقدار پیش‌فرض دیگر
    }

    void OnMouseEnter()
    {
        //Debug.Log("Mouse entered object");
    }
}
