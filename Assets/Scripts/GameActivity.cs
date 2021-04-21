using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameActivity : MonoBehaviour
{
     [SerializeField] private GameObject _stopButton;
     [SerializeField] private GameObject _startPanel;
     [SerializeField] private GameObject _lossPanel;
     [SerializeField] private GameObject _winPanel;
     
     private bool _isPlayerWon = false;
     private PauseButton _pauseButton;
     private void Start()
     {
          _pauseButton = _stopButton.GetComponent<PauseButton>();
          _stopButton.SetActive(false);
          ActiveCars.onPlayerWon += PassLevel;
     }

     private void OnDisable()
     {
          ActiveCars.onPlayerWon -= PassLevel;
     }

     public void StartGame()
     {
          _stopButton.SetActive(true);
          _pauseButton.StartGame();
          _startPanel.gameObject.SetActive(false);
     }
     public void StopGame()
     {
          _stopButton.SetActive(false);
          _pauseButton.StopGame();
          if (_isPlayerWon)
          {
               _winPanel.SetActive(true);
          }
          else
          {
               _lossPanel.SetActive(true);
          }
     }

     private void PassLevel()
     {
          _isPlayerWon = true;
          StopGame();
     }
}
