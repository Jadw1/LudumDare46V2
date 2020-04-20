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
    private CharacterManager _characterManager;
    private Torch _torch;

    private bool burn = false;
    
    private void Start()
    {
        _tmp = GetComponentInChildren<TextMeshProUGUI>();
        var player = GameObject.FindWithTag("Player");
        _inv = player.GetComponent<CharacterInventory>();
        _torch = player.GetComponentInChildren<Torch>();
        _characterManager = player.GetComponent<CharacterManager>();
        
        RedrawInventory();

        _inv.OnItemChange += RedrawInventory;
        _inv.OnLightSourceChange += RedrawInventory;
        _torch.TorchConditionChangeEvent += TorchConditionChangeEvent;
        _characterManager.OnBurn += OnBurn;
    }

    private void TorchConditionChangeEvent(int condition)
    {
        RedrawInventory();
    }

    private void OnBurn()
    {
        burn = true;
        RedrawInventory();
    }

    private void RedrawInventory()
    {
        if (burn)
        {
            _tmp.SetText("<color #ff0000>YOU</color>\n" +
                         "<color #ffff00>ARE</color>\n" +
                         "<color #ff0000>THE TORCH</color>");
            return;
        }
        
        StringBuilder builder = new StringBuilder();

        builder.AppendLine("You have");

        string color = _torch.condition > 0 ? "#ffff00" : "#666666";
        
        switch (_inv.LightSource)
        {
            case LightSource.MATCH:
                builder.AppendLine($"<color {color}>a simple match");
                break;
            case LightSource.TORCH:
                builder.AppendLine($"<color {color}>a sturdy torch");
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
