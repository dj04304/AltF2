using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class DoorObjectInteraction : MonoBehaviour
{
    [Header("- Rot")]
    [SerializeField] private float rotX;
    [SerializeField] private float rotY;
    [SerializeField] private float rotZ;

    [Header("- time")]
    [SerializeField] private float waitTime;

    private Rigidbody _rigidBody;
    private GameObject _gameObject;

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
        if (collision.gameObject.CompareTag("Player") && Quaternion.Equals(_gameObject.transform.rotation, _objectRotation))
        {
            Debug.Log("???");
            //InteractDoor();
            StartCoroutine(InteractDoor());

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
        
        Quaternion targetRotation = Quaternion.Euler(currentRotation.eulerAngles + new Vector3(rotX, -rotY, rotZ)); // ���� ���ϴ� rotation��

        float elapsedTime = 0f;
        float duration = 0.3f; // ȸ���� �ɸ��� �ð�

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
        Quaternion reverseRotation = Quaternion.Euler(targetRotation.eulerAngles + new Vector3(rotX, rotY, rotZ));
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

}
