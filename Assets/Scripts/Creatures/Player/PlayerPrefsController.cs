using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;

namespace Creatures.Player
{
    public static class PlayerPrefsController
    {
        #region Player
        private const string LOCATION = "PlayerLocation";
        private const string POSITION_X = "PlayerPosX";
        private const string POSITION_Y = "PlayerPosY";
        private const string SCALE = "PlayerScale";

        private const string HEALTH = "PlayerHealth";
        private const string MAX_HEALTH = "PlayerMaxHealth";

        private const string PROGRESS = "PlayerProgress";

        private const string FIRST_START = "PlayerFirstStart";
        private static Vector2 firstStartPos = new Vector2(0, 0);

        private const string STORAGE_KEY = "PlayerData";

        public static PlayerData GetPlayerData()
        {
            if (!PlayerPrefs.HasKey(FIRST_START))
                return new PlayerData(GetLocation(), GetPosition());
            else
                return new PlayerData(GetLocation(), GetPosition(), GetScale(),
                                      GetHealth(), GetMaxHealth(),
                                      GetFirstStartState());
        }

        public static bool HaveData()
        {
            return PlayerPrefs.HasKey(FIRST_START);
        }

        public static void SavePlayerData()
        {
            if (PlayerController.Instance == null)
                return;

            PlayerData data = PlayerController.Instance.SaveData();
            SaveData(data);
        }

        public static void SaveData(PlayerData data)
        {
            SetPosition(data.Position.Value);
            SetScale(data.Scale);

            SetHealth(data.Mana);
            SetMaxHealth(data.MaxMana);

            SetFirstStartState(data.FirstStart);
        }

        #endregion

        #region Audio
        private const string MUSIC_VOLUME = "MusicVolume";


        public static float GetMusicVolume()
        {
            return GetFloat(MUSIC_VOLUME, 1f);
        }

        public static void SetMusicVolume(float volume)
        {
            SetFloat(MUSIC_VOLUME, volume);
        }
        #endregion

        #region Position
        public static Vector2 GetPosition()
        {
            return new Vector2(PlayerPrefs.GetFloat(POSITION_X, firstStartPos.x), PlayerPrefs.GetFloat(POSITION_Y, firstStartPos.y));
        }

        public static void SetPosition(Vector2 position)
        {
            PlayerPrefs.SetFloat(POSITION_X, position.x);
            PlayerPrefs.SetFloat(POSITION_Y, position.y);
        }
        #endregion

        #region Scale
        public static float GetScale()
        {
            return GetFloat(SCALE, 1f);
        }

        public static void SetScale(float scale)
        {
            SetFloat(SCALE, scale);
        }
        #endregion

        #region Location
        public static string GetLocation()
        {
            return GetString(LOCATION);
        }
        public static void SetPlayerLocation(string location)
        {
            SetString(LOCATION, location);
        }
        #endregion

        #region Health
        public static float GetHealth()
        {
            return GetFloat(HEALTH, 100f);
        }

        public static void SetHealth(float health)
        {
            SetFloat(HEALTH, health);
        }
        #endregion

        #region MaxHealth
        public static float GetMaxHealth()
        {
            return GetFloat(MAX_HEALTH, 100f);
        }

        public static void SetMaxHealth(float health)
        {
            SetFloat(MAX_HEALTH, health);
        }
        #endregion

        #region Progress
        public static float GetProgress()
        {
            return GetFloat(PROGRESS);
        }

        public static void SetProgress(float progress)
        {
            SetFloat(PROGRESS, progress);
        }
        #endregion

        #region FirstStart
        public static bool GetFirstStartState()
        {
            return GetBool(FIRST_START, true);
        }

        public static void SetFirstStartState(bool state)
        {
            SetBool(FIRST_START, state);
        }
        #endregion

        #region Bool
        private static bool GetBool(string key, bool defaultValue = false)
        {
            if (PlayerPrefs.HasKey(key))
            {
                return PlayerPrefs.GetInt(key) == 1;
            }
            return defaultValue;
        }

        private static void SetBool(string key, bool state) 
        {
            PlayerPrefs.SetInt(key, state ? 1 : 0);
        }
        #endregion

        #region Float
        public static float GetFloat(string key, float defaultValue = 0f)
        {
            return PlayerPrefs.HasKey(key) ? PlayerPrefs.GetFloat(key) : defaultValue;
        }

        public static void SetFloat(string key, float value)
        {
            PlayerPrefs.SetFloat(key, value);
        }
        #endregion

        #region String
        public static string GetString(string key)
        {
            return PlayerPrefs.HasKey(key) ? PlayerPrefs.GetString(key) : "";
        }
        public static void SetString(string key, string value)
        {
            PlayerPrefs.SetString(key, value);
        }
        #endregion

        public static void CleanPlayerInfo()
        {
            PlayerPrefs.DeleteKey(POSITION_X);
            PlayerPrefs.DeleteKey(POSITION_Y);
            PlayerPrefs.DeleteKey(SCALE);
            PlayerPrefs.DeleteKey(LOCATION);

            PlayerPrefs.DeleteKey(HEALTH);
            PlayerPrefs.DeleteKey(MAX_HEALTH);

            PlayerPrefs.DeleteKey(FIRST_START);
        }
    }
}