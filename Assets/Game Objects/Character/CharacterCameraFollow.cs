using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCameraFollow : MonoBehaviour
{
    public float lerpFactor = 0.8f;
    private Transform _obj;

    private void Start()
    {
        _obj = GameObject.FindWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        var pos = transform.localPosition;

        var destPos = Vector3.Lerp(pos, _obj.position, lerpFactor);
        destPos.z = pos.z;

        transform.localPosition = destPos;
    }
}
