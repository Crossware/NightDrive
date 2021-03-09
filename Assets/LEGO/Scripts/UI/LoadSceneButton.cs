using Unity.LEGO.Game;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Unity.LEGO.UI
{
    public class LoadSceneButton : MonoBehaviour
    {
        private string sceneName = "Night Drive";

        public void LoadScene()
        {
            SceneManager.LoadScene(sceneName);
        }

        public void LoadPreviousScene()
        {
            if (GameFlowManager.PreviousScene != null)
            {
                SceneManager.LoadScene(GameFlowManager.PreviousScene);
            }
            else
            {
                LoadScene();
            }
        }
    }
}