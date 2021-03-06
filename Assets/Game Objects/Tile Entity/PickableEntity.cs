﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    PICKAXE,
    TORCH,
    OTHER
}

public class PickableEntity : TileEntity
{
    public ItemType itemType;
    
    public override void OnCollision(Transform collision, int tick) {
        collision.GetComponent<CharacterInventory>().AddGroundItem(this);
    }

    public virtual void Action() {
        Destroy(gameObject);
    }
}
