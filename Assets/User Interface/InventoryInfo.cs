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
        _tmp = GetComponentInChildren<TextMeshProUGUI>();
        _inv = GameObject.FindWithTag("Player").GetComponent<CharacterInventory>();

        RedrawInventory();

        _inv.OnItemChange += RedrawInventory;
        _inv.OnLightSourceChange += RedrawInventory;
    }

    private void RedrawInventory()
    {
        StringBuilder builder = new StringBuilder();

        builder.AppendLine("You have");
        
        switch (_inv.LightSource)
        {
            case LightSource.MATCH:
                builder.AppendLine("<color #ffff00>a simple match");
                break;
            case LightSource.TORCH:
                builder.AppendLine("<color #ffff00>a sturdy torch");
                break;
            default:
                builder.AppendLine("<color #ff0000>nothing");
                break;
        }

        builder.AppendLine("</color>to keep you warm.\n");

        builder.Append(_inv.InventoryItem == InventoryItem.PICKAXE
            ? "You have a <color #ffff00>pickaxe</color>."
            : "You don't have any tools.");

        _tmp.SetText(builder.ToString());
    }
}
