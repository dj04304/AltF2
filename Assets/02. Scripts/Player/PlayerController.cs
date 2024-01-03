using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Move")]
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float runSpeed;
    [SerializeField]
    private float rotateSpeed;
    private Vector2 _moveInput;

    [Header("Jump")]
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private LayerMask GroundLayerMask;

    [Header("Attack")]
    [SerializeField]
    private GameObject chickenPrefab;
    [SerializeField]
    private GameObject chickenSpaawnPos;
    [SerializeField]
    private float attackSpeed;


    private bool _isGrounded;
    private bool _isElevator;
    private float lastAttackTime = float.MaxValue;
    private Vector3 boxSize = new Vector3(0.6f, 0.1f, 0.6f);
    private const float GroundedOffset = -0.1f;
    private const float boxCastDistance = 0.15f;
    private bool _isAttack;
    private bool _isRun;

    private Rigidbody _rigidbody;
    private Animator _animator;
    private Transform _mainCameraTransform;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();

        _mainCameraTransform = Camera.main.transform;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        Move();
        Attack();
        CheckElevator();
    }

    private void Move()
    {
        Vector3 dir = GetMoveDir();
        Rotate(dir);
        var movementSpeed = _isRun ? runSpeed : moveSpeed;
        dir = dir * movementSpeed;
        dir.y = _rigidbody.velocity.y;
        if (IsGrounded()) dir.y = 0f;
        _rigidbody.velocity = dir;
    }

    private void Attack()
    {
        lastAttackTime += Time.deltaTime;
        if (_isAttack && (lastAttackTime >= (1 / attackSpeed)))
        {
            // ����
            _animator.SetTrigger("Attack");
            Rotate(GetCamDir());
            Instantiate(chickenPrefab, chickenSpaawnPos.transform.position, Quaternion.identity);

            lastAttackTime = 0f;
        }
    }

    private void Rotate(Vector3 dir)
    {
        if (dir == Vector3.zero) return;
        Quaternion rotation = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotateSpeed * Time.deltaTime);
    }

    private Vector3 GetMoveDir()
    {
        var fowardDir = _mainCameraTransform.forward;
        var rightDir = _mainCameraTransform.right;

        fowardDir.y = 0f;
        rightDir.y = 0f;

        fowardDir.Normalize();
        rightDir.Normalize();

        return fowardDir * _moveInput.y + rightDir * _moveInput.x;

    }

    private Vector3 GetCamDir()
    {
        var fowardDir = _mainCameraTransform.forward;
        fowardDir.y = 0f;
        fowardDir.Normalize();
        return fowardDir;
    }
    #region InputAction
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            _moveInput = context.ReadValue<Vector2>();
            _animator.SetBool("Move", true);
        }
        else if(context.phase == InputActionPhase.Canceled)
        {
            _moveInput = Vector2.zero;
            _animator.SetBool("Move", false);
        }
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        {
            if (IsGrounded())
            {
                _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
    }

    public void OnAttackInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        {
            _isAttack = true;
        }
        else if(context.phase == InputActionPhase.Canceled)
        {
            _isAttack = false;
        }
    }

    public void OnRunInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        {
            _isRun = true;
        }
        if(context.phase == InputActionPhase.Canceled)
        {
            _isRun = false;
        }
    }
    #endregion

    private bool IsGrounded()
    {
        Vector3 boxPosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset,
            transform.position.z);
        _isGrounded = Physics.BoxCast(boxPosition, boxSize / 2, Vector3.down, Quaternion.identity, boxCastDistance, GroundLayerMask,
            QueryTriggerInteraction.Ignore);

        return _isGrounded;
    }

    private void CheckElevator()
    {
        RaycastHit hit;
        Vector3 boxPosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset ,
            transform.position.z);
        _isElevator = Physics.BoxCast(boxPosition, boxSize / 2, Vector3.down, out hit, Quaternion.identity, boxCastDistance, LayerMask.GetMask("Elevator"),
            QueryTriggerInteraction.Ignore);

        if (_isElevator)
        {
            transform.parent = hit.transform;
        }
        else
        {
            transform.parent = null;
        }
    }

    // test
    private void OnDrawGizmos()
    {
        Vector3 boxPosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset,
            transform.position.z);

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(boxPosition, Vector3.down * boxCastDistance);

        Gizmos.color = Color.red;

        Vector3 gizmoBoxSize = new Vector3(0.6f, 0.1f, 0.6f);
        Gizmos.DrawWireCube(boxPosition + Vector3.down * boxCastDistance, gizmoBoxSize);
    }

}
