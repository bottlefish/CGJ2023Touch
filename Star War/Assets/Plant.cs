using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public Transform sprite1, sprite2;
    public float speed = 1f; // The speed of movement
    public float maxHeight = 10f; // The max height the sprites will reach
    public float minHeight = 0f; // The minimum height (usually the initial height)

    private bool movingUp = true; // Track if the sprites are moving up or down
    private bool moving = false; // Track if the plant is currently moving
    public Transform attachPoint;
    //public GameObject growsound;
    

    public void ReactToTouch()
    {
        // The plant starts moving when reacted to a touch
        moving = true;
        UpdatePlant();
    }

    public void StopMovement()
    {
        // The plant stops moving when the user releases the touch
        moving = false;
        //growsound.GetComponent<AudioSource>().enabled = false;
    }

    private void UpdatePlant()
    {
        if (!moving)
        {
            return;
        }

        //growsound.GetComponent<AudioSource>().enabled = true;

        float moveDistance = speed * Time.deltaTime;
        if (movingUp)
        {
            if (sprite1.position.y >= maxHeight)
            {
                movingUp = false;
            }
            else
            {
                // Move the sprites up
                sprite1.position += new Vector3(0, moveDistance, 0);
                sprite2.position += new Vector3(0, moveDistance, 0);
            }
        }
        else
        {
            if (sprite1.position.y <= minHeight)
            {
                movingUp = true;
            }
            else
            {
                // Move the sprites down
                sprite1.position -= new Vector3(0, moveDistance, 0);
                sprite2.position -= new Vector3(0, moveDistance, 0);
            }
        }
    }
}