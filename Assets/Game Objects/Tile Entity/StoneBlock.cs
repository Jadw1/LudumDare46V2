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

        character.Think("I can't break this with my bare hands...");
        return false;
    }

    protected override void OnTryBreak(CharacterManager characterManager)
    {
        characterManager.Think("[try again to break this]");
    }
}
