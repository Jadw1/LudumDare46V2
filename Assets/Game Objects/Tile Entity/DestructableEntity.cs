using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableEntity : TileEntity
{
    private int lastContact = -1;

    protected virtual bool CanBreak(CharacterManager character, CharacterInventory inventory)
    {
        return true;
    }

    protected virtual void OnTryBreak(CharacterManager characterManager)
    {
        
    }

    public override void OnCollision(Transform collision, int tick) {
        var characterManager = collision.GetComponent<CharacterManager>();
        var characterInventory = collision.GetComponent<CharacterInventory>();
        
        if (tick == lastContact + 1)
        {
            if (CanBreak(characterManager, characterInventory)) {
                GameObject.FindWithTag("Game Master").GetComponent<GameManager>().RegisterEntityToRestore(this);
                gameObject.SetActive(false);
            }
        }
        else
        {
            OnTryBreak(characterManager);
        }

        lastContact = tick;
    }
}
