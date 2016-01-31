using UnityEngine;
using System.Collections;

public class Tree : MonoBehaviour {

    private Vector2 desiredColliderOffset;
    private Vector2 maxSize;
    private Vector2 maxLocalScale;
    private BoxCollider2D boxCollider;
	void Start () {

        boxCollider = this.GetComponent<BoxCollider2D>();
        desiredColliderOffset = boxCollider.offset;
        maxSize = boxCollider.size;
        maxLocalScale = this.transform.localScale;

        boxCollider.offset = new Vector2(boxCollider.offset.x, 0.4f);
        boxCollider.size = new Vector2(boxCollider.size.x, 0.01f);
        this.transform.localScale = new Vector2(transform.localScale.x, 0.0001f);

	}
	
	// Update is called once per frame
	void Update () {

        boxCollider.offset = Vector2.Lerp(boxCollider.offset, desiredColliderOffset, Time.deltaTime);
        boxCollider.size = Vector2.Lerp(boxCollider.size, maxSize, Time.deltaTime);
        transform.localScale = Vector2.Lerp(transform.localScale, maxLocalScale, Time.deltaTime);
	}
}
