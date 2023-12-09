using CustomerS;
using Kitchen;
using Players;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public abstract class Timer : MonoBehaviour
{
    [SerializeField]protected Image _image;
    [SerializeField]private float _clockTimer;
    [SerializeField]private float _clockTime;

    public class IKitchen : Timer
    {

        [SerializeField] protected Fridge _fridge;
        [SerializeField] protected Own _own;
        protected virtual void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent(out Player _))
            {
                
            }
        }
        protected virtual void OnTriggerStay(Collider other)
        {
            if(other.TryGetComponent(out Player _))
                if(_clockTimer > 0)
                {
                    _clockTimer -= Time.deltaTime;
                    _image.fillAmount = (_clockTimer) / _clockTime;
                }
            else if(_clockTimer < 0)
                {
                    _clockTimer = 0;
                    _image.fillAmount = 0;
                }
            
        }
        protected virtual void OnTriggerExit(Collider other)
        {
            if(other.TryGetComponent(out Player _))
            {
                _image.fillAmount = 0;
            }
        }

        private void OnEnable()
        {
            _own.TimerCallBack += SetTimer;
            _fridge.TimerCallBack += SetTimer;
        }
        private void OnDisable()
        {
            _own.TimerCallBack -= SetTimer;
            _fridge.TimerCallBack -= SetTimer;
        }

        private void SetTimer(float _float)
        {
            _clockTimer = _float;
            _clockTime = _float;
        }
    }
    public class CustomerTimerr : Timer
    {

        [SerializeField]protected Customer _customer;


        protected void OnEnable()
        {
            _customer.TimerCallBack += SetTimer;
        }
        protected void OnDisable()
        {
            _customer.TimerCallBack -= SetTimer;
        }

        protected void SetTimer(float _float)
        {
            _clockTime = _float;
            _clockTimer = _float;
        }

        protected virtual void SetFillAmount()
        {
            if(_clockTimer > 0)
            {
                _clockTimer -= Time.deltaTime;
                _image.fillAmount = (_clockTime - _clockTimer) / _clockTime;
            }
            else if(_clockTimer < 0)
            {
                _clockTimer = 0;
            }
        }
    }
}
