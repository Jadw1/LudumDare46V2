using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour {
    private int tickCounter;
    
    private Transform player;
    private Vector2Int playerMovement;

    public float minTickTime;
    public GameObject playerFootstep;

    private LayerManager layerManager;
    
    // Start is called before the first frame update
    private void Awake() {
        player = GameObject.FindWithTag("Player")?.transform;
        layerManager = GameObject.FindWithTag("Game Controller")?.GetComponent<LayerManager>();
        
        if (player == null || layerManager == null) {
            throw new Exception("LOL XD");
        }

        tickCounter = 0;
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
        MovePlayer();

        tickCounter++;
    }

    private void MovePlayer() {
        if (playerMovement.x == 0 && playerMovement.y == 0) {
            return;
        }

        if (CanMove()) {
         if (playerFootstep) {
             var footstep = Instantiate(playerFootstep, transform);
             footstep.transform.position = player.position;
         }
        
         player.Translate(playerMovement.x, playerMovement.y, 0.0f);
        }
        playerMovement = Vector2Int.zero;
    }

    private bool CanMove() {
        int toX = (int)player.position.x + playerMovement.x;
        int toY = (int)player.position.y + playerMovement.y;
        if (!layerManager.CanMoveTo(toX, toY)) {
            return false;
        }

        var ray = Physics2D.RaycastAll(player.position, playerMovement, 1.0f);
        if (ray.Length == 0) {
            return true;
        }

        bool result = true;
        foreach (var hit2D in ray) {
            var tile = hit2D.transform.GetComponent<TileEntity>();

            if (tile == null) {
                result = false;
                continue;
            }
            
            result &= tile.walkable;
            tile.OnCollision(player, tickCounter);
        }

        return result;
    }
}
