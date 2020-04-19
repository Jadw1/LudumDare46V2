using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThinkBox : TileEntity
{
    public string[] thoughts;
    public bool immediete;
    public override void OnCollision(Transform collision, int tick)
    {
        var character = collision.GetComponent<CharacterManager>();
        
        foreach (var thought in thoughts)
        {
            if (immediete)
            {
                immediete = false;
                character.ThinkImmediete(thought);
            }
            else character.Think(thought);
        }
        
        Destroy(gameObject);
    }
}
