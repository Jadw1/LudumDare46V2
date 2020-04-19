using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ThoughtInfo : MonoBehaviour
{
    public string[] initialThoughts;
    
    public float typingSpeed = 0.05f;
    public float pauseTime = 2.5f;
    
    private TextMeshProUGUI _tmp;
    private Image _bg;

    private Queue<string> _thoughts = new Queue<string>();
    private bool _idle = true;
    private void Start()
    {
        _bg = GetComponent<Image>();
        _tmp = GetComponentInChildren<TextMeshProUGUI>();
        Enable(false);

        if (initialThoughts == null) return;
        foreach (var thought in initialThoughts)
        {
            AddThought(thought);
        }
    }

    private IEnumerator TypeThought(string thought)
    {
        _idle = false;
        _tmp.SetText("");
        foreach (var letter in thought.ToCharArray())
        {
            _tmp.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        
        yield return new WaitForSeconds(pauseTime);
        _idle = true;
    }

    private void Enable(bool enable)
    {
        _bg.enabled = enable;
        _tmp.enabled = enable;
    }

    public void AddThought(string thought)
    {
        Enable(true);
        _thoughts.Enqueue(thought);
    }
    
    private void FixedUpdate()
    {
        if (_idle)
        {
            if (_thoughts.Count != 0)
            {
                StartCoroutine(TypeThought(_thoughts.Dequeue()));
            }
            else
            {
                Enable(false);
            }
        }
    }
}
