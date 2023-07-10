using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevealOnClick : MonoBehaviour 
{
    public float radius = 5f;
    public LayerMask revealLayerMask;
    public float revealSpeed = 0.1f;
    public float hideSpeed = 0.1f;

    private bool fadeOnRelease = false;
    private List<SpriteRenderer> revealedRenderers = new List<SpriteRenderer>();
    private Vector2 previousMousePosition;

    void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButton(0))
        {
            fadeOnRelease = false;
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(mousePosition, radius, revealLayerMask);

            foreach (var hitCollider in hitColliders)
            {
                SpriteRenderer sr = hitCollider.GetComponent<SpriteRenderer>();
                if (sr != null && !revealedRenderers.Contains(sr))
                {
                    revealedRenderers.Add(sr);
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            fadeOnRelease = true;
        }

        List<SpriteRenderer> spritesToRemove = new List<SpriteRenderer>();

        foreach (var renderer in revealedRenderers)
        {
            if (fadeOnRelease || !IsWithinRadius(renderer.gameObject, mousePosition, radius))
            {
                ChangeTransparency(renderer, -hideSpeed);
            }
            else
            {
                ChangeTransparency(renderer, revealSpeed);
            }

            if (renderer.color.a <= 0)
            {
                spritesToRemove.Add(renderer);
            }
        }

        foreach (var sprite in spritesToRemove)
        {
            revealedRenderers.Remove(sprite);
        }
        
        previousMousePosition = mousePosition;
    }

    bool IsWithinRadius(GameObject obj, Vector2 position, float radius)
    {
        return Vector2.Distance(obj.transform.position, position) <= radius;
    }

    void ChangeTransparency(SpriteRenderer renderer, float change)
    {
        Color color = renderer.color;
        color.a = Mathf.Clamp(color.a + change * Time.deltaTime, 0, 1);
        renderer.color = color;
    }
}