using UnityEngine;
using System.Collections;

public class WolfSpawner : MonoBehaviour {

    [SerializeField]
    private float minSpawnTime = 15f;
    [SerializeField]
    private float maxSpawnTime = 50f;
    [SerializeField]
    private GameObject wolfCritter;
    [SerializeField]
    private GameObject wolfCritterLeft;

    private Transform leftWolf, rightWolf;
    float currentTimer = 0;
    float currentSpawntime;

    void Start()
    {
        leftWolf = transform.FindChild("leftWolf");
        rightWolf = transform.FindChild("rightWolf");
    }

    void Update()
    {
        if (currentTimer == 0) {
            currentSpawntime = Random.Range(minSpawnTime, maxSpawnTime);
            currentTimer += Time.deltaTime;
        }
        else {
            currentTimer += Time.deltaTime;

            if (currentTimer >= currentSpawntime) {
                float probability = Random.Range(0f, 1f);
                if (probability >= 0.5f) {
                    Instantiate(wolfCritter, leftWolf.position, Quaternion.identity);
                }
                else {
                    Instantiate(wolfCritterLeft, rightWolf.position, Quaternion.identity);
                }
                currentTimer = 0f;
            }
            
            
        }
    }
}
