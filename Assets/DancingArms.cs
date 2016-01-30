using UnityEngine;
using System.Collections;

public class DancingArms : MonoBehaviour
{
    [SerializeField]
    Transform leftHandT;
    [SerializeField]
    Transform rightHandT;

    [SerializeField]
    Transform leftIdleT;
    [SerializeField]
    Transform rightIdleT;

    [SerializeField]
    Transform torsoT;

    float radius = 3;
    float speed = 10;
    float maxAngle = 15;
    float rotationSpeed = 4f;

    bool swingBack = false;

    float T = 0;
    // Use this for initialization
    void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
        float lx = Input.GetAxis("Horizontal");
        float ly = Input.GetAxis("Vertical");

        float rx = Input.GetAxis("Rx");
        float ry = -Input.GetAxis("Ry");

        //leftHandT.localPosition = leftIdleT.localPosition + radius * new Vector3(lx, ly, 0);
        //rightHandT.localPosition = rightIdleT.localPosition + radius * new Vector3(rx, ry, 0);
        leftHandT.localPosition = Vector3.Lerp(leftHandT.localPosition, leftIdleT.localPosition + radius * new Vector3(lx, ly, 0), Time.deltaTime * speed);
        rightHandT.localPosition = Vector3.Lerp(rightHandT.localPosition, rightIdleT.localPosition + radius * new Vector3(rx, ry, 0), Time.deltaTime * speed);
        
    }
}
