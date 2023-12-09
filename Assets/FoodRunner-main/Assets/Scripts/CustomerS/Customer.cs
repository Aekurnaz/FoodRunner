using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using CustomerS;
using Players;
using System;
using DG.Tweening;
using UnityEngine.UI;

namespace CustomerS
{
    public class Customer : MonoBehaviour
    {
        public event Action<float> TimerCallBack;
        public event Action<int> EarnPoint;
        [SerializeField] private Table[] _tables;
        [SerializeField] private Food[] _foods;
        [SerializeField] private Point _points;
        [SerializeField] private Point _missingCustomer;
        private int _rand;
        public int Foodindex { get => _rand + 1; }
        private GameObject _randFood;
        private CustomerSpawner _spawner;
        private NavMeshAgent _customer;
        private int _tableIndex;
        private CapsuleCollider _boxCollider;
        [Header("Customer's Behaviours")]
        private bool _isArrive;
        [SerializeField] private bool _isSitting;
        public bool Sitting { get => _isSitting; }
        private bool _isStartCounting;
        [SerializeField] private float _waitTimeToGiveOrder;
        [SerializeField] private float _waitTimerToGiveOrder;
        [SerializeField] private bool _isOrderTaken;
        [SerializeField] private float _orderTakenTime;
        [SerializeField] private float _orderTakenTimer;
        [SerializeField] private bool _isOrderArrive;
        [SerializeField] private float _orderArriveWaitTime;
        [SerializeField] private float _orderArriveWaitTimer;
        [SerializeField] private float _reciveOrderTime;
        [SerializeField] private float _reciveOrderTimer;
        [SerializeField] private bool _orderDelivered;
        public bool OrderDelivered { get => _orderDelivered; }
        [SerializeField] private bool _isOrderTrue;
        [SerializeField] private bool _didEat;
        [SerializeField] private float _eatTime;
        [SerializeField] private float _eatTimer;

        private Animator _anim;
        private bool _pointChanged;
        private bool _orderDeliveredPoints;
        private bool _orderWaitPoints;

        [Header("Images")]
        private Image _image;
        private void Awake()
        {
            _spawner = GetComponent<CustomerSpawner>();
            _customer = GetComponent<NavMeshAgent>();
            _boxCollider = GetComponent<CapsuleCollider>();
            _rand = UnityEngine.Random.Range(0, _foods.Length);
            _anim = GetComponent<Animator>();
        }
        void Start()
        {
            CustomerMovement();
        }

        // Update is called once per frame
        void Update()
        {
            CheckPos();
            WaitToGiveOrder();
            WaitingForOrder();
            Eating();

            if (_isSitting)
            {
                _anim.SetBool("Sitting", true);
            }
            else
            {
                _anim.SetBool("Sitting", false);
            }
        }

