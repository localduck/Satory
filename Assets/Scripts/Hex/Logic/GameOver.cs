using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Satory23
{
    public class GameOver : MonoBehaviour
    {
        [SerializeField] private Text m_TextStatus;
        [SerializeField] private Button ResetButton;

        public void DefeatOrVictory(bool victory)
        {
            if (victory)
            {
                m_TextStatus.text = "WIN";
            }
            else
            {
                m_TextStatus.text = "LOSE";
            }
        }

        public void OnRestart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}