using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LayerEnchancer : MonoBehaviour {
    public int duplicateCounter = 1;
    public float distanceBetweenBaseLayers = 1.0f;
    //public float bottomLayerHeight = 0.0f;
    
    private void Awake() {
        List<Transform> layers = new List<Transform>();
        var childCount = transform.childCount;
        for (int i = 0; i < childCount; i++) {
            layers.Add(transform.GetChild(i));
        }
        
        Transform[,] duplicates = new Transform[layers.Count, duplicateCounter];

        var gap = distanceBetweenBaseLayers / (duplicateCounter + 1);
        
        for (int i = 0; i < layers.Count; i++) {
            float baseHeight = distanceBetweenBaseLayers * (i + 1);
            layers[i].transform.localPosition = new Vector3(0.0f, 0.0f, -baseHeight);
            layers[i].tag = "Original Layer";
            
            for (int j = 0; j < duplicateCounter; j++) {
                var dupHeight = baseHeight - (j + 1) * gap;
                
                var dup = Instantiate(layers[i], transform);
                dup.transform.localPosition = new Vector3(0.0f, 0.0f, -dupHeight);
                dup.tag = "Duplicate Layer";
                duplicates[i, j] = dup;
            }
        }
        
        foreach (var duplicate in duplicates) {
            layers.Add(duplicate);
        }
        
        //detach layers from parent to sort them
        foreach (var layer in layers) {
            layer.parent = null;
        }
        
        var sorted = layers.OrderByDescending(l => l.transform.localPosition.z);

        var index = 0;
        foreach (var layer in sorted) {
            layer.parent = transform;
            layer.SetSiblingIndex(index);
            index++;
        }
    }
}
