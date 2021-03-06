using System.Collections;
using System.Collections.Generic;
using Mirror;
using Mirror.Examples.MultipleMatch;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NetworkRoomPlayerOil : NetworkBehaviour
{
    [Header("UI")] [SerializeField] private GameObject lobbyUI = null;

    [SerializeField] private TMP_Text[] playerRolesTexts = new TMP_Text[3];

    [SerializeField] private TMP_Text[] playerRolesReadyTexts = new TMP_Text[3];

    [SerializeField] private Button startGameBtn;

    [SyncVar(hook = nameof(HandleDisplayNameChanged))]
    public string DisplayName = "Loading...";

    [SyncVar(hook = nameof(HandleReadyStatusChanged))]
    public bool IsReady = false;

    private bool isLeader;

    public bool IsLeader
    {
        set
        {
            IsLeader = value;
            startGameBtn.gameObject.SetActive(true);
        }
    }

    private NetworkManagerOilSpill room;

    private NetworkManagerOilSpill Room
    {
        get
        {
            if (room != null)
            {
                return room;
            }

            return room = NetworkManager.singleton as NetworkManagerOilSpill;
        }
    }

    public override void OnStartAuthority()
    {
      //  CmdSetDisplayName(PlayerNameInput.DisplayName);
    }

    public override void OnStartClient()
    {
        Room.RoomPlayers.Add(this);
        UpdateDisplay();
    }

   // public override void OnNetworkDestroy()
   // {
   //     Room.RoomPlayers.Add(this);
   //     UpdateDisplay();
   // }

    public void HandleReadyStatusChanged(string oldValue, string newValue) => UpdateDisplay();

    public void HandleDisplayNameChanged(string oldValue, string newValue) => UpdateDisplay();


    private void UpdateDisplay()
    {
        if (!isLocalPlayer)
        {
            foreach (var player in Room.RoomPlayers)
            {
                if (player.isLocalPlayer)
                {
                    player.UpdateDisplay();
                    break;
                }
            }

            return;
        }

        for (int i = 0; i < playerRolesTexts.Length; i++)
        {
            playerRolesTexts[i].text = Room.RoomPlayers[i].DisplayName;
            playerRolesReadyTexts[i].text = Room.RoomPlayers[i].IsReady
                ? "<color=green>Ready</color>"
                : "<color=red>Not Ready</color>";
        }
    }

    public void HandleReadyToStart(bool readyToStart)
    {
        if (!isLeader)
        {
            return;
        }

        startGameBtn.interactable = readyToStart;
    }


    [Command]
    private void CmdSetDisplayName(string displayName)
    {
        DisplayName = displayName;
    }

    [Command]
    private void CmdReadyUp()
    {
        IsReady = !IsReady;
        Room.NotifyPLayersOfReadyState();
    }

    [Command]
    public void CmdStartGame()
    {
        Room.StartGame();
    }
}