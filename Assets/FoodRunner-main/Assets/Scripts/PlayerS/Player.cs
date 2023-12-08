using System;
using System.Collections;
using System.Collections.Generic;
using Kitchen;
using CustomerS;
using Unity.VisualScripting;
using UnityEngine;

namespace Players
{
public class Player : MonoBehaviour
{
    [SerializeField] Fridge _fridge;
    [SerializeField] Own _own;
    [SerializeField] Customer _customer;
    [SerializeField]private bool _isCarryUnCookedFood;
    [SerializeField]private bool _isCarryCookedFood;

    [SerializeField ]private int _foodId;
    [SerializeField] public int CookedFoodID;
    public int FoodID { get => _foodId; }
    public bool IsCarryCookedFood
        {get => _isCarryCookedFood;
        }
    public bool IsCarryUnCookedFood
        {get => _isCarryUnCookedFood;
        }

        private void OnTriggerStay(Collider other)
        {
            if(other.TryGetComponent(out Customer _customer))
            {
                if (_customer.OrderDelivered == true)
                {
                    _foodId = 0;
                    _isCarryCookedFood = false;
                    _isCarryUnCookedFood = false;
                }
            }
        }
        private void OnEnable()
        {
            _own.CarryUnCookedFood += NotCarryUnCookedFood;
            _own.CollectedCallBack += CarryCookedFood;
            _own.CookedFoodID += CookedFoodId;
            _fridge.FridgeDoorCloseCallBack += CarryUnCookedFood;
            _fridge.HotDogSelected += HotDogId;
            _fridge.PizzaSelected += PizzaId;
            _fridge.LambSelected += LambId;
        }

        private void OnDisable()
        {
            _own.CarryUnCookedFood -= NotCarryUnCookedFood;
            _own.CollectedCallBack -= CarryCookedFood;
            _own.CookedFoodID -= CookedFoodId;
            _fridge.FridgeDoorCloseCallBack -= CarryUnCookedFood;
            _fridge.HotDogSelected -= HotDogId;
            _fridge.PizzaSelected -= PizzaId;
            _fridge.LambSelected -= LambId;
        }

        private void CarryUnCookedFood()
        {
            _isCarryUnCookedFood = true;
        }

        private void NotCarryUnCookedFood()
        {
            _isCarryUnCookedFood = false;
            _foodId = 0;
        }

        private void CarryCookedFood()
        {
            _isCarryCookedFood = true;
        }
        private void NotCarryCookedFood()
        {
            _isCarryCookedFood = false;
            _foodId = 0;
        }
        private void HotDogId()
        {
            _foodId = 1;
        }
        private void LambId()
        {
            _foodId = 2;
        }
        private void PizzaId()
        {
            _foodId = 3;
        }
        private void CookedFoodId()
        {
            _foodId = CookedFoodID;
        }
    }
}