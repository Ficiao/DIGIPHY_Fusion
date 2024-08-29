using Digiphy;
using Fusion;
using UnityEngine;

namespace ConnectFour
{
    public class GameResetter : MonoBehaviour, XrSelectable
    {
        public void Selected()
        {
            GameManager.Instance.RPC_ResetGame();
        }
    }
}