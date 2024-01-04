using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class SavePoint : MonoBehaviour
{
    private Vector3 _startPoint; // ������ġ ����.
    private Vector3 _firstStartPoint = new Vector3(10,10,10); // 1�������� ������ġ ����.
    private Vector3 _SecondStartPoint = new Vector3(94, 0, 15); // 2�������� ������ġ ����.

    private Vector3 _savePoint = Vector3.zero;  // ������ġ ����.

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();   
    }
    private void Start()
    {
        SceneManager.sceneLoaded += LoadedsceneEvent;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R)) 
        {
            StartCoroutine(ReStartCo());
        }
    }

    private void LoadedsceneEvent(Scene scene, LoadSceneMode arg1)
    {
        sceneCheck(scene);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(_savePoint);
        if (collision.gameObject.tag == "SaveBoard")
        {
            _savePoint = collision.transform.position + Vector3.up;
            //���̺� ����Ʈ
            Destroy(collision.gameObject,1f);
        }
    }
    
    IEnumerator ReStartCo()
    {
        //������Ʈ
        _animator.enabled = false;
        yield return new WaitForSeconds(1f);
        gameObject.transform.position = _savePoint;
        _savePoint = _startPoint;
        _animator.enabled = true;
    }

    private void sceneCheck(Scene scene) 
    {
        if (scene.name == "KJW")
        {
            _startPoint = _firstStartPoint;
        }else if (scene.name == "99.BJH")
        {
            _startPoint = _SecondStartPoint;
        }
    }
}
