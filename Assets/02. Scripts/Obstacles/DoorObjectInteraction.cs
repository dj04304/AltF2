using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class DoorObjectInteraction : BounceObstacle
{
    [Header("- Rot")]
    [SerializeField] private float rotX;
    [SerializeField] private float rotY;
    [SerializeField] private float rotZ;

    [Header("- time")]
    [SerializeField] private float waitTime;

    [Header("- speed")]
    [SerializeField] private float duration;  // ȸ���� �ɸ��� �ð�

    [Header("- force")]
    [SerializeField] private float addForce;

    private Rigidbody _rigidBody;
    private GameObject _gameObject;

   private float angleErrorRange = 1.0f;

    private Quaternion _objectRotation;
    private bool _isInteracting = false;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _gameObject = _rigidBody.gameObject;
        _objectRotation = gameObject.transform.rotation;

        Debug.Log(_objectRotation.y);
        Debug.Log(gameObject.transform.rotation.y);
    }

    private void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);

        if (collision.gameObject.CompareTag("Player")) //&& Quaternion.Equals(_gameObject.transform.rotation, _objectRotation)
        {
            float angleDifference = Quaternion.Angle(_gameObject.transform.rotation, _objectRotation);

            //StartCoroutine(PlayerComponentControl(collision));

            DoorPushForce();

            if (angleDifference < angleErrorRange)
            {
                Debug.Log("???");
                // ���⿡ ���� ���� ���� ���� �߰�
                StartCoroutine(InteractDoor());
            }
            //InteractDoor();
            //StartCoroutine(InteractDoor());
        }
    }


    /// <summary>
    /// �ڷ�ƾ�� �̿��� ���� ȸ��
    /// </summary>
    /// <returns></returns>
    private IEnumerator InteractDoor()
    {
        _isInteracting = true;

        Quaternion currentRotation = _gameObject.transform.rotation; // ������Ʈ�� ���� rotaion��
        
        Quaternion targetRotation = Quaternion.Euler(currentRotation.eulerAngles + new Vector3(rotX, rotY, rotZ)); // ���� ���ϴ� rotation��

        float elapsedTime = 0f;

        
        
        // ȸ�� �ð�
        while (elapsedTime < duration)
        {
            // Quaternion.Lerp ���ʹϾ��� �߰��� ���� ��ȯ�Ͽ� �ε巯�� ȿ���� ��, Quaternion.Lerp(���� ���ʹϾ�, Ÿ�� ���ʹϾ�, 0~1���� �ִ� �ð�)
            _gameObject.transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        _gameObject.transform.rotation = targetRotation;


        /// 2�ʰ� ���
        yield return new WaitForSeconds(waitTime);  

        // ��ȸ��
        Quaternion reverseRotation = Quaternion.Euler(targetRotation.eulerAngles + new Vector3(rotX, -rotY, rotZ));
        elapsedTime = 0f;

        // ��ȸ�� �ð�
        while (elapsedTime < duration)
        {
            _gameObject.transform.rotation = Quaternion.Lerp(targetRotation, reverseRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _gameObject.transform.rotation = _objectRotation;  // �ʱ� ��ġ�� ���ƿ�
        _isInteracting = false;  // ��ȣ�ۿ� ����
    }


    private void DoorPushForce()
    {
        float zValue = 1f;

        Vector3 forceDirection = _gameObject.transform.forward;
        forceDirection.x = 0;
        forceDirection.z = zValue;

        _rigidBody.AddForce(forceDirection.normalized * addForce, ForceMode.Impulse);
        Debug.Log(forceDirection.normalized * addForce);

    }


    //private IEnumerator PlayerComponentControl(Collision collision)
    //{
    //    collision.gameObject.GetComponent<Animator>().enabled = false;
    //    collision.gameObject.GetComponent<PlayerController>().enabled = false;

    //    yield return new WaitForSeconds(1.1f);

    //    collision.gameObject.GetComponent<Animator>().enabled = true;
    //    collision.gameObject.GetComponent<PlayerController>().enabled = true;

    //}

}
