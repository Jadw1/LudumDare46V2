using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public event Action<string> OnThought;
    public event Action<string> OnThoughtImmediete;

    public void Think(string thought)
    {
        OnThought?.Invoke(thought);
    }
    
    public void ThinkImmediete(string thought)
    {
        OnThoughtImmediete?.Invoke(thought);
    }
}
