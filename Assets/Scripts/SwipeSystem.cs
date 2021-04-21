using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeSystem : MonoBehaviour
{
       [HideInInspector] public enum SwipeType
       {
            Left,
            Right,
            Up,
            Down
       }
       public delegate void OnSwipeInput(SwipeType type);
       public event OnSwipeInput onSwipeInput;
       
       private bool _isMobilePlatform;
       private bool _isDragging;
       private Vector2 _tapPoint, _swipeDelta;
       private float _minSwipeDelta = 50;
       private Ray _ray;
       private RaycastHit _hit;
       private Camera _mainCamera;
       private void Awake()
       {
           #if UNITY_EDITOR || UNITY_STANDLONE
            _isMobilePlatform = false;
           #else
             _isMobilePlatform = true;
           #endif
   
       }

       private void Start()
       {
           _mainCamera = Camera.main;
       }
       private void Update()
       {
           CheckPress();
       }

       private void CheckPress()
       {
           if (!_isMobilePlatform)
           {
               if (Input.GetMouseButtonDown(0))
               {
                   _ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
                   _isDragging = true;
                   _tapPoint = Input.mousePosition;
               }
               else if (Input.GetMouseButtonUp(0))
               {
                   ResetSwipe();
               }
           }
           else
           {
               if (Input.touchCount > 0)
               {
                   if (Input.touches[0].phase == TouchPhase.Began)
                   {
                       _ray = _mainCamera.ScreenPointToRay(Input.touches[0].position);
                       _isDragging = true;
                       _tapPoint = Input.touches[0].position;
                   }
                   else if (Input.touches[0].phase == TouchPhase.Canceled ||
                            Input.touches[0].phase == TouchPhase.Ended)
                   {
                       ResetSwipe();
                   }
               }
           }

           CalculateSwipe();
       }


       private void CalculateSwipe()
       {
           _swipeDelta = Vector2.zero;
           if(_isDragging)
           {
               if (!_isMobilePlatform && Input.GetMouseButton(0))
               {
                   _swipeDelta = (Vector2)Input.mousePosition - _tapPoint;
               }
               else if(Input.touchCount > 0)
               {
                   _swipeDelta = Input.touches[0].position - _tapPoint;
               }
           }
           if (_swipeDelta.magnitude > _minSwipeDelta)
           {
               SelectCar();
               
               if (Mathf.Abs(_swipeDelta.x) > Mathf.Abs(_swipeDelta.y))
               { 
                   onSwipeInput?.Invoke(_swipeDelta.x < 0 ? SwipeType.Left : SwipeType.Right);
               }
               else 
               { 
                   onSwipeInput?.Invoke(_swipeDelta.y < 0 ? SwipeType.Down : SwipeType.Up);
               }
               ResetSwipe();
           }
       }

       private void SelectCar()
       {
           if (Physics.Raycast(_ray, out _hit))
           {
               if (_hit.collider.TryGetComponent<CarSwipeChecker>(out CarSwipeChecker car))
               {
                   car.IsCarSelected = true;
               }
           }
       }

       private void ResetSwipe()
       {
           _isDragging = false;
           _tapPoint = _swipeDelta = Vector2.zero;
       }
}
