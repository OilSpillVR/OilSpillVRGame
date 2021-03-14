using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace OilSpillVR
{
    public class UILobby : MonoBehaviour
    {
        public static UILobby instance;
        [SerializeField] private InputField joinMatchInput;
        [SerializeField] private Button hostButton;
        [SerializeField] private Button joinButton;
        [SerializeField] private Canvas lobbyCanvas;

        private void Start()
        {
            instance = this;
        }

        public void Host()
        {
            joinMatchInput.interactable = false;
            hostButton.interactable = false;
            joinButton.interactable = false;

            Player.localPlayer.HostGame();
        }

        public void Join()
        {
            joinMatchInput.interactable = false;
            hostButton.interactable = false;
            joinButton.interactable = false;
        }

        public void HostSuccess(bool success)
        {
            if (success)
            {
                lobbyCanvas.enabled = true;
            }
            else
            {
                joinMatchInput.interactable = true;
                hostButton.interactable = true;
                joinButton.interactable = true;
            }
        }

        public void JoinSuccess(bool success)
        {
            if (success)
            {
            }
            else
            {
                joinMatchInput.interactable = true;
                hostButton.interactable = true;
                joinButton.interactable = true;
            }
        }
    }
}