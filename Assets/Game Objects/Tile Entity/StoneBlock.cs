using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneBlock : DestructableEntity
{
    protected override bool CanBreak(CharacterManager character, CharacterInventory inventory)
    {
        if (inventory.InventoryItem == InventoryItem.PICKAXE)
        {
            return true;
        }

        character.ThinkImmediete("I can't break this with my bare hands...");
        return false;
    }

    protected override void OnTryBreak(CharacterManager characterManager)
    {
        characterManager.ThinkImmediete("[try again to break this]");
    }
}
