using System.Collections;
using TMPro;
using UnityEngine;

namespace pathfinding.demo
{
    public class GameManager : MonoBehaviour
    {
        #region GameManager Instance
        public static GameManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }
        #endregion

        [SerializeField] private TMP_Text infoText;

        [SerializeField] private GameObject player;
        [SerializeField] private GameObject enemy;

        [SerializeField] private int playerMoveRange = 4;
        [SerializeField] private int playerAttackRange = 3;

        [SerializeField] private float coroutineTime = 3.0f;

        public GameObject Player { get { return player; } }
        public GameObject Enemy { get { return enemy; } }

        public int PlayerMoveRange { get { return playerMoveRange; } }
        public int PlayerAttackRange { get { return playerAttackRange; } }

        private IEnumerator PressButtonCo()
        {
            yield return new WaitForSeconds(coroutineTime);
            SetInfoText("Press \"Find Path\" button");
        }

        public void PressButton()
        {
            StartCoroutine("PressButtonCo");
        }

        public void SetInfoText(string info)
        {
            infoText.text = info;
        }
    }
}