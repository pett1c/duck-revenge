using UnityEngine;

public class Joystick : MonoBehaviour
{
    [SerializeField] public Vector2 localStickPosition;

    [Header("Prefabs")]
    [SerializeField] private GameObject stick;

    [Header("Parameters")]
    [SerializeField] private float stickSize;
    [SerializeField] private float cameraSize;

    [Header("Other")]
    private Transform stickTransform;
    private Transform joystickTransform;

    private float screenRatio;
    private Vector2 screenResolution;

    private bool stickWasTouched = false;

    private void Start()
    {
        joystickTransform = gameObject.transform;
        stickTransform = stick.transform;

        screenRatio = (float)Screen.currentResolution.width / (float)Screen.currentResolution.height;
        screenResolution = new Vector2(Screen.currentResolution.width, Screen.currentResolution.height);
    }

    private void Update()
    {
        Vector2 joystickPosition, localTouchPosition;

        joystickPosition = new Vector2(joystickTransform.localPosition.x, joystickTransform.localPosition.y);

        localTouchPosition = Input.mousePosition / screenResolution * cameraSize * 2 - new Vector2(cameraSize, cameraSize);
        localTouchPosition = (localTouchPosition * new Vector2(screenRatio, 1) - joystickPosition) / 2;

        bool isTouchOutOfBounds = localTouchPosition.magnitude > stickSize;

        localStickPosition = Vector2.zero;

        if (!Input.GetMouseButton(0) || (!stickWasTouched && isTouchOutOfBounds)) 
        {
            stickTransform.localPosition = Vector2.zero;
            stickWasTouched = false;
            return;
        }

        localStickPosition = localTouchPosition / stickSize;

        if (stickWasTouched && isTouchOutOfBounds)
        {
            localStickPosition = localStickPosition.normalized;
            localTouchPosition = localStickPosition * stickSize;
        }

        stickTransform.localPosition = localTouchPosition;
        stickWasTouched = true;
    }
}