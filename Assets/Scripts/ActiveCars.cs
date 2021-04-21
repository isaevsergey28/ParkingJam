using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveCars : MonoBehaviour
{
    public delegate void OnPlayerWon();
    public static event OnPlayerWon onPlayerWon;
    
    [SerializeField] private List<GameObject> _activeCars = new List<GameObject>();
    
    private void Start()
    {
        GameObject[] activeCars = GameObject.FindGameObjectsWithTag("Car");
        foreach (var car in activeCars)
        {
            _activeCars.Add(car);
        }
    }
    public void RemoveCarFromList(GameObject car)
    {
        _activeCars.Remove(car);
        if (_activeCars.Count == 0)
        {
            onPlayerWon?.Invoke();
        }
    }
}
