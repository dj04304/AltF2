using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private float _time;
    private float _delay =0.5f;
    private PlayerController _playerController;

    private Animator _animator;

    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody =GetComponent<Rigidbody>();
        _playerController =GetComponent<PlayerController>();
    }
    private void Update()
    {
        WalkSound();
    }
    public void LandingSound() 
    {
        GameManager.Sound.SFXPlay(("landing"), gameObject.transform.position, 0.1f);
    }
    public void WalkSound()
    {
        _time += Time.deltaTime;
        if (Mathf.Abs(_rigidbody.velocity.y)<0.1f&& Mathf.Abs(_rigidbody.velocity.magnitude)> 0.1f)
        {
            if (_playerController.IsGrounded()&& _animator.enabled)
            {
                if (_time > _delay)
                {
                    GameManager.Sound.SFXPlay(("Footstep"), gameObject.transform.position, 0.1f);
                    _time = 0;
                }
            }
            
        }
    }


}
