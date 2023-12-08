using System;
using System.Collections;
using System.Collections.Generic;
using Players;
using Unity.VisualScripting;
using UnityEngine;

namespace Kitchen
{
public class Own : MonoBehaviour
{
    public event Action CookedCallBack;
    public event Action CollectedCallBack;
    public event Action CarryUnCookedFood;
    public event Action CookedFoodID;

    [Header("Food's")]
    
    [SerializeField] private bool _isCooking;
    [SerializeField] private bool _cooked;
    [SerializeField] private int _foodId;
    

    [Header("Timers")]
    [SerializeField] private float _playerToOwnTime;
    [SerializeField] private float _playerToOwnTimer;
    [SerializeField] private float _cookingTime;
    [SerializeField] private float _cookingTimer;
    [SerializeField]  private float _collectedFoodTime;
    [SerializeField]  private float _collectedFoodTimer;

    [SerializeField] private Fridge _fridge;
    [SerializeField ] private Player _player;

        
        private void Update() {
        Cooking();
        }
    private void OnTriggerEnter(Collider other) {
        if(other.TryGetComponent(out Player _))
        {
            if(_player.IsCarryUnCookedFood && (!_isCooking && !_cooked))
            {
                _playerToOwnTimer = _playerToOwnTime;
            }
            else if(_cooked ==true && !_player.IsCarryUnCookedFood && !_player.IsCarryCookedFood)
            {
                _collectedFoodTimer = _collectedFoodTime;   
            }
        }
    }
    private void OnTriggerStay(Collider other) {
        if(other.TryGetComponent(out Player _))
        {
            if(_cooked)
            {
                CollectingFoodTimer();
            }
        
            if(_player.IsCarryUnCookedFood)
            {
                  ToOwnTimer();
            }
        }
    }

    private void Cooking()
    {
        if(_isCooking == true)
        {
            if(_cookingTimer > 0)
            {
                _cookingTimer -= Time.deltaTime;
            }
            else if( _cookingTimer < 0)
            {
                _isCooking = false;
                _cooked = true;
                _cookingTimer=0;
                CookedCallBack?.Invoke();
            }
        }
    }

    private void CollectingFoodTimer()
    {
        if(_collectedFoodTimer > 0 )
            {
                _collectedFoodTimer -= Time.deltaTime;
            }
            else if(_collectedFoodTimer < 0 && _cooked)
            {
                _isCooking = false;
                _cooked = false;
                _player.CookedFoodID = _foodId;
                _foodId = 0;
                CookedFoodID?.Invoke();
                CollectedCallBack?.Invoke();
            }
    }    private void ToOwnTimer()
    {
        if(_playerToOwnTimer> 0)
        {
            _playerToOwnTimer -= Time.deltaTime;
        }
        else if(_playerToOwnTimer < 0)
        { 
            _isCooking = true;
            _playerToOwnTimer = 0;
            _cookingTimer = _cookingTime;
            if(CarryUnCookedFood == null)
            {
                Debug.Log("Null");
            }
            else
            {
                _foodId = _player.FoodID;
                CarryUnCookedFood.Invoke();
            }
             
        }
    }
}
}