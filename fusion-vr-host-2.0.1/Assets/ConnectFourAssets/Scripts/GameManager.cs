using ExitGames.Client.Photon;
using Fusion;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ConnectFour
{
    public class GameManager : NetworkBehaviour
    {
        public const byte GameEndConnect4Code = 1;

        [SerializeField] private TMP_Text _winnerText;
        [SerializeField] private List<Coin> _coins;
        private CoinType[,] _coinMatrix = new CoinType[6,7];

        private static GameManager _instance;
        public static GameManager Instance { get => _instance; }

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
            }
        }

        //private void Start()
        //{            
        //    PhotonNetwork.NetworkingClient.EventReceived += OnEvent;
        //}

        public void AddCoin(CoinType coinType, int rowIndex)
        {
            for(int i = 0; i < 6; i++)
            {
                if(_coinMatrix[i, rowIndex] == CoinType.None)
                {
                    _coinMatrix[i, rowIndex] = coinType;
                    CheckForGameEnd();
                    return;
                }
            }
        }

        private void CheckForGameEnd()
        {
            for(int i = 0; i < 6; i++)
            {
                for(int j = 0; j < 7; j++)
                {
                    CoinType coinType = _coinMatrix[i, j];

                    int combo = 1;
                    for(int k = 1; i + k < 6; k++)
                    {
                        if (_coinMatrix[i + k, j] == coinType) combo++;
                        else break;

                        if (combo >= 4)
                        {
                            GameEnd(coinType);
                            return;
                        }
                    }

                    combo = 1;
                    for (int k = 1; i - k >= 0; k++)
                    {
                        if (_coinMatrix[i - k, j] == coinType) combo++;
                        else break;

                        if (combo >= 4)
                        {
                            GameEnd(coinType);
                            return;
                        }
                    }

                    combo = 1;
                    for (int k = 1; j + k < 7; k++)
                    {
                        if (_coinMatrix[i, j + k] == coinType) combo++;
                        else break;

                        if (combo >= 4)
                        {
                            GameEnd(coinType);
                            return;
                        }
                    }

                    combo = 1;
                    for (int k = 1; j - k >= 0; k++)
                    {
                        if (_coinMatrix[i, j - k] == coinType) combo++;
                        else break;

                        if (combo >= 4)
                        {
                            GameEnd(coinType);
                            return;
                        }
                    }

                    combo = 1;
                    for (int k = 1; i + k < 6 && j + k < 7; k++)
                    {
                        if (_coinMatrix[i + k, j + k] == coinType) combo++;
                        else break;

                        if (combo >= 4)
                        {
                            GameEnd(coinType);
                            return;
                        }
                    }

                    combo = 1;
                    for (int k = 1; i - k >= 0 && j + k < 7; k++)
                    {
                        if (_coinMatrix[i - k, j + k] == coinType) combo++;
                        else break;

                        if (combo >= 4)
                        {
                            GameEnd(coinType);
                            return;
                        }
                    }

                    combo = 1;
                    for (int k = 1; i + k < 6 && j - k >= 0; k++)
                    {
                        if (_coinMatrix[i + k, j - k] == coinType) combo++;
                        else break;

                        if (combo >= 4)
                        {
                            GameEnd(coinType);
                            return;
                        }
                    }

                    combo = 1;
                    for (int k = 1; i - k >= 0 && j - k >= 0; k++)
                    {
                        if (_coinMatrix[i - k, j - k] == coinType) combo++;
                        else break;

                        if (combo >= 4)
                        {
                            GameEnd(coinType);
                            return;
                        }
                    }
                }
            }
        }

        private void GameEnd(CoinType winnerName)
        {
            Debug.Log($"Connect 4 winner is {winnerName}");
            _winnerText.text = $"Connect 4 winner is {winnerName}";
            if (winnerName == CoinType.Red) _winnerText.color = Color.red;
            else if (winnerName == CoinType.Yellow) _winnerText.color = Color.yellow;
            _winnerText.gameObject.SetActive(true);
        }

        [Rpc(sources: RpcSources.All, targets: RpcTargets.All)]
        public void RPC_ResetGame()
        {
            foreach (Coin coin in _coins) coin.ResetPosition();

            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    _coinMatrix[i, j] = CoinType.None;
                }
            }

            _winnerText.gameObject.SetActive(false);
        }
    }
}
