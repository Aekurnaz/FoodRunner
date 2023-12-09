using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointChange : MonoBehaviour
{
    private event Action PointChanged;

    [SerializeField] Animator _anim;
    [SerializeField] Point _point;
    private int _oldPoint;
    [SerializeField] TextMeshProUGUI _text;
    // Start is called before the first frame update
    void Start()
    {
        _point.Points = 0;
    }

    // Update is called once per frame
    void Update()
    {
        PointChangeCheck();
    }

    private void OnPointChange()
    {
        _anim.SetTrigger("Changed");
        _text.text =_point.Points.ToString();
    }

    private void PointChangeCheck()
    {
        if(_oldPoint != _point.Points)
        {
            _oldPoint = _point.Points;
            PointChanged?.Invoke();
        }
        
    }
    private void OnEnable()
    {
        PointChanged += OnPointChange;
    }
    private void OnDisable()
    {
        PointChanged -= OnPointChange;
    }
}
