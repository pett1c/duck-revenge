using System.Collections;
using System.Collections.Generic;
using System.Reflection;
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
    private Transform joystickTransform, stickTransform;

    private Vector2 screenResolution;
    private float screenRatio;

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
        Vector2 localTouchPosition, joystickPosition;

        localTouchPosition = Input.mousePosition / screenResolution * cameraSize * 2 - new Vector2(cameraSize, cameraSize);
        localTouchPosition *= new Vector2(screenRatio, 1);
        joystickPosition = new Vector2(joystickTransform.position.x, joystickTransform.position.y);

        bool isTouchOutOfBounds = (localTouchPosition - joystickPosition).magnitude > stickSize;

        localStickPosition = Vector2.zero;

        if (!Input.GetMouseButton(0) || (!stickWasTouched && isTouchOutOfBounds)) 
        {
            stickTransform.position = joystickPosition;
            stickWasTouched = false;
            return;
        }

        localStickPosition = (localTouchPosition - joystickPosition) / stickSize;

        if (stickWasTouched && isTouchOutOfBounds)
        {
            localStickPosition = localStickPosition.normalized;
            localTouchPosition = joystickPosition + localStickPosition * stickSize;
        }

        stickTransform.position = localTouchPosition;
        stickWasTouched = true;
    }
}
