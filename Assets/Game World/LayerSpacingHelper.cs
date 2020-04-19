using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LayerSpacingHelper : MonoBehaviour
{
    public float distanceBetweenLayers = 0.25f;
    public bool spaceLayers = false;

    private void AdjustSpacingForChild(Transform child, float offset = 0.0f)
    {
        for (var i = 0; i < child.childCount; i++)
        {
            var layer = child.GetChild(i);
            var height = distanceBetweenLayers * (i + 1);
            layer.localPosition = new Vector3(0, 0, -height - offset);
        }
    }
    
    private void OnValidate()
    {
        Debug.Log("Layers have been adjusted.");

        //AdjustSpacingForChild(transform.GetChild(0));
        //AdjustSpacingForChild(transform.GetChild(1), 0.05f);
        
        spaceLayers = false;
    }
}
