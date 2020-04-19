using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileEntity : MonoBehaviour
{
    public Vector2Int GetPos()
    {
        Vector3 localPosition = transform.localPosition;
        return new Vector2Int((int) localPosition.x, (int) localPosition.y);
    }
}
