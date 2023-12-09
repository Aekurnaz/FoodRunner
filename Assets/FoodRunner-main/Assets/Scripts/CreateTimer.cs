using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTimer : MonoBehaviour
{
    [SerializeField] GameObject _image;
    GameObject _timer;
    void Start()
    {
        _timer = Instantiate(_image,new Vector3(0,17.5f,0) + transform.position,Quaternion.identity);
        _timer.transform.parent = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
