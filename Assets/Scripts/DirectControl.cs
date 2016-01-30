using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class DirectControl : MonoBehaviour
{
    [SerializeField]
    Transform controlTarget;
	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void LateUpdate ()
    {
	    if(controlTarget != null)
        {
            this.transform.position = controlTarget.transform.position;
            this.transform.rotation = controlTarget.transform.rotation;
        }
	}
}
