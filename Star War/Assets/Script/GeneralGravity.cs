using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralGravity : MonoBehaviour
{
    private Rigidbody2D rb;
    public float radius;
    public bool activated;

    public static float g = 1.3f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public Vector2 CalcGrivity(float sourceMass, Vector2 sourcePosition)
    {
        // λ�ò�
        Vector2 offset = sourcePosition - rb.position;
        // ����
        Vector2 dir = offset.normalized;
        // ����ƽ��
        float distance = offset.magnitude;
        // ����
        float gravity = (sourceMass * rb.mass * g) / distance;

        return dir * gravity;
    }

    public void AddGravity(Vector2 force)
    {
        if (activated)
        {
            rb.AddForce(force);
        }
    }

    private void Update()
    {
        if (InputManager.touching)
        {
            List<Vector2> touchPosList = InputManager.ReadPositionList();
            for (int i = 0; i < touchPosList.Count; i++)
            {
                float sqrDistance = (touchPosList[i] - rb.position).sqrMagnitude;
                if (sqrDistance < totalSqrRadius)
                {
                    if(gameObject.name=="Ship")
                    {
                        gameObject.GetComponent<AudioSource>().enabled = true;
                    }
                    if (gameObject.name == "BlackHole")
                    {
                        gameObject.GetComponent<AudioSource>().enabled = true;
                    }
                    if (gameObject.name == "TargetStar")
                    {
                        gameObject.GetComponent<AudioSource>().enabled = true;
                    }
                    activated = true;
                    break;
                }
                else
                {
                    if (gameObject.name == "Ship")
                    {
                        gameObject.GetComponent<AudioSource>().enabled = false;
                    }
                    if (gameObject.name == "BlackHole")
                    {
                        gameObject.GetComponent<AudioSource>().enabled = false;

                    }
                    if (gameObject.name == "TargetStar")
                    {
                        gameObject.GetComponent<AudioSource>().enabled = false;
                    }
                    activated = false;
                }
            }
        }
        else
        {
            if (gameObject.name == "Ship")
            {
                gameObject.GetComponent<AudioSource>().enabled = false;
            }
            if (gameObject.name == "BlackHole")
            {
                gameObject.GetComponent<AudioSource>().enabled = false;
            }
            if (gameObject.name == "TargetStar")
            {
                gameObject.GetComponent<AudioSource>().enabled = false;
            }
            activated = false;
        }

        if (gameObject.tag != "Stone" && gameObject.name != "Ship")
        {
            return;
        }

        if (!activated)
        {
            rb.velocity = Vector3.zero;
            rb.rotation = 0f;
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
