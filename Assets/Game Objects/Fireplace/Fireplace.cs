using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireplace : PickableEntity
{
    private SpriteRenderer _spriteRenderer;
    private ParticleSystem _particleSystem;
    public bool activated = false;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _particleSystem = GetComponentInChildren<ParticleSystem>();
        
        _particleSystem.Stop();
        _spriteRenderer.color = Color.white;
    }

    public override void OnCollision(Transform collision, int tick) {
        if(!activated)
            collision.GetComponent<CharacterInventory>().AddGroundItem(this);
    }
    
    public override void Action() {
        _spriteRenderer.color = Color.red;
        _particleSystem.Play();
        activated = true;
    }
}
