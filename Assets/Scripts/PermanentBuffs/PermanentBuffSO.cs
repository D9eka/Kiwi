using UnityEngine;

namespace PermanentBuffs
{
    [CreateAssetMenu]
    public class PermanentBuffSO : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private Sprite _icon;
        [SerializeField] private string _description;
        [SerializeField] private int _price;

        public string Name => _name;
        public Sprite Icon => _icon;
        public string Description => _description;
        public int Price => _price;
    }
}