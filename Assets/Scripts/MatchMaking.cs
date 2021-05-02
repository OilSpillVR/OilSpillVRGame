using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using Mirror;
using Random = UnityEngine.Random;

namespace OilSpillVR
{
    [System.Serializable]
    public class Match
    {
        public string id;
        public SyncListGameObject players = new SyncListGameObject();


        public Match(string matchId, GameObject player)
        {
            id = matchId;
            players.Add(player);
        }

        public Match()
        {
        }
    }

    public class SyncListGameObject : SyncList<GameObject>
    {
    }

    public class SyncListMatch : SyncList<Match>
    {
    }

    public class MatchMaking : NetworkBehaviour
    {
        public static MatchMaking instance;
        public SyncList<Match> matches = new SyncList<Match>();
        public SyncList<string> matchIDs = new SyncList<string>();
        [SerializeField] private GameObject turnManagerPrefab;

        public void Start()
        {
            instance = this;
        }

        public bool HostGame(string matchId, GameObject player, out int playerIndex)
        {
            playerIndex = -1;
            if (!matchIDs.Contains(matchId))
            {
                matchIDs.Add(matchId);
                matches.Add(new Match(matchId, player));
                playerIndex = 1;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool JoinGame(string matchId, GameObject player, out int playerIndex)
        {
            playerIndex = -1;
            if (matchIDs.Contains(matchId))
            {
                for (int i = 0; i < matches.Count; i++)
                {
                    if (matches[i].id == matchId)
                    {
                        matches[i].players.Add(player);
                        playerIndex = matches[i].players.Count;
                        break;
                    }
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        public void BeginGame(string matchId)
        {
            GameObject newTurnManager = Instantiate(turnManagerPrefab);
            NetworkServer.Spawn(newTurnManager);
            newTurnManager.GetComponent<NetworkMatchChecker>().matchId = matchId.ToGuid();
            TurnManager turnManager = newTurnManager.GetComponent<TurnManager>();
            for (int i = 0; i < matches.Count; i++)
            {
                if (matches[i].id == matchId)
                {
                    foreach (var player in matches[i].players)
                    {
                        Player _player = player.GetComponent<Player>();
                        turnManager.AddPlayer(_player);
                        _player.StartGame();
                    }
                    break;
                }
            }
        }

        public static string GetRandomMatchID()
        {
            string id = string.Empty;
            for (int i = 0; i < 5; i++)
            {
                int random = UnityEngine.Random.Range(0, 36);
                if (random < 26)
                {
                    id += (char) (random + 65);
                }
                else
                {
                    id += (random - 26).ToString();
                }
            }

            Debug.Log($"Random Match ID: {id}");
            return id;
        }
    }

    public static class MatchExtensions
    {
        public static Guid ToGuid(this string id)
        {
            MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();

            byte[] inputBytes = Encoding.Default.GetBytes(id);
            byte[] hasBytes = provider.ComputeHash(inputBytes);

            return new Guid(hasBytes);
        }
    }
}