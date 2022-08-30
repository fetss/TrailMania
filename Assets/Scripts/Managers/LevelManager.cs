using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using System.Linq;

public sealed class LevelManager : NetworkBehaviour
{
    public static LevelManager Instance { get; private set; }

    public Player onwerPlayer;

    [SyncObject]
    public readonly SyncList<Player> players = new SyncList<Player>();

    [SyncVar]
    public bool canStart;

    [SyncVar]
    public float raceTimer;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (!IsServer)
        {
            return;
        }

        players.RemoveAll(player => player == null);

        if (players.Count > 0)
        {
            canStart = players.All(player => player.isReady);
        }
    }

    [Server]
    public void ServerStartGame()
    {
        if (!canStart)
        {
            return;
        }

        Debug.Log("StartGame");

        for (int i = 0; i < players.Count; i++)
        {
            players[i].ServerStartGame();
        }

        StartCoroutine(StartTimer());
    }

    IEnumerator StartTimer()
    {
        raceTimer = -3;
        while (raceTimer < 0)
        {
            raceTimer += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        ServerStartRace();
    }

    [Server]
    public void ServerStartRace()
    {
        Debug.Log("StartRace");

        for (int i = 0; i < players.Count; i++)
        {
            players[i].ServerStartRace();
        }
    }

    [Server]
    public void ServerStopGame()
    {
        for (int i = 0; i < players.Count; i++)
        {
            players[i].ServerStopGame();
        }
    }
}
