using UnityEngine;

namespace Game.UI
{
    public sealed class RandomizedTile : MonoBehaviour
    {
        [SerializeField] private GameObject[] _bottomDecors;
        [SerializeField] private GameObject[] _topDecors;

        private static readonly float[] _decorRotations = {
            0f, 90f, 180f, 270f
        };
        
        private void Start()
        {
            Randomize();
        }

        public void Randomize()
        {
            foreach (GameObject decor in _bottomDecors) 
                decor.SetActive(Random.value < 0.5f);

            var decorIndex = Random.Range(0, _topDecors.Length);
            for (var i = 0; i < _topDecors.Length; i++)
            {
                var decor = _topDecors[i];
                if (i == decorIndex)
                {
                    decor.SetActive(true);
                    var rotation = decor.transform.rotation.eulerAngles;
                    rotation.y = _decorRotations[Random.Range(0, _decorRotations.Length)];
                    decor.transform.rotation = Quaternion.Euler(rotation);
                }
                else
                {
                    decor.SetActive(false);
                }
            }
        }
    }
}