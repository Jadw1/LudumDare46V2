using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum PlayerAction {
    MOVE, SKIP, PICKUP
}

public class GameManager : MonoBehaviour {
    #region TICKS
    public float minTickTime;
    public event Action<int> tickEvent;
    
    private int tickCounter;
    #endregion

    #region PLAYER
    private Transform player;
    private PlayerAction playerAction;
    private Vector2Int playerMovement;
    public GameObject playerFootstep;

    private Torch torch;
    public int tickToDieInDark = 3;
    
    public Vector2Int checkPoint;
    #endregion
    
    private LayerManager layerManager;
    private List<DestructableEntity> savedObjects;
    
    private void Awake() {
        player = GameObject.FindWithTag("Player")?.transform;
        torch = player.GetComponentInChildren<Torch>();
        layerManager = GameObject.FindWithTag("Game Controller")?.GetComponent<LayerManager>();
        
        if (player == null || layerManager == null) {
            throw new Exception("LOL XD");
        }

        tickCounter = 0;

        savedObjects = new List<DestructableEntity>();
    }

    public void RegisterEntityToRestore(DestructableEntity entity) {
        savedObjects.Add(entity);
    }

    private void RestoreEntities() {
        foreach (var entity in savedObjects) {
            entity.gameObject.SetActive(true);
        }
        savedObjects.Clear();
    }

    private void KillPlayer() {
        player.localPosition = new Vector3(checkPoint.x, checkPoint.y);
        torch.RestoreTorch();
        RestoreEntities();
    }

    #region ACTIONS
    public void Skip() {
        playerAction = PlayerAction.SKIP;
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
        playerAction = PlayerAction.MOVE;
        Tick();
    }

    public void PickUp() {
        playerAction = PlayerAction.PICKUP;
        Tick();
    }
    #endregion
    
    private void Tick() {
        HandlePlayerAction();
        CheckTorch();
        tickEvent?.Invoke(tickCounter);
        tickCounter++;
    }

    private void CheckTorch() {
        if (torch.condition == 0) {
            if (tickToDieInDark == 0) {
                KillPlayer();
            }
            else {
                tickToDieInDark--;
            }
        }
    }

    private void HandlePlayerAction() {
        switch (playerAction) {
            case PlayerAction.MOVE:
                MovePlayer();
                break;
            case PlayerAction.SKIP:
                CheckStaticCollision();
                break;
            case PlayerAction.PICKUP:
                CheckStaticCollision();
                var inv = player.GetComponent<CharacterInventory>();
                inv.PickUp();
                break;
        }
    }

    #region MOVEMENT & COLLISION
    private void MovePlayer() {
        if (playerMovement.x == 0 && playerMovement.y == 0) {
            CheckStaticCollision();
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
            CheckStaticCollision();
            return false;
        }

        var pos = player.position + new Vector3(playerMovement.x, playerMovement.y, 0.0f);
        var ray = Physics2D.RaycastAll(pos, playerMovement, 0.4f);
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

    private void CheckStaticCollision() {
        var ray = Physics2D.RaycastAll(player.position, Vector2.up, 0.4f);
        foreach (var hit2D in ray) {
            hit2D.transform.GetComponent<TileEntity>()?.OnCollision(player, tickCounter);
        }
    }
    #endregion
    
}
