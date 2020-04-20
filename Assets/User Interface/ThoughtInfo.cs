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

    private bool _immediate;
    private float _timer = 0.0f;
    private Queue<char> _thought = new Queue<char>();

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

        var player = GameObject.FindWithTag("Player");
        var playerManager = player.GetComponent<CharacterManager>();

        playerManager.OnThought += AddThought;
        playerManager.OnThoughtImmediete += AddImmediateThought;
    }

    private void Enable(bool enable)
    {
        if (_bg.enabled == enable) return;
        _bg.enabled = enable;
        _tmp.enabled = enable;
    }

    public void AddImmediateThought(string thought)
    {
        _immediate = true;
        _thoughts.Clear();
        _thoughts.Enqueue(thought);
    }

    public void AddThought(string thought)
    {
        if (_thought.Count == 0) _immediate = true;
        _thoughts.Enqueue(thought);
    }

    private void FixedUpdate()
    {
        _timer += Time.deltaTime;
        
        if (_immediate)
        {
            _tmp.text = "";
            Enable(true);
            _thought.Clear();
            _immediate = false;

            var thought = _thoughts.Dequeue();

            if (string.IsNullOrWhiteSpace(thought))
            {
                _tmp.text = "";
                Enable(false);
                return;
            }
            
            foreach (var character in thought.ToCharArray())
            {
                _thought.Enqueue(character);
            }
        } else if (_thought.Count == 0)
        {
            if (!(_timer > pauseTime)) return;
            
            _timer = 0.0f;
            if (_thoughts.Count != 0)
            {
                foreach (var character in _thoughts.Dequeue().ToCharArray())
                {
                    _thought.Enqueue(character);
                }

                _tmp.text = "";
                Enable(true);
            }
            else
            {
                _tmp.text = "";
                Enable(false);
            }

            return;
        }

        if (!(_timer > typingSpeed)) return;
        
        _timer = 0.0f;
        _tmp.text += _thought.Dequeue();
    }
}