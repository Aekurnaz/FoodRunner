using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomerS;

namespace CustomerS
{
    public class CustomerSpawner : MonoBehaviour
    {
        [SerializeField] private Table[] _tablePos;
        [SerializeField] private GameObject _customer;
        [SerializeField] private bool _canSpawn;
        public int TableId;
        // Start is called before the first frame update
        private void Awake()
        {
            for(int i = 0;  i < _tablePos.Length; i++)
            {
                _tablePos[i].IsTableAvaible = true;
            }
        }
        void Start()
        {
            StartCoroutine(CustomerSpawnerr());
        }

        // Update is called once per frame
        void Update()
        {
            CheckAvaibleTable();
        }

        IEnumerator  CustomerSpawnerr()
        {
            while (_canSpawn)
            {
                    Instantiate(_customer, new Vector3(-48, 0, -41), Quaternion.identity);
                    _canSpawn = false;
                    yield return new WaitForSeconds(5f);
            }
        }

        private void CheckAvaibleTable()
        {
            for(int i=0; i< _tablePos.Length; i++)
            {
                if (_tablePos[i].IsTableAvaible)
                {
                    _canSpawn = true;
                    break;
                }
                else { }
            }
        }
    }
}