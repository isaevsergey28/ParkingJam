using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    private bool _isMoving = false;
    private CarSwipeChecker _carSwipeChecker;
    private Vector3 _moveDirection;
    private bool _isGameRunning = false;
    private void Start()
    {
        _carSwipeChecker = GetComponent<CarSwipeChecker>();
        _carSwipeChecker.onCorrectSwipe += Move;
        PauseButton.onGameStop += ChangeGameState;
    }

    private void Move(Vector3 moveDirection)
    {
        if (_isGameRunning)
        {
            _isMoving = true;
            _moveDirection = moveDirection;
            transform.Translate(_moveDirection);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fence") || other.CompareTag("Car"))
        {
            Move(_moveDirection * -1);
            _isMoving = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("ParkingLot"))
        {
            if (_isMoving)
            {
                Move(_moveDirection);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ParkingLot"))
        {
            GetComponent<PathFollower>().enabled = true;
        }
    }
    private void ChangeGameState(bool isGameRunning)
    {
        _isGameRunning = isGameRunning;
    }
}
