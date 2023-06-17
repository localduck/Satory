using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Satory23
{
    public class GlobalDependenciesContainer : Dependency
    {
        private static GlobalDependenciesContainer instance;

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;

            DontDestroyOnLoad(gameObject);

            SceneManager.sceneLoaded += OnScenLoaded;
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnScenLoaded;
        }

        private void OnScenLoaded(Scene arg0, LoadSceneMode arg1)
        {
            RindAllObjectToBind();
        }
    }
}
