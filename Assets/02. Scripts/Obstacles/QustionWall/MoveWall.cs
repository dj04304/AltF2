using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWall : MonoBehaviour
{

    private bool _isMoving;
    [SerializeField]
    private float _speed = 3f;

    private void Start()
    {
        _isMoving = false;
        gameObject.SetActive(true);
        
        //transform .position = new Vector3(-11, 0.1f, 5);
    }
    private void Update()
    {
        if (_isMoving)
        {
            gameObject.transform.Translate(Vector3.back * _speed * Time.deltaTime);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") 
        {
            _isMoving = true;
            Invoke("hideWall", 10f);
        }
    }

    private void hideWall() 
    {
        gameObject.SetActive(false);
    }
}

