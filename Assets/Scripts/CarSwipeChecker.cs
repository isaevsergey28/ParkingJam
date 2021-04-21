using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
public class CarSwipeChecker : MonoBehaviour
{
    [SerializeField] private SwipeType _swipeType;
    
    public delegate void OnCorrectSwipe(Vector3 dir);
    public event OnCorrectSwipe onCorrectSwipe;
    public bool IsCarSelected { get; set; } = false;
    private enum SwipeType
    {
        Left,
        Right,
        Up,
        Down
    }
    
    private SwipeSystem _swipeSystem;
    
    [Inject]
    private void Construct(SwipeSystem swipeSystem)
    {
        _swipeSystem = swipeSystem;
    }
    private void Start()
    {
        _swipeSystem.onSwipeInput += CheckSwipe;
    }
    private void OnDisable()
    {
        _swipeSystem.onSwipeInput -= CheckSwipe;
    }
    private void CheckSwipe(SwipeSystem.SwipeType type)
    {
        if (IsCarSelected)
        {
            if (type == (SwipeSystem.SwipeType)_swipeType )
            {
                onCorrectSwipe?.Invoke(Vector3.forward);
            }
            else
            {
                onCorrectSwipe?.Invoke(Vector3.back);
            }
            IsCarSelected = false;
        }
    }
}
