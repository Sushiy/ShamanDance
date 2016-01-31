﻿using UnityEngine;
using System.Collections;

public class SeekBehaviour : MonoBehaviour {


    private bool _isPlayerInSight;
    private bool _isMovingForward;
    private bool _hasReachedPlayer;
    private int  direction;

    public bool hasReachedPlayer { get { return _hasReachedPlayer; }}
    public bool isPlayerInSight { get { return _isPlayerInSight; } }

	// Use this for initialization
	void Start () {

        
    }
	
	// Update is called once per frame
	void Update () {
        if(_isPlayerInSight && _isMovingForward)
           transform.Translate(new Vector3(direction * 3f * Time.deltaTime, 0f, 0f));

    }

    void OnTriggerStay2D(Collider2D other) {
    
      if (other.CompareTag("Player")) {
            _isPlayerInSight = true;
           Vector3 movingDirection = other.transform.position - transform.position;
            direction = movingDirection.x < 0 ? -1 : 1;
            _isMovingForward = true;
            _hasReachedPlayer = false;
            if (movingDirection.magnitude <= 8) {
                _isMovingForward = false;
                _hasReachedPlayer = true;
            }

    
      


       }
    }


}
