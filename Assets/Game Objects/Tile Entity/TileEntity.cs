using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(BoxCollider2D))]
public abstract class TileEntity : MonoBehaviour {
    public bool walkable;

    public abstract void OnCollision(Transform collision, int tick);
}