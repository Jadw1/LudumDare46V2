using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class LightSourceInfo : MonoBehaviour
{
    private TextMeshProUGUI _tmp;
    private CharacterInventory _inv;

    private void Start()
    {
        _tmp = GetComponent<TextMeshProUGUI>();
        _inv = GameObject.FindWithTag("Player").GetComponent<CharacterInventory>();

        RedrawLightSourceInfo();

        _inv.OnLightSourceChange += RedrawLightSourceInfo;
    }
    
    private void RedrawLightSourceInfo()
    {
        StringBuilder builder = new StringBuilder();

        builder.Append("<color #ff0000>");

        var lightSource = _inv.LightSource;
        
        switch (lightSource)
        {
            case LightSource.MATCH:
                builder.Append("simple match");
                break;
            case LightSource.TORCH:
                builder.Append("sturdy torch");
                break;
            default:
                builder.Append("No light source...");
                break;
        }

        builder.AppendLine("</color>");

        if (lightSource == LightSource.NONE)
        {
            builder.AppendLine("With no source of warmth, my death is imminent.");
        }
        else
        {
            builder.Append("<color #ffff00>");
            builder.Append(_inv.LightSourceCharge);
            builder.AppendLine("</color>");
            builder.Append("moves left");
        }
        
        _tmp.SetText(builder.ToString());
    }
}
