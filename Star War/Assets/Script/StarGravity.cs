using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StarGravity : MonoBehaviour
{ 
    public bool enable;
    public float radius;
    public float mass;
    private RbCollector rbCollector;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        GameObject rbCollectorObj = GameObject.Find("RbCollector");
        rbCollector = rbCollectorObj.GetComponent<RbCollector>();
    }

    private void Update()
    {
        //if (gameObject.name == "BlackHole")
        //{
        //    rbCollector.AddGrivity(mass, rb.position, name);
        //    return;
        //}

        CheckTouch();

        //如果是可用的，则持续向引力范围内物体施加力
        if (enable)
        {
            rbCollector.AddGrivity(mass, rb.position, name);
        }
    }

    private void CheckTouch()
    {
        if (InputManager.touching)
        {
            List<Vector2> touchPosList = InputManager.ReadPositionList();
            for (int i = 0; i < touchPosList.Count; i++)
            {
                float sqrDistance = (touchPosList[i] - rb.position).sqrMagnitude;
                if (sqrDistance < totalSqrRadius)
                {
                    enable = true;
                    break;
                }
                else
                {
                    enable = false;
                }
            }
        }
        else
        {
            enable = false;
        }
    }

    private float totalSqrRadius
    {
        get
        {
            return Mathf.Sqrt(radius + InputManager.touchRadius);
        }
    }
}
