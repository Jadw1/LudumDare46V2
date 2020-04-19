using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnThought(string thought);
public class CharacterManager : MonoBehaviour
{
    public event OnThought OnThought;

    public void Think(string thought)
    {
        OnThought?.Invoke(thought);
    }
}
