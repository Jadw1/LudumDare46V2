using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour {
    private Transform player;
    private Vector2Int playerMovement;

    public float minTickTime;
    public GameObject playerFootstep;
    
    // Start is called before the first frame update
    private void Awake() {
        player = GameObject.FindWithTag("Player")?.transform;
        if (player == null) {
            throw new Exception("LOL XD");
        }
    }

    public void Skip() {
        Tick();
    }

    public void Move(MoveDirection direction) {
        int moveX = 0;
        int moveY = 0;

        switch (direction) {
            case MoveDirection.UP:
                moveY = 1;
                break;
            case MoveDirection.DOWN:
                moveY = -1;
                break;
            case MoveDirection.LEFT:
                moveX = -1;
                break;
            case MoveDirection.RIGHT:
                moveX = 1;
                break;
        }

        playerMovement = (moveX != 0) ? new Vector2Int(moveX, 0) : new Vector2Int(0, moveY);

        Tick();
    }
    
    private void Tick() {
        movePlayer();
    }

    private void movePlayer() {
        if (playerFootstep && playerMovement.magnitude != 0.0f) {
            var footstep = Instantiate(playerFootstep, transform);
            footstep.transform.position = player.position;
        }
        
        player.Translate(playerMovement.x, playerMovement.y, 0.0f);
        playerMovement = Vector2Int.zero;
    }
}
