using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileEntityManager : MonoBehaviour
{
    private readonly Dictionary<Vector2Int, TileEntity> _entities = new Dictionary<Vector2Int, TileEntity>();

    private void Start()
    {
        foreach (var obj in GameObject.FindGameObjectsWithTag("Tile Entity"))
        {
            RegisterGameObject(obj);
        }
        
        RegisterGameObject(GameObject.FindWithTag("Player"));
    }

    private void RegisterGameObject(GameObject obj)
    {
        var te = obj.GetComponent<TileEntity>();
            
        if (te == null) throw new Exception("WTF 2!");

        Register(te);
    }

    public void Register(TileEntity te)
    {
        _entities.Add(te.GetPos(), te);
    }

    public bool Move(TileEntity te, Vector2Int dest)
    {
        Vector2Int currentPos = te.GetPos();
        
        if (!_entities.ContainsKey(currentPos))
        {
            throw new Exception("WTF");
        }

        if (_entities.TryGetValue(dest, out TileEntity destTe))
        {
            // Handle interaction but otherwise abort
            return false;
        }
        
        _entities.Remove(currentPos);
        _entities.Add(dest, te);
        var teTransform = te.transform;
        teTransform.localPosition = new Vector3(dest.x, dest.y, teTransform.localPosition.z);

        return true;
    }
}
