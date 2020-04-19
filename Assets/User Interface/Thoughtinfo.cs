using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Thoughtinfo : MonoBehaviour
{
    private TextMeshProUGUI _tmp;

    private void Start()
    {
        _tmp = GetComponent<TextMeshProUGUI>();
        _tmp.SetText("Hello!");
    }

    public void SetThought(string thought)
    {
        _tmp.SetText(thought);
    }
}
