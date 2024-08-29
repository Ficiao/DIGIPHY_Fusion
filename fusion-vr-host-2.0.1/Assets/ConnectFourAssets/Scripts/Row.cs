using System.Collections;
using UnityEngine;

namespace ConnectFour
{
    public class Row : MonoBehaviour
    {
        [SerializeField] private int _rowIndex;


        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.parent != null && other.transform.parent.TryGetComponent<Coin>(out Coin coin))
            {
                coin.Grabbable.ShouldUngrab = true;
                coin.transform.parent = transform;
                StartCoroutine(CoinMover(coin.transform.localPosition, coin.transform.localRotation.eulerAngles, coin));
                GameManager.Instance.AddCoin(coin.CoinType, _rowIndex);
            }
        }

        private IEnumerator CoinMover(Vector3 startPosition, Vector3 startRotation, Coin coin)
        {
            coin.Rigidbody.isKinematic = true;

            Vector3 endRotation = new Vector3(0, 90, 0);
            for (float i = 0; i <= 1; i += 0.1f)
            {
                yield return new WaitForSeconds(.1f);
                coin.transform.localRotation = Quaternion.Euler(Vector3.Lerp(startRotation, endRotation, i));
                coin.transform.localPosition = Vector3.Lerp(startPosition, Vector3.zero, i);
            }
            //coin.Rigidbody.isKinematic = false;
        }
    }
}
