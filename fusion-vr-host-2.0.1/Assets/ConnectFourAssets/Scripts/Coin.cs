using Fusion;
using Fusion.XR.Host.Grabbing;
using UnityEngine;

namespace ConnectFour
{
    public class Coin : NetworkBehaviour
    {
        [SerializeField] private CoinType _coinType;
        [SerializeField] private Rigidbody _rigidBody;
        [SerializeField] private Grabbable _grabbable;
        private Vector3 _startPosition;
        private Quaternion _startRotation;

        public CoinType CoinType => _coinType;
        public Grabbable Grabbable => _grabbable;
        public Rigidbody Rigidbody => _rigidBody;

        private void Awake()
        {
            _startPosition = transform.localPosition;
            _startRotation = transform.localRotation;
        }

        public override void Spawned()
        {
            transform.localPosition = _startPosition;
            transform.localRotation = _startRotation;
            _rigidBody.useGravity = true;
        }

        public void ResetPosition()
        {
            transform.localPosition = _startPosition;
            transform.localRotation = _startRotation;
        }
    }
}