using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Layer : MonoBehaviour
{
    private Tilemap _tilemap;

    public bool doNotDuplicate;
    
    private Color topColor = Color.white;
    private Color bottomColor = new Color(0.2f, 0.2f, 0.2f, 1.0f);

    private void Start() {
        _tilemap = GetComponent<Tilemap>();
    }

    private void Update()
    {
        // ReSharper disable once Unity.InefficientPropertyAccess
        var shade = transform.GetSiblingIndex() / (float) transform.parent.childCount;

        var color = Color.Lerp(bottomColor, topColor, shade);

        _tilemap.color = color;
    }
}
