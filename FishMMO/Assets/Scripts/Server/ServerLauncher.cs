using UnityEngine;
using UnityEngine.SceneManagement;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Server
{
    public class ServerLauncher : MonoBehaviour
    {
        public Server Server { get; private set; }

        void Start()
        {
            string[] args = Environment.GetCommandLineArgs();
            switch (args[1].ToUpper())
            {
                case "LOGIN":
					Initialize("LoginServer");
					break;
                case "WORLD":
					Initialize("WorldServer");
					break;
                case "SCENE":
					Initialize("SceneServer");
					break;
                default:
					Debug.Log("[" + DateTime.UtcNow + "] ServerLauncher: Unknown server type. Available servers {Login, World, Scene}");
#if UNITY_EDITOR
					EditorApplication.ExitPlaymode();
#else
					Application.Quit();
#endif
					break;
            }
        }

        private void Initialize(string bootstrapSceneName)
        {
			SceneManager.LoadScene(bootstrapSceneName, LoadSceneMode.Single);
        }
    }
}