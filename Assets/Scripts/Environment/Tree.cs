using UnityEngine;
using System.Collections;

public class Tree : MonoBehaviour {

    private Vector2 desiredColliderOffset;
    private Vector2 maxSize;
    private Vector2 maxLocalScale;
    private BoxCollider2D boxCollider;
    [SerializeField]
    private float lifeTime;

    public Seed seedPrefab;

	void Start () {

        boxCollider = this.GetComponent<BoxCollider2D>();
        desiredColliderOffset = boxCollider.offset;
        maxSize = boxCollider.size;
        maxLocalScale = this.transform.localScale;
        lifeTime = 8f;

        boxCollider.offset = new Vector2(boxCollider.offset.x, 0.4f);
        boxCollider.size = new Vector2(boxCollider.size.x, 0.01f);
        this.transform.localScale = new Vector2(transform.localScale.x, 0.0001f);

	}
	
	// Update is called once per frame
	void Update () {

        boxCollider.offset = Vector2.Lerp(boxCollider.offset, desiredColliderOffset, Time.deltaTime);
        boxCollider.size = Vector2.Lerp(boxCollider.size, maxSize, Time.deltaTime);
        transform.localScale = Vector3.Lerp(transform.localScale, maxLocalScale, Time.deltaTime);
       
       
        lifeTime -= Time.deltaTime;

        if (lifeTime < 0.0f)
        {
            boxCollider.offset = Vector2.Lerp(boxCollider.offset, new Vector2 (boxCollider.offset.x, 0.1f), Time.deltaTime);
            boxCollider.size = Vector2.Lerp(boxCollider.size, new Vector2 (boxCollider.size.x, 0.1f), Time.deltaTime);
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3 (1f, -1f, 1f), Time.deltaTime);

            if (transform.localScale.y  < 0.01f)
            {

                Destroy(this.gameObject);
                GameObject seed = Instantiate(seedPrefab, transform.position, Quaternion.identity) as GameObject;
            }
        }
	}
}
