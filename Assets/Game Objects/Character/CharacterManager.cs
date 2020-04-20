using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public event Action<string> OnThought;
    public event Action<string> OnThoughtImmediete;

    private CharacterInventory _inventory;

    private void Start()
    {
        _inventory = GetComponent<CharacterInventory>();
    }

    public void OnDeath()
    {
        ThinkImmediete("Weird... Was it just a dream?");

        if (_inventory.LightSource == LightSource.TORCH)
        {
            Think("I swear I had a match before...");
        }

        if (_inventory.InventoryItem == InventoryItem.PICKAXE)
        {
            Think("How did I get this pickaxe?");
            Think("I'm not a miner...");
            Think("Maybe?");
        }
    }
    
    public void Think(string thought)
    {
        OnThought?.Invoke(thought);
    }
    
    public void ThinkImmediete(string thought)
    {
        OnThoughtImmediete?.Invoke(thought);
    }
}
