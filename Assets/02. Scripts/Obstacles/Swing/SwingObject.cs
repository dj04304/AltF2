using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SwingObject : MonoBehaviour
{
    [SerializeField] private float swingSpeed = 2.0f;  // ������ ��鸮�� �ӵ�
    [SerializeField] private float swingAngle = 30.0f; // ������ �ִ� ��鸮�� ����

    void Update()
    {
        // swing
        float angle =  Mathf.Sin(Time.time * swingSpeed) * swingAngle;

        //Debug.Log(angle);

        //float posAngle = _originalRotation + Mathf.Sin(Time.time * swingSpeed) * swingPosAngle;

        //float posZ = posAngle * _amplitude;

        // ���� rotation�� ��ȭ
        transform.rotation = Quaternion.Euler(angle, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        // ���� postion�� ��ȭ
        //transform.position = new Vector3(transform.position.x, transform.position.y, posZ);

    }
}
