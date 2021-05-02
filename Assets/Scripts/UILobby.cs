using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace OilSpillVR
{
    public class UILobby : MonoBehaviour
    {
        public static UILobby instance;

        [Header("Host & Join")] [SerializeField]
        private InputField joinMatchInput;

        [SerializeField] private Button hostButton;
        [SerializeField] private Button joinButton;
        [SerializeField] private Canvas lobbyCanvas;


        [Header("Lobby")] [SerializeField] private Transform uiPlayerParent;
        [SerializeField] private GameObject uiPlayerPrefab;
        [SerializeField] private TextMeshProUGUI matchIDTxT;
        [SerializeField] private GameObject btnBeginGame;

        private void Start()
        {
            instance = this;
        }

        public void Host()
        {
            joinMatchInput.interactable = false;
            hostButton.interactable = false;
            joinButton.interactable = false;
            lobbyCanvas.enabled = true;
            Player.localPlayer.HostGame();
        }

        public void Join()
        {
            joinMatchInput.interactable = false;
            hostButton.interactable = false;
            joinButton.interactable = false;
            lobbyCanvas.enabled = true;
            Player.localPlayer.JoinGame(joinMatchInput.text.ToUpper());
        }

        public void HostSuccess(bool success, string matchID)
        {
            if (success)
            {
                lobbyCanvas.enabled = true;
                SpawnPlayerPrefab(Player.localPlayer);
                matchIDTxT.text = matchID;
                btnBeginGame.SetActive(true);
            }
            else
            {
                joinMatchInput.interactable = true;
                hostButton.interactable = true;
                joinButton.interactable = true;
            }
        }

        public void JoinSuccess(bool success, string matchID)
        {
            if (success)
            {
                lobbyCanvas.enabled = true;
                SpawnPlayerPrefab(Player.localPlayer);
                matchIDTxT.text = matchID;
            }
            else
            {
                joinMatchInput.interactable = true;
                hostButton.interactable = true;
                joinButton.interactable = true;
            }
        }

        public void SpawnPlayerPrefab(Player player)
        {
            GameObject newUIPlayer = Instantiate(uiPlayerPrefab, uiPlayerParent);
            newUIPlayer.GetComponent<UIPlayer>().SetPlayer(player);
        }


        public void BeginGame()
        {
            Player.localPlayer.BeginGame();
        }
    }
}