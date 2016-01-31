using UnityEngine;
using System.Collections;

public class WaterLog : MonoBehaviour
{
    [SerializeField]
    private float maxVelocity = 2f;

    Vector3 waterlevel;
    private float waterlevelYpos;
    private WaterHole hole;
    private float maxHeight;
    private float maxWidth;
    //private GameObject log;

    void Start()
    {
        maxHeight = GetComponent<BoxCollider2D>().size.y;
        maxWidth = GetComponent<BoxCollider2D>().size.x;
        hole = GetComponent<WaterHole>();
    }

    void Update()
    {
        // Update the waterlevel
        waterlevel = transform.position + hole.fillHeight * maxHeight * Vector3.up;

        //if (log != null) {
        //    LiftLog(log);
        //}
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Log") {
            LiftLog(other.gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Log") {
            LiftLog(other.gameObject);
        }
    }

    void LiftLog(GameObject log)
    {
        // is the logs y-level lower than the waterlevel?
        if (log.transform.position.y < waterlevel.y) {
            Debug.Log("Lifting Log");
            // seek behaviour!
            Seek(log);

        }
        else{
            //smoothdamp it to zero
        }
        //    // is the log fully in the water?
        //else {
        //    float wMin = transform.position.x - maxWidth / 0.5f;
        //    float wMax = transform.position.x + maxWidth / 0.5f;
        //    float lMin = log.transform.position.x - log.GetComponent<BoxCollider2D>().size.x * 0.5f;
        //    float lMax = log.transform.position.x + log.GetComponent<BoxCollider2D>().size.x * 0.5f;
        //    if (lMin > wMin && lMax < wMax) {
        //        Seek(log);
        //    }
        //}
    }

    void Seek(GameObject log)
    {
        float diff = waterlevel.y - log.transform.position.y;
        Vector2 velocity = new Vector2(0, diff).normalized * maxVelocity;
        log.GetComponent<Rigidbody2D>().velocity = velocity;
    }
}
