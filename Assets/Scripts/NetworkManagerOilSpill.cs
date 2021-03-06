using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class NetworkManagerOilSpill : NetworkManager
{
    [SerializeField] private int minPlayers = 3;
    [Scene] [SerializeField] private string menuScene = string.Empty;
    [Header("Room")] [SerializeField] private NetworkRoomPlayerOil roomPlayerPrefab = null;

    [Header("Game")] [SerializeField] private NetworkRoomPlayerOil gamePlayerPrefab = null;
    [SerializeField] private GameObject playerSpawnSystem = null;
    [SerializeField] private GameObject roundSystem = null;


    public static event Action OnClientConnected;
    public static event Action OnClientDisconnected;
    private static event Action<NetworkConnection> OnServerReadied;

    public List<NetworkRoomPlayerOil> RoomPlayers { get; } = new List<NetworkRoomPlayerOil>();
    public List<NetworkRoomPlayerOil> GamePlayers { get; } = new List<NetworkRoomPlayerOil>();


    public override void OnStopServer()
    {
        RoomPlayers.Clear();
        GamePlayers.Clear();
    }

    public void NotifyPLayersOfReadyState()
    {
        foreach (var player in RoomPlayers)
        {
            player.HandleReadyToStart(IsReadyToStart());
        }
    }

    private bool IsReadyToStart()
    {
        if (numPlayers < minPlayers) return false;
        foreach (var player in RoomPlayers)
        {
            if (!player.IsReady) return false;
        }

        return true;
    }

    public void StartGame()
    {
        
    }
}