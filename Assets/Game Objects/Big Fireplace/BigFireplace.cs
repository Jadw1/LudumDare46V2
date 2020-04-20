using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class BigFireplace : PickableEntity {
    private bool activated = false;

    private BigFireplacePart[] fires;
    private Light2D light;
    void Start() {
        fires = GetComponentsInChildren<BigFireplacePart>();
        light = GetComponentInChildren<Light2D>();

        light.enabled = false;
    }

    public override void Action() {
        foreach (var fire in fires) {
            fire.Activate();
        }

        light.enabled = true;
        activated = true;
    }

    public override void OnCollision(Transform collision, int tick) {
        if (!activated) {
            collision.GetComponent<CharacterInventory>().AddGroundItem(this);
        }
    }
}
