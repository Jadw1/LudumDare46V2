using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireplace : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private ParticleSystem _particleSystem;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _particleSystem = GetComponentInChildren<ParticleSystem>();
        
        _particleSystem.Stop();
        _spriteRenderer.color = Color.white;
    }
}
