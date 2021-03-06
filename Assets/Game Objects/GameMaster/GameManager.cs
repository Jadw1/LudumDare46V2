﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public enum PlayerAction {
    MOVE, SKIP, PICKUP
}

public class GameManager : MonoBehaviour {
    #region TICKS
    public float minTickTime;
    public event Action<int> tickEvent;
    
    private int tickCounter;
    private bool block = false;
    #endregion

    #region PLAYER
    private Transform player;
    private CharacterManager characterManager;
    private PlayerAction playerAction;
    private Vector2Int playerMovement;
    public GameObject playerFootstep;

    private Torch torch;
    public int lifeTimeInDark = 5;
    private int tickToDieInDark;
    private Animator deathAnimator;
    
    public Vector2Int checkPoint;
    #endregion
    
    private LayerManager layerManager;
    private List<DestructableEntity> savedObjects;
    public VolumeProfile postProcessing;
    private WhiteBalance temperature;
    public float waitForDeathAnimation = 1.0f;

    private int burnTicks = -1;
    
    private void Awake() {
        player = GameObject.FindWithTag("Player")?.transform;
        characterManager = player.GetComponent<CharacterManager>();
        torch = player.GetComponentInChildren<Torch>();
        layerManager = GameObject.FindWithTag("Game Controller")?.GetComponent<LayerManager>();
        temperature = postProcessing.components[1] as WhiteBalance;
        deathAnimator = GameObject.FindWithTag("UI")?.GetComponent<Animator>();

        if (!player || !layerManager || !postProcessing || !temperature) {
            throw new Exception("LOL XD");
        }
        

        tickCounter = 0;
        tickToDieInDark = lifeTimeInDark;
        savedObjects = new List<DestructableEntity>();
    }

    public void BurnPlayer()
    {
        if (burnTicks != -1) return;
        burnTicks = 8;
        characterManager.ThinkImmediete("Finally!");
        characterManager.EnableBurn();
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

    public void SetCheckpoint(Vector2Int pos)
    {
        checkPoint = pos;
    }

    private IEnumerator KillPlayer() {
        block = true;
        deathAnimator.SetTrigger("Die");
        yield return new WaitForSeconds(waitForDeathAnimation);
        if (burnTicks == 0) SceneManager.LoadScene("EndGameScene");
        player.localPosition = new Vector3(checkPoint.x, checkPoint.y);
        torch.RestoreTorch();
        RestoreEntities();
        tickToDieInDark = lifeTimeInDark;
        temperature.temperature.value = 0.0f;
        temperature.active = false;
        block = false;
        characterManager.OnDeath();
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
        if(block)
            return;
        HandlePlayerAction();
        if (burnTicks == -1)
        {
            CheckTorch();
        }
        else if (burnTicks > 0)
        {
            burnTicks--;
            if (burnTicks == 4) characterManager.Think("The time has come!");
            if (burnTicks == 0) StartCoroutine(KillPlayer());
        }
        tickEvent?.Invoke(tickCounter);
        tickCounter++;
    }

    private void CheckTorch()
    {
        if (torch.condition != 0) return;
        if (tickToDieInDark == 0) {
            StartCoroutine(KillPlayer());
        }
        else {
            tickToDieInDark--;
        }

        temperature.active = true;
        temperature.temperature.value = Mathf.Lerp(-60.0f, 0.0f, (float) tickToDieInDark / lifeTimeInDark);
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
