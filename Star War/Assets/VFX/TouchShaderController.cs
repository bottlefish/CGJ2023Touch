using UnityEngine;
using System.Collections;

using UnityEngine;
using System.Collections;

using UnityEngine;
using System.Collections;

using UnityEngine;
using System.Collections;

using UnityEngine;
using System.Collections;

public class TouchShaderController : MonoBehaviour
{
    public Material paperMaterial;
    private Coroutine[] scaleUpRoutines = new Coroutine[2];
    private Coroutine[] scaleDownRoutines = new Coroutine[2];
    private bool[] touchActive = {false, false};

    private IEnumerator ScaleUp(string scaleName)
    {
        float scale = paperMaterial.GetFloat(scaleName);
        while (scale < 0.5f)
        {
            scale += Time.deltaTime;
            paperMaterial.SetFloat(scaleName, scale);
            yield return null;
        }
    }

    private IEnumerator ScaleDown(string scaleName)
    {
        float scale = paperMaterial.GetFloat(scaleName);
        while (scale > 0)
        {
            scale -= Time.deltaTime;
            paperMaterial.SetFloat(scaleName, scale);
            yield return null;
        }
    }

    void Start()
    {
        paperMaterial.SetVector("_TouchPoint1", Vector4.zero);
        paperMaterial.SetVector("_TouchPoint2", Vector4.zero);
        paperMaterial.SetFloat("_Scale1", 0);
        paperMaterial.SetFloat("_Scale2", 0);
    }

    void Update()
    {
        // Reset touchActive states
        touchActive[0] = false;
        touchActive[1] = false;

        // Check touch inputs
        for (int i = 0; i < Mathf.Min(2, Input.touchCount); i++)
        {
            Touch touch = Input.GetTouch(i);
            touchActive[i] = true;

            // Convert screen coordinates to target UV range
            float u = touch.position.x / Screen.width * 4.0f;
            float v = touch.position.y / Screen.height * 2.0f;

            // Set shader properties
            string touchPointName = "_TouchPoint" + (i + 1);
            paperMaterial.SetVector(touchPointName, new Vector4(u, v, 0, 0));

            // Start scaling up
            string scaleName = "_Scale" + (i + 1);
            if (scaleDownRoutines[i] != null)  // If ScaleDown coroutine is running
            {
                StopCoroutine(scaleDownRoutines[i]);  // Stop ScaleDown coroutine
            }
            if (scaleUpRoutines[i] != null)
            {
                StopCoroutine(scaleUpRoutines[i]);
            }
            scaleUpRoutines[i] = StartCoroutine(ScaleUp(scaleName));
        }

        // Check touchActive states
        for (int i = 0; i < 2; i++)
        {
            if (!touchActive[i])
            {
                // Start scaling down
                string scaleName = "_Scale" + (i + 1);
                if (scaleDownRoutines[i] != null)
                {
                    StopCoroutine(scaleDownRoutines[i]);
                }
                scaleDownRoutines[i] = StartCoroutine(ScaleDown(scaleName));
            }
        }
    }
}