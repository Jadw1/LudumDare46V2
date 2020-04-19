using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {
    public float tickTime;
    private float timeToTick = 0.0f;
    
    private void Update() {
        if (timeToTick > 0.0f) {
            timeToTick -= Time.deltaTime;
        }

        if (timeToTick <= 0.0f) {
            float moveX = 0.0f;
            float moveY = 0.0f;
            
            if (Input.GetKey(KeyCode.W)) {
                moveX = 0.0f;
                moveY = 1.0f;
            }
            else if (Input.GetKey(KeyCode.S)) {
                moveX = 0.0f;
                moveY = -1.0f;
            }
            else if (Input.GetKey(KeyCode.A)) {
                moveX = -1.0f;
                moveY = 0.0f;
            }
            else if (Input.GetKey(KeyCode.D)) {
                moveX = 1.0f;
                moveY = 0.0f;
            }

            if (moveX != 0.0f || moveY != 0.0f) {
                timeToTick = tickTime;
                transform.Translate(moveX, moveY, 0.0f);
            }
        }
    }
}
