using Creatures.Player;
using System;
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
        public bool IsReceived => SpentEssence >= _data.Price;
        public EventHandler OnChangeSpentEssence;

        protected virtual void Start()
        {
            if (IsReceived)
            {
                Activate();
            }
        }

        public abstract void Activate();

        public abstract void Deactivate();

        public void SpendEssence()
        {
            SpendEssence(_data.Price - SpentEssence);
        }

        public void SpendEssence(int spentEssence)
        {
            if (MyGameManager.TrySpendEssence(spentEssence))
            {
                SpentEssence += spentEssence;
                OnChangeSpentEssence?.Invoke(this, EventArgs.Empty);
                if (IsReceived)
                {
                    Activate();
                }
            }
        }
    }
}
