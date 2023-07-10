using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperController : MonoBehaviour
{
    public Material PaperMaterial;
    private Vector2 _mousePosition;
    private float _alphaThreshold = 0.0f;
    private float _rate = 0.01f;

    private void Update()
    {
        if(Input.GetMouseButton(0))
        {
            _mousePosition = Input.mousePosition;
            _mousePosition.x /= Screen.width;
            _mousePosition.y /= Screen.height;
            _alphaThreshold = Mathf.Min(1, _alphaThreshold + _rate);
        }
        else
        {
            _alphaThreshold = Mathf.Max(0, _alphaThreshold - _rate);
        }

        PaperMaterial.SetVector("_MousePosition", _mousePosition);
        PaperMaterial.SetFloat("Scale", _alphaThreshold);
    }
}
