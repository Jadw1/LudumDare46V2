using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableEntity : TileEntity
{

    public override void OnCollision(Transform collision, int tick) {
        collision.GetComponent<CharacterInventory>().AddGroundItem(this);
    }
}
