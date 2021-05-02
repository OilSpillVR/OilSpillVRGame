using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace OilSpillVR
{
    public class UIPlayer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI playerNameTxt;
        public static UIPlayer uiPlayer;

        private Player _player;

        // Start is called before the first frame update
        void Start()
        {
            uiPlayer = this;
        }
        
        public void SetPlayer(Player player)
        {
            this._player = player;
            playerNameTxt.text = "Player " + player.playerIndex;
        }
    }
}