using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush : DestructableEntity
{
    protected override void OnTryBreak(CharacterManager characterManager)
    {
        characterManager.ThinkImmediete("[try again to break this]");
    }
}
