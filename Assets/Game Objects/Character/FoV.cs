using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct FoVTile {
    public int remaining;
    public int distance;
    public Vector2Int pos;

    public FoVTile(Vector2Int pos, int distance, int remaining) {
        this.pos = pos;
        this.distance = distance;
        this.remaining = remaining;
    }
}

public class FoV : MonoBehaviour {
    private Dictionary<Vector2Int, FoVTile> _FoV;
    private LayerManager manager;

    private int viewDistance;
    
    // Start is called before the first frame update
    void Start() {
        _FoV = new Dictionary<Vector2Int, FoVTile>();
    }

    public void UpdateFoV() {
        //player position
        Vector2Int pos = new Vector2Int((int)transform.position.x, (int)transform.position.y);
        Dictionary<Vector2Int, FoVTile> visible = new Dictionary<Vector2Int, FoVTile>();

        Stack<FoVTile> stack = new Stack<FoVTile>();
        stack.Push(new FoVTile(pos, 0, viewDistance));

        while (stack.Count > 0) {
            var tile = stack.Pop();
            
        }

    }
}
