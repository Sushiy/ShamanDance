using UnityEngine;
using System.Collections;

public class Mag_CameraController : MonoBehaviour
{
    private GameObject _player;
    [SerializeField]
    private float cameradistance = 20f;
    [SerializeField]
    private float velocityScale = 0.5f;

    private float LerpSpeed = 3f;

    [HideInInspector]
    public bool Follow = true;

	// Use this for initialization
	void Start ()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!Follow)
            return;

        Vector2 middlePos;
        Vector2 playerPos = _player.transform.position;
        /*if (_targetSystem.SelectedTarget != null)
        {
            Vector2 targetPos = new Vector2(_targetSystem.SelectedTarget.transform.position.x, _targetSystem.SelectedTarget.transform.position.y);
            middlePos = playerPos + ( - playerPos) / 2;
        }
        else*/
            middlePos = playerPos;

        Vector3 playerV = _player.GetComponent<Rigidbody2D>().velocity;
        playerV *= velocityScale;

        Vector3 desiredPos = new Vector3(middlePos.x, middlePos.y + 3f, -cameradistance) + playerV;
        transform.position = Vector3.Lerp(transform.position, desiredPos, Time.deltaTime);

    }
}
