using UnityEngine;
using System.Collections;

public class Seed : MonoBehaviour {

    public bool isHitByRain;

    [SerializeField]
    private float lifeTime;

    public Tree treePrefab;


	// Use this for initialization
	void Start () {

        lifeTime = 8.5f;
        
        
	}

    private float wobble = 0.5f;

	// Update is called once per frame
	void Update () {

        if (isHitByRain)
        {
            if (transform.localScale.x > 1.1f)
                wobble = 0.5f;
            if (transform.localScale.x < 0.9f)
                wobble = 1.5f;

            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * wobble, Time.deltaTime*0.8f);
            lifeTime -= Time.deltaTime;
        }
        if (lifeTime < 0) {
            Destroy(this.gameObject);
            GameObject tree = Instantiate(treePrefab, transform.position, Quaternion.identity) as GameObject;
        }
	}

    void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Rain"))
        {
            isHitByRain = true;
        }
    }
}
