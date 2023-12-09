using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Players;

namespace Kitchen
{
public class Fridge : MonoBehaviour
{
    public event Action<float> TimerCallBack;
    public event Action FridgeDoorOpenCallBack;
    public event Action FridgeDoorCloseCallBack;
    public event Action HotDogSelected;
    public event Action PizzaSelected;
    public event Action LambSelected;

    [Header("Player's")]
    [SerializeField] Player _player;

    [Header("Fridge's")]
    [SerializeField] private bool _isOpen;
    [SerializeField] private GameObject _foodPanel;
    

    [Header("Timers")]
    [SerializeField] private float _openTime;
    [SerializeField] private float _openTimer;


        void Update()
    {
        FoodPanelController(_isOpen);
    }

    private void OnTriggerEnter(Collider other)
    {
       if (other.TryGetComponent(out Player _playerr))
        {
            if(!_playerr.IsCarryCookedFood && !_playerr.IsCarryUnCookedFood)
            {
                 _openTimer = _openTime;
                 TimerCallBack?.Invoke(_openTime);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
            if(other.TryGetComponent(out Player _))
            {
                TimerCount();
            }
    }
        private void OnTriggerExit(Collider other)
        {
            if(other.TryGetComponent(out Player _))
            {
                _openTimer = 0;
            }
        }

        private void TimerCount()
    {
        if(_openTimer > 0 && !_player.IsCarryCookedFood && !_player.IsCarryUnCookedFood)
        {
            _openTimer -= Time.deltaTime;
        }
        else if (_openTimer < 0 && !_player.IsCarryCookedFood && !_player.IsCarryUnCookedFood)
        {
            _isOpen = true;
            FridgeDoorOpenCallBack?.Invoke();
        }
    }

    private void FoodPanelController(bool _isFridgeOpen)
    {
        if(_isFridgeOpen)
        {
            _foodPanel.SetActive(true);
        }
        else
        { 
            _foodPanel.SetActive(false); 
        }
    }

    public void Hotdog()
    {
        FridgeDoorCloseCallBack?.Invoke();
        HotDogSelected?.Invoke();
        _isOpen =false;
        _openTimer = 0;
    }

    public void Pizza()
    {
        FridgeDoorCloseCallBack?.Invoke();
        PizzaSelected?.Invoke();
        _isOpen =false;
        _openTimer = 0;
    }

    public void Lamb()
    {
        FridgeDoorCloseCallBack?.Invoke();
        LambSelected?.Invoke();
        _isOpen =false;
        _openTimer = 0;
    }
}
}