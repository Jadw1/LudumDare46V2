using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Torch : MonoBehaviour {
    public int maxCondition;
    public int condition;

    public float maxRadious;
    
    private Light2D light;
    
    // Start is called before the first frame update
    void Start() {
        light = GetComponent<Light2D>();
        GameObject.FindWithTag("Game Master").GetComponent<GameManager>().tickEvent += OnTick;
        
        SetTorch(maxCondition, maxRadious);
    }

    public void SetTorch(int maxCondition, float maxRadious) {
        this.maxCondition = maxCondition;
        this.maxRadious = maxRadious;

        condition = maxCondition;
        light.pointLightOuterRadius = maxRadious;
    }

    void OnTick(int tick) {
        if (condition > 0) {
            condition--;
            light.pointLightOuterRadius = Mathf.Lerp(0.0f, maxRadious, (float) condition / maxCondition);
        }
        //event if light goes out?
    }
}
