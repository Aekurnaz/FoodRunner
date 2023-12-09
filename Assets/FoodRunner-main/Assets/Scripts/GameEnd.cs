using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnd : MonoBehaviour
{
    [SerializeField] private Point _missCustomers;
    [SerializeField] private GameObject _gameEndPanel;
    // Start is called before the first frame update
    void Start()
    {
        _missCustomers.Points = 0;
    }

    // Update is called once per frame
    void Update()
    {
     if(_missCustomers.Points > 3)
        {
            _gameEndPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
