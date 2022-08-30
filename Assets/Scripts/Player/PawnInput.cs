using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;


public class PawnInput : NetworkBehaviour
{
    private Pawn _pawn;

    public float horizontal;
    public float vertical;

    public float mouseX;
    public float mouseY;

    public float sensitivity;

    public bool jump;

    void Start()
    {

    }

    void Update()
    {
        if (!IsOwner)
        {
            return;
        }

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        mouseX = Input.GetAxis("Mouse X") * sensitivity;
        mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        jump = Input.GetButton("Jump");
    }

    public override void OnStartNetwork()
    {
        base.OnStartNetwork();
        _pawn = GetComponent<Pawn>();
    }

}
