using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet;
using FishNet.Object;
using FishNet.Object.Prediction;

public class CarNetworking : NetworkBehaviour
{
    public struct MoveData
    {
        public bool LeftTouch;
        public bool RightTouch;

        public MoveData(bool leftTouch, bool rightTouch)
        {
            LeftTouch = leftTouch;
            RightTouch = rightTouch;
        }
    }
    public struct ReconcileData
    {
        public Vector3 Position;
        public Vector3 Velocity;
        public Quaternion Rotation;
        public float AngularVelocity;
        public float GravityScale;
        public ReconcileData(Vector3 position, Vector3 velocity, Quaternion rotation, float angularVelocity, float gravityScale)
        {
            Position = position;
            Velocity = velocity;
            Rotation = rotation;
            AngularVelocity = angularVelocity;
            GravityScale = gravityScale;
        }
    }

    private CarMovement carMovement;
    private CarInputHandler carInputHandler;

    private void Awake()
    {
        InstanceFinder.TimeManager.OnTick += TimeManager_OnTick;
        InstanceFinder.TimeManager.OnPostTick += TimeManager_OnPostTick;
        carMovement = GetComponent<CarMovement>();
        carInputHandler = GetComponent<CarInputHandler>();
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        //carMovement.enabled = (base.IsServer || base.IsOwner);
    }

    private void OnDestroy()
    {
        if (InstanceFinder.TimeManager != null)
        {
            InstanceFinder.TimeManager.OnTick -= TimeManager_OnTick;
            InstanceFinder.TimeManager.OnPostTick -= TimeManager_OnPostTick;
        }
    }

    private void TimeManager_OnTick()
    {
        if (base.IsOwner)
        {
            Reconciliation(default, false);
            CheckInput(out MoveData md);
            Move(md, false);
        }
        if (base.IsServer)
        {
            Move(default, true);
        }
    }

    private void TimeManager_OnPostTick()
    {
        if (base.IsServer)
        {
            ReconcileData rd = carMovement.GetReconcileData();
            Reconciliation(rd, true);
        }
    }

    private void CheckInput(out MoveData md)
    {
        md = default;

        if (!carInputHandler.LeftTouch && !carInputHandler.RightTouch)
        {
            return;
        }

        md = new MoveData()
        {
            LeftTouch = carInputHandler.LeftTouch,
            RightTouch = carInputHandler.RightTouch,
        };
    }

    [Replicate]
    private void Move(MoveData md, bool asServer, bool replaying = false)
    {
        carMovement.UpdateMovement(md);
    }

    [Reconcile]
    private void Reconciliation(ReconcileData rd, bool asServer)
    {
        carMovement.ReconcileData(rd);
    }




}
