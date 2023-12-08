using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Kitchen;
using System;

namespace Players
{
public class NavMeshController : MonoBehaviour
{
    [SerializeField] private Fridge _fridge;
    [SerializeField] private GameObject _foodPanel;

    [SerializeField]private bool _isFridgeOpen;
    
    private NavMeshAgent _agent;
    //private Ray _ray;
    private Camera _camera;
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _camera = Camera.main;
    }
    

    // Update is called once per frame
    private void Update()
    {
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        if (Input.GetMouseButtonDown(0) && !_isFridgeOpen)
        {
             Ray _ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit _hit;

            if (Physics.Raycast(_ray, out _hit))
            {
                _agent.SetDestination(_hit.point);
            }
        }
    }

    private void OnEnable()
    {
        _fridge.FridgeDoorOpenCallBack += FridgeTrueCheck;
        _fridge.FridgeDoorCloseCallBack += FridgeFalseCheck;
    }

    private void OnDisable()
    {
        _fridge.FridgeDoorOpenCallBack -= FridgeTrueCheck;
        _fridge.FridgeDoorCloseCallBack -= FridgeFalseCheck;
    }

     private void FridgeTrueCheck()
    {
          _isFridgeOpen = true;
    }

    private void FridgeFalseCheck()
    {
          _isFridgeOpen = false;
    }
}
}
