using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;
public class PawnCameraLook : NetworkBehaviour
{
    private PawnInput _pawnInput;

    [SerializeField]
    private Transform myCamera;

    [SerializeField]
    private float xMax;
    [SerializeField]
    private float xMin;

    private Vector3 _eulerAngles;

    public override void OnStartNetwork()
    {
        base.OnStartNetwork();

        _pawnInput = GetComponent<PawnInput>();
    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        myCamera.GetComponent<Camera>().enabled = IsOwner;
    }

    void Update()
    {
        if (!IsOwner)
        {
            return;
        }

        _eulerAngles.x -= _pawnInput.mouseY;

        _eulerAngles.x = Mathf.Clamp(_eulerAngles.x, xMin, xMax);

        myCamera.localEulerAngles = _eulerAngles;

        transform.Rotate(0, _pawnInput.mouseX, 0, Space.World);
    }
}
