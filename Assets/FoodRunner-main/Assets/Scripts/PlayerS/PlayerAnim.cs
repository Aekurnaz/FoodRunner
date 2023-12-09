using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Players
{
    public class PlayerAnim : MonoBehaviour
    {
        private Animator _anim;
        private Player _player;
        private Vector3 _oldPos;
        private void Awake()
        {
            _anim = GetComponent<Animator>();
            _player = GetComponent<Player>();
        }

        private void Update()
        {
            checkIdle();
        }
        private void OnEnable()
        {
            _player.Carry += Carry;
            _player.NotCarry += NotCarry;
        }
        private void OnDisable()
        {
            _player.Carry -= Carry;
            _player.NotCarry -= NotCarry;
        }

        private void Carry()
        {
            _anim.SetBool("Carry", true);
        }
        private void NotCarry()
        {
            _anim.SetBool("Carry", false);
        }

        private void checkIdle()
        {
            if(_oldPos == transform.position)
            {
                _anim.SetBool("Idle", true);
            }
            else
            {
                _anim.SetBool("Idle", false);
                _oldPos = transform.position;
            }
        }
    }
}
