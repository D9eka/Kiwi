using Components.Health;
using Components.UI;
using Creatures.Player;
using UnityEngine;

namespace Components.LevelManagement
{
    public class Checkpoint : MonoBehaviour
    {
        public void SavePosition()
        {
            Vector2 lastPlayerPosition = PlayerPrefsController.GetPosition();
            PlayerPrefsController.SavePlayerData();
        }
    }
}