using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace DataService
{
    public static class JsonDataService
    {
        private const string PLAYER_FILE = "/player-data.json";
        private const string WEAPON_CONTROLLER_FILE = "/weapon-controller-data.json";
        private const string SECTION_MANAGER_FILE = "/section-manager-data.json";
        /*
        public static void Save(PlayerController playerController)
        {
            Save(PLAYER_FILE, playerController);
        }
        */

        public static void Save(string[] data)
        {
            Save(WEAPON_CONTROLLER_FILE, data);
        }
        /*
        public static void Save(SectionManagerData data)
        {
            Save(SECTION_MANAGER_FILE, data);
        }
        */
        public static void Save<T>(string relativePath, T data)
        {
            string path = Application.persistentDataPath + relativePath;
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            using FileStream stream = File.Create(path);
            stream.Close();
            File.WriteAllText(path, JsonConvert.SerializeObject(data));
        }
        /*
        public static bool TryLoad(SectionManager sender, out SectionManagerData data)
        {
            return TryLoad(SECTION_MANAGER_FILE, out data);
        }
        */
        public static bool TryLoad(WeaponController sender, out string[] data)
        {
            return TryLoad(SECTION_MANAGER_FILE, out data);
        }

        public static bool TryLoad<T>(string relativePath, out T data)
        {
            string path = Application.persistentDataPath + relativePath;
            Debug.Log(File.Exists(path));
            if (!File.Exists(path))
            {
                data = default;
                return false;
            }
            data = JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
            return true;
        }

        public static void DeleteAllData(bool includePlayerData = false)
        {
            List<string> files = new() { WEAPON_CONTROLLER_FILE, SECTION_MANAGER_FILE };
            if (includePlayerData)
                files.Add(PLAYER_FILE);

            foreach (string file in files)
                if (File.Exists(Application.persistentDataPath + file))
                    File.Delete(Application.persistentDataPath + file);
        }
    }
}
