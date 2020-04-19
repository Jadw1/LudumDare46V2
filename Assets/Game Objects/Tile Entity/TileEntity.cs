using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileEntity : MonoBehaviour
{
    public Vector2 GetPos()
    {
        Vector3 localPosition = transform.localPosition;
        return new Vector2(localPosition.x, localPosition.y);
    }

    public void Move(int x, int y)
    {
        transform.localPosition = new Vector3(x, y, 0);
    }
}
