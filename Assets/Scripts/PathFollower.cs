using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
public class PathFollower : MonoBehaviour
{
    [SerializeField] private PathCreator _pathCreator;
    [SerializeField] private float _speed;
    
    private float _distanceTravelled;
    private bool _isGameRunning = false;
    private ActiveCars _activeCars;
    private void Start()
    {
        PauseButton.onGameStop += ChangeGameState;
        _activeCars = transform.parent.GetComponent<ActiveCars>();
    }

    private void OnEnable()
    {
        _isGameRunning = true;
        _distanceTravelled = _pathCreator.path.
            GetClosestDistanceAlongPath(_pathCreator.path.GetClosestPointOnPath(transform.position));
        
    }

    private void OnDisable()
    {
        PauseButton.onGameStop -= ChangeGameState;
    }

    private void Update()
    {
        if (_isGameRunning)
        {
            _distanceTravelled += _speed * Time.deltaTime;
            transform.position = _pathCreator.path.GetPointAtDistance(_distanceTravelled);
            transform.rotation = _pathCreator.path.GetRotationAtDistance(_distanceTravelled);
            transform.Rotate(Vector3.forward, 90);
            if (_distanceTravelled >= _pathCreator.path.length)
            {
                Destroy(this.gameObject);
                _activeCars.RemoveCarFromList(this.gameObject);
            }
        }
       
    }
    private void ChangeGameState(bool isGameRunning)
    {
        _isGameRunning = isGameRunning;
    }
}
