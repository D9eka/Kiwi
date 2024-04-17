using Creatures.Player;
using System;
using TMPro;
using UnityEngine;

namespace PermanentBuffs
{
    public abstract class PermanentBuff : MonoBehaviour
    {
        [SerializeField] private PermanentBuffSO _data;

        public PermanentBuffSO Data => _data;
        public int SpentEssence
        {
            get
            {
                return PlayerPrefsController.GetInt(_data.Name);
            }

            private set
            {
                PlayerPrefsController.SetInt(_data.Name, value);
            }
        }
        public EventHandler OnChangeSpentEssence;

        public abstract void Activate();

        public abstract void Deactivate();

        public void SpendEssence(int spentEssence)
        {
            if (GameManager.Instance.TrySpendEssence(spentEssence))
            {
                SpentEssence += spentEssence;
                OnChangeSpentEssence?.Invoke(this, EventArgs.Empty);
            }
        }

        public void Save() => PlayerPrefs.SetInt(_data.Name, SpentEssence);
    }
}
