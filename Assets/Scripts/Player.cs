using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace OilSpillVR
{
    public class Player : NetworkBehaviour
    {
        public static Player localPlayer;
        [SyncVar] public string matchID;

        private NetworkMatchChecker _networkMatchChecker;

        // Start is called before the first frame update
        void Start()
        {
            if (isLocalPlayer)
                localPlayer = this;
            _networkMatchChecker = GetComponent<NetworkMatchChecker>();
        }

        public void HostGame()
        {
            string matchID = MatchMaking.GetRandomMatchID();
            CmdHostGame(matchID);
        }


        [Command]
        public void CmdHostGame(string _matchId)
        {
            matchID = _matchId;
            if (MatchMaking.instance.HostGame(_matchId, gameObject))
            {
                _networkMatchChecker.matchId = _matchId.ToGuid();
                TargetHostGame(true, _matchId);
                Debug.Log("Success");
            }

            else
            {
                TargetHostGame(false, _matchId);
                Debug.Log("error");
            }
        }

        [TargetRpc]
        void TargetHostGame(bool success, string matchId)
        {
            UILobby.instance.HostSuccess(success);
        }

        // Update is called once per frame
        void Update()
        {
        }
    }
}