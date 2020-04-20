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
            _inventoryItem = value;
            OnItemChange();
        }
    }
    
    public LightSource LightSource
    {
        get => _lightSource;
        set
        {
            _lightSource = value;
            OnLightSourceChange();
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

            if (item.itemType == ItemType.PICKAXE)
            {
                InventoryItem = InventoryItem.PICKAXE;
                foreach (var o in GameObject.FindGameObjectsWithTag("Pickaxe Thought"))
                {
                    Destroy(o);
                }
            }
            else if(item.itemType == ItemType.TORCH)
            {
                GetComponentInChildren<Torch>().SetTorch(170, 15);
                LightSource = LightSource.TORCH;
            }
            
            item.Action();
        }
    }

    private void OnTick(int tick) {
        onGround.Clear();
    }
    
    
}
