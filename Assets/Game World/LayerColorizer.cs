using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LayerColorizer : MonoBehaviour
{
    private Tilemap _tilemap;

    private void Start()
    {
        _tilemap = GetComponent<Tilemap>();

        // ReSharper disable once Unity.InefficientPropertyAccess
        var shade = transform.GetSiblingIndex() / (float) transform.parent.childCount;

        var color = Color.Lerp(Color.gray, Color.white, shade);

        _tilemap.color = color;    
    }
}
