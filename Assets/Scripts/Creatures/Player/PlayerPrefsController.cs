using Components.Health;
using Components.Oxygen;
using Sections;
using System;
using UnityEngine;

namespace Creatures.Player
{
    public static class PlayerPrefsController
    {
        public const string TUTORIAL_COMPLETE = "TutorialComplete";

        #region Int
        public static int GetInt(string key, int defaultValue = 0)
        {
            if (PlayerPrefs.HasKey(key))
            {
                return PlayerPrefs.GetInt(key);
            }
            return defaultValue;
        }

        public static void SetInt(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
        }
        #endregion

        #region Bool
        public static bool GetBool(string key, bool defaultValue = false)
        {
            if (PlayerPrefs.HasKey(key))
            {
                return PlayerPrefs.GetInt(key) == 1;
            }
            return defaultValue;
        }

        public static void SetBool(string key, bool state)
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

        #region Vector2
        public static Vector2 GetVector2(string key)
        {
            string xKey = key + "X";
            float x = PlayerPrefs.HasKey(xKey) ? GetFloat(xKey) : 0f;

            string yKey = key + "Y";
            float y = PlayerPrefs.HasKey(yKey) ? GetFloat(yKey) : 0f;

            return new Vector2(x, y);
        }

        public static void SetVector2(string key, Vector2 value)
        {
            SetFloat(key + "X", value.x);
            SetFloat(key + "Y", value.y);
        }
        #endregion

        #region StringArray
        public static string[] GetStringArray(string key)
        {
            if (PlayerPrefs.HasKey(key))
            {
                int length = GetInt($"{key}Length");
                string[] array = new string[length];
                for (int i = 0; i < length; i++)
                {
                    array[i] = GetString($"{key}{i}");
                }
                return array;
            }
            return Array.Empty<string>();
        }
        public static void SetStringArray(string key, string[] value)
        {
            SetInt($"{key}Length", value.Length);
            for (int i = 0; i < value.Length; i++)
            {
                SetString($"{key}{i}", value[i]);
            }
        }
        #endregion

        public static bool IsCompleteTutorial()
        {
            return PlayerPrefs.HasKey(TUTORIAL_COMPLETE);
        }

        public static void DeleteKey(string key)
        {
            PlayerPrefs.DeleteKey(key);
        }

        public static void CleanRunInfo()
        {
            SectionManagerData.Clear();
            WeaponControllerData.Clear();
            ChipManagerData.Clear();
            MyGameManager.Clear();
            StatsModifier.Clear();

            PlayerPrefs.DeleteKey(HealthComponent.SAVE_KEY + "X");
            PlayerPrefs.DeleteKey(HealthComponent.SAVE_KEY + "Y");

            PlayerPrefs.DeleteKey(OxygenComponent.SAVE_KEY + "X");
            PlayerPrefs.DeleteKey(OxygenComponent.SAVE_KEY + "Y");
        }

        public static void CleanPlayerInfo()
        {
            float soundVolume = GetFloat(SoundManager.SOUND_VOLUME_KEY, 1f);
            float musicVolume = GetFloat(SoundManager.MUSIC_VOLUME_KEY, 1f);

            PlayerPrefs.DeleteAll();

            SetFloat(SoundManager.SOUND_VOLUME_KEY, soundVolume);
            SetFloat(SoundManager.MUSIC_VOLUME_KEY, musicVolume);
        }
    }
}