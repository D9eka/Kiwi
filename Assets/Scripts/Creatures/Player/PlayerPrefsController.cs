using UnityEngine;

namespace Creatures.Player
{
    public static class PlayerPrefsController
    {
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
            float x = PlayerPrefs.HasKey(xKey) ? PlayerPrefs.GetFloat(xKey) : 0f;

            string yKey = key + "Y";
            float y = PlayerPrefs.HasKey(yKey) ? PlayerPrefs.GetFloat(yKey) : 0f;

            return new Vector2(x, y);
        }

        public static void SetVector2(string key, Vector2 value)
        {
            PlayerPrefs.SetFloat(key + "X", value.x);
            PlayerPrefs.SetFloat(key + "Y", value.y);
        }
        #endregion

        public static void CleanPlayerInfo()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}