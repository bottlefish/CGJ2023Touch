using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static TouchSystem touchSystem;
    public static Camera cam;
    public static bool touching1;
    public static bool touching2;
    public static bool touching;
    public static float touchRadius = 5f;

    public static List<Vector2> touchPosList = new List<Vector2>();
    
    private void Awake()
    {
        if (touchSystem == null)
        {
            touchSystem = new TouchSystem();
        }

        if (cam == null)
        {
            cam = Camera.main;
        }

        //touchSystem.TouchPlay.TouchPosition1.started += StartTouch1;
        //touchSystem.TouchPlay.TouchPosition1.canceled += CanceledTouch1;

        //touchSystem.TouchPlay.TouchPosition2.started += StartTouch2;
        //touchSystem.TouchPlay.TouchPosition2.canceled += CanceledTouch2;
    }

    private void Start()
    {
        Input.multiTouchEnabled = true;
    }

    //private void CanceledTouch1(InputAction.CallbackContext context)
    //{
    //    Debug.Log("Start");
    //    touching1 = false;
    //}

    //private void StartTouch1(InputAction.CallbackContext context)
    //{
    //    touching1 = true;
    //}

    //private void CanceledTouch2(InputAction.CallbackContext context)
    //{
    //    touching2 = false;
    //}

    //private void StartTouch2(InputAction.CallbackContext context)
    //{
    //    touching2 = true;
    //}

    //private void OnEnable()
    //{
    //    touchSystem.Enable();
    //}

    //private void OnDisable()
    //{
    //    touchSystem.Disable();
    //}

    private void Update()
    {
        if (Input.touchCount <= 0)
        {
            Camera.main.GetComponent<AudioSource>().enabled = false;
            touchPosList.Clear();
            touching = false;
            return;
        }

        if (Input.touchCount > 0)
        {
            Camera.main.GetComponent<AudioSource>().enabled = true;
            touchPosList.Clear();
            touching = true;
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);
                touchPosList.Add(cam.ScreenToWorldPoint(touch.position));
            }
        }
    }

    public static List<Vector2> ReadPositionList()
    {
        //List<Vector2> res = new List<Vector2>();

        //Vector2 touchPos1 = touchSystem.TouchPlay.TouchPosition1.ReadValue<Vector2>();
        //if (touchPos1 != Vector2.zero)
        //{
        //    res.Add(cam.ScreenToWorldPoint(touchPos1));
        //}

        //Vector2 touchPos2 = touchSystem.TouchPlay.TouchPosition2.ReadValue<Vector2>();
        //if (touchPos2 != Vector2.zero)
        //{
        //    res.Add(cam.ScreenToWorldPoint(touchPos2));
        //}

        return touchPosList;
    }
}
