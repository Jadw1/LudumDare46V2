using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableEntity : TileEntity {

    private int lastContact = -1;
    
    public override void OnCollision(Transform collision, int tick) {
        if (tick == lastContact + 1) {
            Destroy(gameObject);
        }

        lastContact = tick;
    }
}
