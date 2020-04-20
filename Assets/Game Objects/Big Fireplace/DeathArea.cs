using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathArea : TileEntity
{
    public override void OnCollision(Transform collision, int tick) {
        Debug.Log("GAME FINISHED!");
    }
}
