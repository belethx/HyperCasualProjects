using TMPro;
using UnityEngine;

namespace UI
{
    public class Score : MonoBehaviour
    {
        public int score;
        [SerializeField] private TextMeshProUGUI shotScoreText;

        void Update()
        {
            shotScoreText.text = score.ToString();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Constants.tenpinTag))
            {
                score++;
                other.gameObject.GetComponent<BoxCollider>().enabled = false;
            }
        }
    }
}