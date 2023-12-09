using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CustomerS
{
    public class SetRotation : MonoBehaviour
    {
        [SerializeField]private Customer _customer;
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            if (_customer.Sitting == true)
            {
                transform.rotation = new Quaternion(0, 0, 0, transform.rotation.w);
            }
            else
            { transform.localRotation = new Quaternion(0, 0, 0, transform.rotation.w); }
        }
    }
}