using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : TileEntity {
    private Fireplace fireplace;
    
    public void Start() {
        fireplace = transform.parent.GetComponent<Fireplace>();
    }

    public override void OnCollision(Transform collision, int tick) {
        if (!fireplace.activated) {
            var characterManager = collision.GetComponent<CharacterManager>();
            characterManager.ThinkImmediete("[press 'E' to activate]");
        }
    }
}