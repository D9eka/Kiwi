using Creatures.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Extensions
{
    public static class SceneManagerExtensions
    {
        public static void LoadScene(this SceneManager sceneManager, string sceneName, Vector2 position, bool invertScale = false)
        {
            PlayerPrefsController.SetString(PlayerData.PLAYER_SECTION_KEY, sceneName);
            PlayerPrefsController.SetVector2(PlayerData.PLAYER_POSITION_KEY, position);
            PlayerPrefsController.SetFloat(PlayerData.PLAYER_SCALE_KEY, invertScale ? -1 : 1);

            SceneManager.LoadScene(sceneName);
        }
    }
}
