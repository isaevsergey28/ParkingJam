using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
public class PauseButton : MonoBehaviour, IPointerDownHandler
{
   
    public delegate void OnGameStop(bool isGameRunning);
    public static event OnGameStop onGameStop;
    
    public Sprite stopIcon;
    public Sprite startIcon;
    private bool _isGameRunning = false;
    public void StartGame()
    {
        _isGameRunning = true;
        onGameStop?.Invoke(_isGameRunning);
    }
    public void StopGame()
    {
        _isGameRunning = false;
        onGameStop?.Invoke(_isGameRunning);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if(_isGameRunning)
        {
            GetComponent<Image>().sprite = startIcon;
            _isGameRunning = false;
        }
        else
        {
            GetComponent<Image>().sprite = stopIcon;
            _isGameRunning = true;
        }
        onGameStop?.Invoke(_isGameRunning);
    }
}
