using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class RotationJump : BaseObstacle
{
    [SerializeField]
    private float _jumpforce = 40f;
    private float _rotateSpeed = 0.5f;
    private Vector3 _stopRotate = new Vector3(70, 0, 0);
    private bool _checkRotate = true;
    private bool _collidertime = true;
    private bool _onPad = false;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && _checkRotate && _collidertime)
        {
            StartCoroutine(RotatePad(collision));
            _collidertime = false;
            _onPad = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        _onPad = false;
    }

    IEnumerator RotatePad(Collision collision)
    {

        yield return new WaitForSeconds(0.2f);
        while (_checkRotate)
        {
            _rotateSpeed += 0.01f;
            transform.Rotate(Vector2.right * _rotateSpeed);
            yield return null;
            if (_stopRotate.x - transform.eulerAngles.x <= 10f)
            {

                Vector3 forwardDirection = transform.up;
                
                //Rigidbody Addforce  (y�� ���� �޴µ� z ���� ������)
                Rigidbody otherRigidbody = collision.gameObject.GetComponent<Rigidbody>();
                if (otherRigidbody != null&& _onPad)
                {
                    Debug.Log("addforce ���� : " + forwardDirection * _jumpforce);
                    otherRigidbody.AddForce(forwardDirection * _jumpforce, ForceMode.Impulse);
                    base.OnCollisionEnter(collision);
                }

                
                _checkRotate = false;
            }
        }

        yield return new WaitForSeconds(2f);

        while (!_checkRotate)
        {
            transform.Rotate(Vector2.left);
            yield return null;

            if (transform.eulerAngles.x <= 1.1f)
            {
                _checkRotate = true;
            }
        }
        _collidertime = true;
        _rotateSpeed = 1f;
    }

}
