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

    private SpriteRenderer _sprite;
    private ParticleSystem _particleSystem;

    public event Action OnBurn;
    
    private void Start()
    {
        _inventory = GetComponent<CharacterInventory>();
        _sprite = GetComponent<SpriteRenderer>();
        _particleSystem = GetComponentInChildren<ParticleSystem>();
        _particleSystem.Stop();
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

    public void EnableBurn()
    {
        OnBurn?.Invoke();
        _sprite.color = Color.red;
        _particleSystem.Play();
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
