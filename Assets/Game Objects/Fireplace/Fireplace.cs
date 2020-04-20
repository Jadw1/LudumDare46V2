using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Fireplace : PickableEntity
{
    private SpriteRenderer _spriteRenderer;
    private ParticleSystem _particleSystem;
    private GameManager _gameManager;
    private Transform _player;
    private Light2D _light;
    public bool activated = false;
    public bool putOut = false;
    public int lifeTime = 3;
    public bool canBeActivated = true;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _particleSystem = GetComponentInChildren<ParticleSystem>();
        _light = GetComponentInChildren<Light2D>();

        _gameManager = GameObject.FindWithTag("Game Master").GetComponent<GameManager>();
        _player = GameObject.FindWithTag("Player").transform;

        _gameManager.tickEvent += OnTick;
        
        if (activated) return;
        
        _particleSystem.Stop();
        _spriteRenderer.color = Color.white;
        _light.enabled = false;
    }

    public override void OnCollision(Transform collision, int tick) {
        if (!activated && canBeActivated) {
            collision.GetComponent<CharacterInventory>().AddGroundItem(this);
        }
    }

    public void OnTick(int tick) {
        if (putOut && lifeTime > 0) {
            lifeTime--;

            if (lifeTime == 0) {
                _particleSystem.Stop();
                _spriteRenderer.color = Color.white;
                canBeActivated = false;
                activated = false;
                _light.enabled = false;
            }
        }
    }
    
    public override void Action() {
        _spriteRenderer.color = Color.red;
        _particleSystem.Play();
        activated = true;
        _light.enabled = true;

        var pos = _player.localPosition;
        _gameManager.SetCheckpoint(new Vector2Int((int) pos.x, (int) pos.y));
    }
}
