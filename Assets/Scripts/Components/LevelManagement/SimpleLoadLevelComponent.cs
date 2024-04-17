using Components.UI;
using Creatures.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Components.LevelManagement
{
    public class SimpleLoadLevelComponent : MonoBehaviour
    {
        [SerializeField] private string _sceneName;
        [SerializeField] private Vector2 _position;
        [SerializeField] private bool _invertScale;

        public void Load()
        {
            PlayerPrefsController.SetString(PlayerData.PLAYER_SECTION_KEY, _sceneName);
            PlayerPrefsController.SetVector2(PlayerData.PLAYER_POSITION_KEY, _position);
            PlayerPrefsController.SetFloat(PlayerData.PLAYER_SCALE_KEY, _invertScale ? -1 : 1);

            SceneManager.LoadScene(_sceneName);
        }
    }
}