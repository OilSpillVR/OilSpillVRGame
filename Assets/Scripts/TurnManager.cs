using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

namespace OilSpillVR
{
    public class TurnManager : NetworkBehaviour
    {
        private List<Player> players = new List<Player>();

        void Awake()
        {
        }

        public void AddPlayer(Player player)
        {
            players.Add(player);
        }
    }
}