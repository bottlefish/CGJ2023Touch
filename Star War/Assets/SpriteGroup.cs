using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpriteGroup
{
    public SpriteRenderer[] sprites;

    public void SetAlpha(float alpha)
    {
        foreach (var sprite in sprites)
        {
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, alpha);
        }
    }
}


