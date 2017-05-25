using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    class TextScript : MonoBehaviour
    {
        Text textUI;
        public int score = 0;
        ObjectCreator creator = new ObjectCreator();

        private void Start()
        {
            textUI = creator.CreateTextScore().GetComponentInChildren<Text>();
        }
        private void Update()
        {
            if (!CheckForWin())
                textUI.text = "Current Score: " + score.ToString();
            else textUI.text = "YOU WIN!!!";
        }

        public void AddScore(int addedScore)
        {
            score += addedScore;
        }

        bool CheckForWin()
        {
            int winScore = 0;
            Run run = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Run>();
            winScore = run.enemiesCount * 15;
            return (score == winScore);
        }
    }
}