using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceShader : MonoBehaviour {
    private Transform player;
    private Torch torch;
    private SpriteRenderer renderer; 
    private Color initColor;
    
    public float scaleTo = 0.05f;
    
    // Start is called before the first frame update
    void Start() {
        player = GameObject.FindWithTag("Player").transform;
        torch = player.GetComponentInChildren<Torch>();
        renderer = GetComponent<SpriteRenderer>();
        initColor = renderer.color;

        GameObject.FindWithTag("Game Master").GetComponent<GameManager>().tickEvent += OnTick;
    }

    public void OnTick(int tick) {
        float distance = (transform.position - player.position).magnitude;
        float scale = (distance <= torch.radious) ? ((torch.radious - distance)/torch.radious) : 0.0f;
        renderer.color = Color.Lerp(new Color(scaleTo, scaleTo, scaleTo, 1.0f), initColor, scale);
    }

    private void OnDestroy() {
        GameObject.FindWithTag("Game Master").GetComponent<GameManager>().tickEvent -= OnTick;
    }
}
