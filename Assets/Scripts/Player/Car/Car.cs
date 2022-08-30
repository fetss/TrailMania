using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;
using FishNet.Object.Synchronizing;

public class Car : NetworkBehaviour
{
    [SerializeField]
    public CarWheel wheelFront;

    [SerializeField]
    public CarWheel wheelBack;

    private Camera mainCamera;

    public bool IsGhost;

    [SyncVar]
    public Player ownerPlayer;

    public void SetGhost()
    {
        IsGhost = true;
        //wheelBack.GetComponent<Collider2D>().enabled = false;
        //wheelFront.GetComponent<Collider2D>().enabled = false;
    }

    [ObserversRpc]
    public void StartCamera()
    {
        if (base.IsOwner)
        {
            this.mainCamera = Camera.main;
            if (mainCamera)
            {
                mainCamera.GetComponent<CameraController>().Initialize(GetComponent<Rigidbody2D>(), -0.25f, 0.05f);
            }
        }
    }

    [ObserversRpc]
    public void EndCamera()
    {
        if (base.IsOwner)
        {
            if (mainCamera)
            {
                mainCamera.GetComponent<CameraController>().following = false;
            }
        }
    }

}
