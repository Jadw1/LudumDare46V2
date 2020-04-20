using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigFireplacePart : MonoBehaviour {
    private SpriteRenderer _spriteRenderer;
    private ParticleSystem _particleSystem;
    
    // Start is called before the first frame update
    void Start() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _particleSystem = GetComponentInChildren<ParticleSystem>();

        _spriteRenderer.color = Color.white;
        _particleSystem.Stop();
    }

    public void Activate() {
        _spriteRenderer.color = Color.red;
        _particleSystem.Play();
    }
}
