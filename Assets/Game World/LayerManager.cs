﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LayerManager : MonoBehaviour {
    
    private Tilemap[] mainLayers;
    private Tilemap[] allLayers;
    private Tilemap baseLayer;

    public float grayOutTo;

    private int duplicateCounter;
    
    
    void Start() {
        mainLayers = transform.GetComponentsInChildren<Tilemap>().Where(t => t.CompareTag("Original Layer")).ToArray();
        allLayers = transform.GetComponentsInChildren<Tilemap>();
        baseLayer = transform.parent.GetChild(1).GetComponent<Tilemap>();

        duplicateCounter = transform.GetComponent<LayerEnchancer>().duplicateCounter;
        
        //SetTopTileColor(-6, 9, Color.red);
    }

    private void Update() {
        
    }
    
    public int GetHeight(int x, int y) {
        return GetTopLayer(x, y) + 1;
    }

    public void SetTopTileColor(int x, int y, Color color) {
        var layer = GetTopLayer(x, y);

        var pos = new Vector3Int(x, y, 0);

        int layersCount = (layer + 1) * (1 + duplicateCounter) + 1;

        for (int i = layersCount - 2; i >= 0; i++) {
            float r = LerpColorComponent(grayOutTo, color.r, ((float) (i + 2)) / layersCount);
            float g = LerpColorComponent(grayOutTo, color.g, ((float) (i + 2)) / layersCount);
            float b = LerpColorComponent(grayOutTo, color.b, ((float) (i + 2)) / layersCount);
            
            SetTileColor(pos, i, new Color(r, g, b));
        }
        
        float rb = LerpColorComponent(grayOutTo, color.r, ((float) 1) / layersCount);
        float gb = LerpColorComponent(grayOutTo, color.g, ((float) 1) / layersCount);
        float bb = LerpColorComponent(grayOutTo, color.b, ((float) 1) / layersCount);
        SetTileColor(pos, -1, new Color(rb, gb,bb));
        
    }

    private float LerpColorComponent(float to, float max, float t) {
        if (max < to) {
            return max;
        }

        return Mathf.Lerp(to, max, t);
    }
    
    //returns index of original layer, -1 if it's base layer
    private int GetTopLayer(int x, int y) {
        Vector3Int pos = new Vector3Int(x, y, 0);
        
        for (int i = mainLayers.Length - 1; i >= 0; i++) {
            var tile = mainLayers[i].GetTile(pos);
            if (tile) {
                return i;
            }
        }
        
        return -1;
    }
    
    //layer is index in allLayers array, negative is base layer
    private void SetTileColor(Vector3Int pos, int layer, Color color) {
        Tilemap tilemap = (layer > 0) ? allLayers[layer] : baseLayer;
        tilemap.SetTileFlags(pos, TileFlags.None);
        tilemap.SetColor(pos, color);
    }
}