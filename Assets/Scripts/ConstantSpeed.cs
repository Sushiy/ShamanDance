using UnityEngine;
using System.Collections;

public class ConstantSpeed : MonoBehaviour {

    public Vector2 speed;
    public float DestroyTimer = 20f;

    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = speed;

        if (speed.x < 0) {
            Vector3 scale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            transform.localScale = scale;
        }
    }

    void Update()
    {
        DestroyTimer -= Time.deltaTime;
        if (DestroyTimer <= 0f) {
            Destroy(gameObject);
        }
    }
}
