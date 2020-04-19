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

public delegate void InventoryItemChanged();

public delegate void InventoryLightSourceChanged();

public class CharacterInventory : MonoBehaviour
{
    public event InventoryItemChanged OnItemChange;
    public event InventoryLightSourceChanged OnLightSourceChange;

    private InventoryItem _inventoryItem = InventoryItem.NONE;
    private LightSource _lightSource = LightSource.MATCH;
    private int _lightSourceCharge = 15;

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
    
    public int LightSourceCharge
    {
        get => _lightSourceCharge;
        set
        {
            OnLightSourceChange();
            _lightSourceCharge = value;
        }
    }
}
