using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Satory23
{
    public class MenuActions : MonoBehaviour
    {
        public void StartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public void ApplySettings()
        {

        }

        public void ExiteApp()
        {
            Application.Quit();
        }
    }
}