using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TimerForRoundSystem : MonoBehaviour
{
   [SerializeField] private float _timerForRound;
   [SerializeField] private GameObject _gameActivityObject;
   private GameActivity _gameActivity;
   
   private Text _timeText;
   private bool _isGameRunning = false;
   private void Start()
   {
      _timeText = GetComponent<Text>();
      _timeText.text = ((int)_timerForRound).ToString();
      PauseButton.onGameStop += ChangeGameState;
      _gameActivity = _gameActivityObject.GetComponent<GameActivity>();
   }

   private void OnDisable()
   {
      PauseButton.onGameStop -= ChangeGameState;
   }

   private void Update()
   {
      ChangeTimer();
   }

   private void ChangeTimer()
   {
      if (_isGameRunning)
      {
         if (_timerForRound == 0)
         {
            _gameActivity.StopGame();
         }
         _timerForRound -= Time.deltaTime;
         _timerForRound = Mathf.Clamp(_timerForRound, 0, 30);
         _timeText.text = ((int) _timerForRound).ToString();
      }
   }

   private void ChangeGameState(bool isGameRunning)
   {
      _isGameRunning = isGameRunning;
   }
}
