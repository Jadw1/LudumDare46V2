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
            int moveX = (int)Input.GetAxisRaw("Horizontal");
            int moveY = (int)Input.GetAxisRaw("Vertical");

            if (moveX != 0 || moveY != 0) {
                timeToTick = tickTime;

                if (moveX != 0)
                    moveY = 0;
                
                transform.Translate(moveX, moveY, 0.0f);
            }
        }
    }
}
