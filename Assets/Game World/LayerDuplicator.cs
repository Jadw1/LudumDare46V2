﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerDuplicator : MonoBehaviour
{
    public int count = 3;

    private void Awake()
    {
        // get all layers
        var layersParent = transform.GetChild(0);
        List<Transform> layers = new List<Transform>();

        // Without this an infinite loop awaits
        var childCount = layersParent.childCount;
        for (var i = 0; i < childCount; i++)
        {
            layers.Add(layersParent.GetChild(i));
        }

        foreach (var layer in layers) {
            layer.SetSiblingIndex(layer.GetSiblingIndex() * (count + 1) + count);
        }

        // calculate distance between two first layers
        var distance = Mathf.Abs(layers[0].localPosition.z - layers[1].localPosition.z);
        
        // calulate gap between new layers for correct layer count
        var gap = distance / (count + 1);
        
        for (int i = 0; i < childCount; i++)
        {
            var layer = layers[i];
            var height = distance * (i);
            var layerIndex = layer.GetSiblingIndex();

            for (int j = 1; j <= count; j++)
            {
                var adjustedHeight = height - j * gap;

                var newLayer = Instantiate(layer, layersParent);
                newLayer.SetSiblingIndex(layerIndex - j);
                newLayer.name = layer.name + "_" + j;
                newLayer.tag = "Duplicate Layer";
                newLayer.transform.localPosition = new Vector3(0, 0, -adjustedHeight);
            }
            
            layer.localPosition = new Vector3(0, 0, -height);
        }
    }
}