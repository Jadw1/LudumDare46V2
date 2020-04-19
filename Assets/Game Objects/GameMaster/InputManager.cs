using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

public enum MoveDirection {
    LEFT, RIGHT, UP, DOWN
}


public class InputManager : MonoBehaviour {
    private GameManager gm;
    private bool movementKeyPressed = false;
    private float previousTick;

    private void Awake() {
        gm = transform.GetComponent<GameManager>();
    }

    private void Update() {
        int moveX = (int)Input.GetAxisRaw("Horizontal");
        int moveY = (int)Input.GetAxisRaw("Vertical");

        if (moveX != 0 || moveY != 0) {
            //calculating movement direction
            MoveDirection dir;
            if (moveX != 0) {
                dir = (moveX > 0) ? MoveDirection.RIGHT : MoveDirection.LEFT;
            }
            else {
                dir = (moveY > 0) ? MoveDirection.UP : MoveDirection.DOWN;
            }

            if (movementKeyPressed) {
                float diff = Time.time - previousTick;
                if (diff < gm.minTickTime) {
                    return;
                }
            }
            else {
                movementKeyPressed = true;
            }

            previousTick = Time.time;
            gm.Move(dir);
            return;
        }
        
        movementKeyPressed = false;

        if (Input.GetKeyDown(KeyCode.Space)) {
            gm.Skip();
        }
    }
}
