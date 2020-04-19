using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LightSource
{
    NONE,
    MATCH,
    TORCH
}

public enum InventoryItem
{
    NONE,
    PICKAXE
}

public class CharacterInventory : MonoBehaviour
{
    public event Action OnItemChange;
    public event Action OnLightSourceChange;

    private InventoryItem _inventoryItem = InventoryItem.NONE;
    private LightSource _lightSource = LightSource.MATCH;

    public InventoryItem InventoryItem
    {
        get => _inventoryItem;
        set
        {
            OnItemChange();
            _inventoryItem = value;
        }
    }
    
    public LightSource LightSource
    {
        get => _lightSource;
        set
        {
            OnLightSourceChange();
            _lightSource = value;
        }
    }

    private Stack<PickableEntity> onGround;

    private void Start() {
        onGround = new Stack<PickableEntity>();
        GameObject.FindWithTag("Game Master").GetComponent<GameManager>().tickEvent += OnTick;
    }

    public void AddGroundItem(PickableEntity item) {
        onGround.Push(item);
    } 
    
    public void PickUp() {
        if (onGround.Count > 0) {
            var item = onGround.Pop();
            Destroy(item.gameObject);
        }
    }

    private void OnTick(int tick) {
        onGround.Clear();
    }
    
    
}
