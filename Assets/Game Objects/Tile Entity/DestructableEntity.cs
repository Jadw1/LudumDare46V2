using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableEntity : TileEntity
{
    public string thought;
    private int lastContact = -1;
    
    public override void OnCollision(Transform collision, int tick) {
        if (tick == lastContact + 1) {
            Destroy(gameObject);
        }
        else
        {
            var characterManager = collision.GetComponent<CharacterManager>();
            
            if (!string.IsNullOrWhiteSpace(thought))
            {
                characterManager.Think(thought);
            }
        }

        lastContact = tick;
    }
}
