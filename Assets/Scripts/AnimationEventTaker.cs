using UnityEngine;
using System.Collections;

public class AnimationEventTaker : MonoBehaviour
{
    CharacterMovement movement;

    void Start()
    {
        movement = transform.parent.GetComponent<CharacterMovement>();
    }
	public void Jump()
    {
        movement.Jump();
    }
}
