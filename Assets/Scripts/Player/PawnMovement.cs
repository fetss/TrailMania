using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;


public class PawnMovement : NetworkBehaviour
{
    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private float jumpSpeed;

    [SerializeField]
    private float gravityScale;

    private CharacterController _characterController;

    private Vector3 _velocity;

    private PawnInput _pawnInput;

    public override void OnStartNetwork()
    {
        base.OnStartNetwork();

        _characterController = GetComponent<CharacterController>();
        _pawnInput = GetComponent<PawnInput>();
    }

    private void FixedUpdate()
    {
        if (!IsOwner)
        {
            return;
        }

        Vector3 xzVelocity = Vector3.ClampMagnitude(new Vector3(_pawnInput.horizontal, 0, _pawnInput.vertical) * moveSpeed, moveSpeed);

        _velocity.x = Mathf.Cos(transform.eulerAngles.y * Mathf.Deg2Rad) * xzVelocity.x + Mathf.Sin(transform.eulerAngles.y * Mathf.Deg2Rad) * xzVelocity.z;
        _velocity.z = -Mathf.Sin(transform.eulerAngles.y * Mathf.Deg2Rad) * xzVelocity.x + Mathf.Cos(transform.eulerAngles.y * Mathf.Deg2Rad) * xzVelocity.z;

        if (_characterController.isGrounded)
        {
            _velocity.y = 0;
            if (_pawnInput.jump)
            {
                _velocity.y += jumpSpeed;
            }
        }
        else
        {
            _velocity.y += Physics.gravity.y * gravityScale * Time.fixedDeltaTime;
        }

        _characterController.Move(_velocity * Time.fixedDeltaTime);
    }
}
