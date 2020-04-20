using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColdArea : TileEntity
{
    private CharacterManager _characterManager;
    private CharacterInventory _characterInventory;
    private Torch _torch;

    private void Start()
    {
        var player = GameObject.FindWithTag("Player");

        _characterManager = player.GetComponent<CharacterManager>();
        _characterInventory = player.GetComponent<CharacterInventory>();
        _torch = player.GetComponentInChildren<Torch>();
    }

    public override void OnCollision(Transform collision, int tick)
    {
        if (_characterInventory.LightSource != LightSource.MATCH || _torch.condition <= 4) return;
        
        _characterManager.ThinkImmediete("These cold winds blew my match off!");
        _characterManager.Think("This is bad. I'm gonna freeze!");
        _torch.condition = 4;
    }
}