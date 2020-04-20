using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireplace : PickableEntity
{
    private SpriteRenderer _spriteRenderer;
    private ParticleSystem _particleSystem;
    public bool activated = false;
    public bool putOut = false;
    public int lifeTime = 3;
    public bool canBeActivated = true;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _particleSystem = GetComponentInChildren<ParticleSystem>();

        if (!activated) {
            _particleSystem.Stop();
            _spriteRenderer.color = Color.white;
        }
    }

    public override void OnCollision(Transform collision, int tick) {
        if (!activated && canBeActivated) {
            collision.GetComponent<CharacterInventory>().AddGroundItem(this);
        }
        else if (putOut && lifeTime > 0) {
            lifeTime--;
            _particleSystem.Stop();
            _spriteRenderer.color = Color.white;
            canBeActivated = false;
            activated = false;
        }
    }
    
    public override void Action() {
        _spriteRenderer.color = Color.red;
        _particleSystem.Play();
        activated = true;
    }
}
