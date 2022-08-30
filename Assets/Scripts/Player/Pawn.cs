using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;
using FishNet.Object.Synchronizing;

public class Pawn : NetworkBehaviour
{
    [SyncVar]
    public float health;

    [SyncVar]
    public Player controllinfPlayer;

    void Start()
    {

    }

    void Update()
    {

    }
}
