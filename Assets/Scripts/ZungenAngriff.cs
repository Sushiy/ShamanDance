using UnityEngine;
using System.Collections;

public class ZungenAngriff : MonoBehaviour
{
    [SerializeField]
    Transform tongue_target;
    LineRenderer line;
	// Use this for initialization
	void Start ()
    {
        line = GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        line.SetPosition(0, transform.position);
        line.SetPosition(1, tongue_target.position);
	}
}
