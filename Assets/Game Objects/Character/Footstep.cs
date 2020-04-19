using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footstep : MonoBehaviour {
    public float lifeTime;
    private float timeToDie;

    public float startScale;
    public float scaleTo;

    private SpriteRenderer renderer;
    
    // Start is called before the first frame update
    void Start() {
        timeToDie = lifeTime;
        renderer = GetComponent<SpriteRenderer>();
        
        transform.localScale = new Vector3(startScale, startScale, 1.0f);
    }

    // Update is called once per frame
    void Update() {
        timeToDie -= Time.deltaTime;
        if (timeToDie <= 0.0f) {
            Destroy(gameObject);
        }

        float lerpT = timeToDie / lifeTime;
        
        Color color = renderer.color;
        color.a = Mathf.Lerp(0.0f, 1.0f, lerpT);
        renderer.color = color;

        float scale = Mathf.Lerp(scaleTo, startScale, lerpT);
        transform.localScale = new Vector3(scale, scale, 1.0f);
    }
}
