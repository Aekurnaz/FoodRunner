using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomerS;

namespace CustomerS
{
    public class CustomerSpawner : MonoBehaviour
    {
        [SerializeField] GameObject _customer;
        [SerializeField] Table[] _tables;
        [SerializeField] bool _canSpawn;
        [SerializeField] Vector3 _spawnPoint;
        [SerializeField] float _spawnTimer;
        [SerializeField] private float _spawnTime = 0;

        private void Awake()
        {
            for(int i=0; i< _tables.Length; i++)
            {
                _tables[i].IsTableAvaible = true;
            }
        }
        void Update()
        {
            CheckTables();
            TimerCount();
            CheckSpawn();
        }

        private void CheckTables()
        {
            for (int i = 0; i < _tables.Length; i++)
            {
                if (_tables[i].IsTableAvaible == true && _spawnTime <= 0)
                {
                    _spawnTime = _spawnTimer;
                    _canSpawn = true;
                    break;
                }
                else { continue; }

            }
        }

        private void TimerCount()
        {
            if (_spawnTime > 0)
            {
                _spawnTime -= Time.deltaTime;
            }
        }
        private void CheckSpawn()
        {
            if (_canSpawn == true && _spawnTime <= 0)
            {
                Instantiate(_customer, _spawnPoint, Quaternion.identity);
                _canSpawn = false;
            }
        }
    }
}