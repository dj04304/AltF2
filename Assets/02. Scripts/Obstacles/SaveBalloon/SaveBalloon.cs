using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SaveBalloon : MonoBehaviour
{
    [Header("Speed")]
    public float speed;
    
    private Animator _animator;
    private Rigidbody _rigidbody;


    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _rigidbody = GetComponentInChildren<Rigidbody>();
    }

    private void Start()
    {
       // �������ڸ��� �ö󰡴� �κ� ����
    }

    public IEnumerator Pop(GameObject gameObj)
    {
        _animator.SetTrigger("Pop");
        yield return new WaitForSeconds(1f);

        _rigidbody.useGravity = true;
        SavePosition();

        Destroy(gameObj);
    }

    private void SavePosition()
    {
        // position ����
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            _rigidbody.velocity = Vector3.up * speed;
        }
    }

}
