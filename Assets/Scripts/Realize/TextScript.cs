using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    class TextScript : MonoBehaviour
    {
        Text textUI, powerupText;
        public int score = 0;
        ObjectCreator creator = new ObjectCreator();

        private void Start()
        {
            Text[] texts = creator.CreateTextScore().GetComponentsInChildren<Text>();
            if (texts[0].gameObject.CompareTag("PowerupText"))
            {
                textUI = texts[1];
                powerupText = texts[0];
            }
            else
            {
                textUI = texts[0];
                powerupText = texts[1];
            }
        }
        private void Update()
        {
            if (CheckForLose())
                textUI.text = "YOU LOSE!!!";
            else
            {
                if (!CheckForWin())
                    textUI.text = "Current Score: " + score.ToString();
                else textUI.text = "YOU WIN!!!";
            }
        }

        public IEnumerator ShowPowerupText(string typeOfPowerup)
        {
            powerupText.text = "You got " + typeOfPowerup;
            yield return new WaitForSeconds(3f);
            powerupText.text = "";
        }

        public void AddScore(int addedScore)
        {
            score += addedScore;
        }

        bool CheckForWin()
        {
            return GameObject.FindGameObjectWithTag("Enemy") == null;
        }
        bool CheckForLose()
        {
            return GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Run>().isPlayerDead;
        }
    }
}