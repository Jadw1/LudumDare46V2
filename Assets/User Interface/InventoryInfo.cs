using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class InventoryInfo : MonoBehaviour
{
    private TextMeshProUGUI _tmp;
    private CharacterInventory _inv;
    
    private void Start()
    {
        _tmp = GetComponent<TextMeshProUGUI>();
        _inv = GameObject.FindWithTag("Player").GetComponent<CharacterInventory>();

        RedrawInventory();

        _inv.OnItemChange += RedrawInventory;
    }

    private void RedrawInventory()
    {
        if (_inv.InventoryItem == InventoryItem.PICKAXE)
        {
            _tmp.SetText("I have a pickaxe.");
        }
        else
        {
            _tmp.SetText("I don't have any tools.");
        }
    }
}