        private void CustomerMovement()
        {
            for (int i = 0; i < _tables.Length; i++)
            {
                {
                    if (_tables[i].IsTableAvaible)
                    {
                        _customer.SetDestination(_tables[i].TablePos);
                        _tables[i].IsTableAvaible = false;
                        _tableIndex = i;
                        break;
                    }
                }

            }
        }
        private void CheckPos()
        {
            if (transform.position.x == _tables[_tableIndex].TablePos.x)
            {
                _customer.SetDestination(_tables[_tableIndex].TablePos + new Vector3(0, 0, -11));
                transform.position = _tables[_tableIndex].TablePos + new Vector3(0, 0, -11);
                _boxCollider.enabled = true;
                _isSitting = true;
                _isArrive = true;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player _playerr))
            {
                if (_isArrive && !_isOrderTaken)
                {
                    _orderTakenTimer = _orderTakenTime;

                }
                else if (_isOrderTaken && (_playerr.IsCarryCookedFood || _playerr.IsCarryUnCookedFood))
                {
                    _reciveOrderTimer = _reciveOrderTime;
                }
            }
        }
        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent(out Player _playerr))
            {
                if (!_isOrderTaken)
                {
                    TakingOrder();
                }
                else if (_isOrderTaken && (_playerr.IsCarryCookedFood || _playerr.IsCarryUnCookedFood))
                {
                    RecivingOrder();
                    if (_orderDelivered)
                    {
                        if (_playerr.FoodID == Foodindex)
                        {
                            _isOrderTrue = true;
                            _randFood.transform.localPosition = _randFood.transform.localPosition + new Vector3(0, -6, 5);
                            _randFood.transform.localScale = new Vector3(7, 7, 7);
                            _randFood.SetActive(true);
                            if(!_orderDeliveredPoints)
                            {
                                _points.Points += 20;
                                _orderDeliveredPoints = true;
                            }
                        }
                        else
                        {
                            Debug.Log("yanlýs siparis");
                            _isSitting = false;
                            _isOrderTrue = false;
                            //leave as angry
                            _boxCollider.enabled = false;
                            _customer.obstacleAvoidanceType = 0;
                            transform.position += new Vector3(-1, 0, -1);
                            _customer.SetDestination(new Vector3(-44, 0, -40));
                            Destroy(this.gameObject, 5);
                            Destroy(_randFood);
                            if (!_orderDeliveredPoints)
                            {
                                _points.Points -= 10;
                                _missingCustomer.Points += 1;
                                _orderDeliveredPoints = true;
                            }
                        }
                    }
                }
            }
        }


        private void WaitToGiveOrder()
        {
            if (!_isOrderTaken && _isSitting && !_isStartCounting)
            {
                _isStartCounting = true;
                _waitTimerToGiveOrder = _waitTimeToGiveOrder;
                if (TimerCallBack == null)
                {
                    Debug.Log("Null");
                }
                else
                {
                    TimerCallBack.Invoke(_waitTimerToGiveOrder);
                }
                _randFood = Instantiate(_foods[_rand]._Food);
                _randFood.transform.localPosition += transform.position + new Vector3(0, 10, 0);
            }
            else if (!_isOrderTaken && _isStartCounting && _waitTimerToGiveOrder > 0)
            {
                _waitTimerToGiveOrder -= Time.deltaTime;
            }
            else if (!_isOrderTaken && _waitTimerToGiveOrder < 0 && _isStartCounting)
            {
                if(!_pointChanged)
                {
                    _points.Points -= 10;
                    _missingCustomer.Points += 1;
                    _pointChanged = true;
                }
                _isSitting = false;
                //leave as angry
                _boxCollider.enabled = false;
                _customer.obstacleAvoidanceType = 0;
                _customer.SetDestination(new Vector3(-44, 0, -40));
                Destroy(this.gameObject, 5);
                Destroy(_randFood);
            }
        }
        private void TakingOrder()
        {
            if (_orderTakenTimer > 0)
            {
                _orderTakenTimer -= Time.deltaTime;
            }
            else if (_orderTakenTimer < 0)
            {
                _isOrderTaken = true;
                _randFood.SetActive(false);
                _orderTakenTimer = 0;
                _orderArriveWaitTimer = _orderArriveWaitTime;
                if (TimerCallBack == null)
                {
                    Debug.Log("Null");
                }
                else
                {
                    TimerCallBack.Invoke(_orderArriveWaitTime);
                }

            }
        }
        private void WaitingForOrder()
        {
            if (_orderArriveWaitTimer > 0 && _isOrderTaken && !_isOrderArrive && !_orderDelivered)
            {
                _orderArriveWaitTimer -= Time.deltaTime;
            }
            else if (_orderArriveWaitTimer < 0 && _isOrderTaken)
            {
                _isSitting = false;
                //Customer leaves as Angry
                _boxCollider.enabled = false;
                _customer.obstacleAvoidanceType = 0;
                _customer.SetDestination(new Vector3(-44, 0, -40));
                Destroy(this.gameObject, 5);
                Destroy(_randFood);
                if(!_orderWaitPoints)
                {
                    _points.Points -= 10;
                    _orderArriveWaitTimer += 1;
                    _orderWaitPoints = true;
                }
            }
        }

        private void RecivingOrder()
        {
            if (_reciveOrderTimer > 0)
            {
                _reciveOrderTimer -= Time.deltaTime;
            }
            else if (_reciveOrderTimer < 0)
            {
                _orderDelivered = true;
            }
        }

        private void Eating()
        {
            if (_isOrderTrue && _eatTimer == 0 && !_didEat)
            {
                _eatTimer = _eatTime;
                if (TimerCallBack == null)
                {
                    Debug.Log("Null");
                }
                else
                {
                    TimerCallBack.Invoke(_eatTime);
                }
            }
            else if (_eatTimer > 0)
            {
                _eatTimer -= Time.deltaTime;
            }
            else if (_eatTimer < 0)
            {
                _didEat = true;
                _eatTimer = 0;
                _isSitting = false;
                transform.position += new Vector3(-1, 0, -1);
                _customer.SetDestination(new Vector3(-44, 0, -40));
                Destroy(this.gameObject, 5);
                // Destroy(_randFood);
            }
        }
        private void OnDestroy()
        {
            _tables[_tableIndex].IsTableAvaible = true;
        }
    }

}