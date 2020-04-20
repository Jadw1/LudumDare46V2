using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Torch : MonoBehaviour
{
    public event Action<int> TorchConditionChangeEvent;
    
    public int maxCondition;
    public int condition;

    public float maxRadious;
    public float radious;
    
    private Light2D light;
    private CharacterManager character;
    
    // Start is called before the first frame update
    void Start() {
        light = GetComponent<Light2D>();
        character = GetComponentInParent<CharacterManager>();
        GameObject.FindWithTag("Game Master").GetComponent<GameManager>().tickEvent += OnTick;
        
        SetTorch(maxCondition, maxRadious);
    }

    public void SetTorch(int maxCondition, float maxRadious) {
        this.maxCondition = maxCondition;
        this.maxRadious = maxRadious;

        condition = maxCondition;
        radious = maxRadious;
        light.pointLightOuterRadius = maxRadious;
    }

    public void RestoreTorch() {
        condition = maxCondition;
        radious = maxRadious;
    }
    
    void OnTick(int tick) {
        if (condition > 0) {
            condition--;
            TorchConditionChangeEvent?.Invoke(condition);
            radious = Mathf.Lerp(0.0f, maxRadious, (float) condition / maxCondition);
            light.pointLightOuterRadius = radious;
        }
        else
        {
            character.Think("Oh no! I'm gonna freeze!");
        }
    }
}
