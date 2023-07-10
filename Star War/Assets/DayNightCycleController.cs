using System.Collections;
using UnityEngine;

using System.Collections;
using UnityEngine;

using System.Collections;
using UnityEngine;

public class DayNightCycleController : MonoBehaviour
{
    public SpriteRenderer sun, sunBackground, moon, moonBackground;
    private bool isDay = true;
    private Coroutine cycleRoutine;
    public AudioSource cycleAudio;

    public bool isCycleBeingTouched { get; private set; } = false;

    private void Start()
    {
        sun.color = sunBackground.color = new Color(1, 1, 1, 1);
        moon.color = moonBackground.color = new Color(1, 1, 1, 0);

        if (cycleAudio != null)
        {
            cycleAudio.loop = true;
        }
    }

    void Update()
    {
        isCycleBeingTouched = false;

        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
            RaycastHit2D hit = Physics2D.Raycast(touchPos, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                isCycleBeingTouched = true;

                if (cycleAudio != null && !cycleAudio.isPlaying)
                {
                    cycleAudio.Play();
                }

                if (cycleRoutine != null)
                    continue;

                cycleRoutine = StartCoroutine(TransitionDayNight());
            }
        }

        if (!isCycleBeingTouched && cycleAudio != null && cycleAudio.isPlaying)
        {
            cycleAudio.Stop();
        }
    }

    IEnumerator TransitionDayNight()
    {
        float duration = 1.5f;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);

            if (isDay)
            {
                var targetAlpha = Mathf.Lerp(1, 0, t);
                sun.color = sunBackground.color = new Color(1, 1, 1, targetAlpha);
                moon.color = moonBackground.color = new Color(1, 1, 1, 1 - targetAlpha);
            }
            else
            {
                var targetAlpha = Mathf.Lerp(0, 1, t);
                sun.color = sunBackground.color = new Color(1, 1, 1, targetAlpha);
                moon.color = moonBackground.color = new Color(1, 1, 1, 1 - targetAlpha);
            }

            yield return null;
        }

        isDay = !isDay;
        cycleRoutine = null;
    }
}