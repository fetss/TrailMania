using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;
using FishNet.Object.Synchronizing;

public class Player : NetworkBehaviour
{
    [SerializeField]
    private Car carPrefab;

    [SyncVar]
    public string username;

    [SyncVar]
    public bool isReady;

    [SyncVar]
    public Car controlledCar;

    [SyncVar]
    public bool isRacing;

    [SyncVar]
    public float raceTime;

    public override void OnStartServer()
    {
        base.OnStartServer();

        LevelManager.Instance.players.Add(this);
    }

    public override void OnStopServer()
    {
        base.OnStopServer();

        LevelManager.Instance.players.Remove(this);
    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        if (!IsOwner)
        {
            return;
        }

        LevelManager.Instance.onwerPlayer = this;
        ViewManager._instance.Initialize();
    }

    private void Update()
    {
        if (!IsOwner)
        {
            return;
        }
    }

    [Server]
    public void ServerStartGame()
    {
        //Debug.Log(controlledCar);
        if (controlledCar == null)
        {
            GameObject car = Instantiate(carPrefab.gameObject);
            controlledCar = car.GetComponent<Car>();
            Spawn(car, Owner);
            controlledCar.gameObject.SetActive(true);
            controlledCar.ownerPlayer = this;
            controlledCar.StartCamera();
        }

        ClientStartGame();
    }

    [ObserversRpc]
    public void ClientStartGame()
    {
        if (controlledCar && !IsOwner)
        {
            controlledCar.SetGhost();
        }
        ViewManager.ChangeView<RaceTimerView>();
    }

    [Server]
    public void ServerStartRace()
    {
        isRacing = true;

        ClientStartRace();
    }

    [ObserversRpc]
    public void ClientStartRace()
    {

    }

    [Server]
    public void ServerStopGame()
    {
        if (controlledCar != null)
        {
            controlledCar.EndCamera();
            controlledCar.gameObject.SetActive(false);
            Despawn(controlledCar.gameObject);

            isRacing = false;
        }

        ClientStopGame();
    }

    [ObserversRpc]
    public void ClientStopGame()
    {

    }

    [ServerRpc]
    public void ServerSetIsReady(bool value)
    {
        isReady = value;
    }

    [ServerRpc]
    public void ServerSetUsername(string name)
    {
        username = name;
    }
}
