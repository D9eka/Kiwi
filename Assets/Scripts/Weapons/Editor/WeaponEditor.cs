using UnityEditor;
using static Weapons.Weapon;

namespace Weapons.Editor
{
    [CustomEditor(typeof(Weapon))]
    public class WeaponEditor : UnityEditor.Editor
    {
        private SerializedProperty _attackDelayProperty;
        private SerializedProperty _modeProperty;

        private SerializedProperty _damageProperty;

        private SerializedProperty _minDamageProperty;
        private SerializedProperty _maxDamageProperty;

        private void OnEnable()
        {
            _attackDelayProperty = serializedObject.FindProperty("_attackDelay");
            _modeProperty = serializedObject.FindProperty("_mode");

            _damageProperty = serializedObject.FindProperty("_damage");
            
            _minDamageProperty = serializedObject.FindProperty("_minDamage");
            _maxDamageProperty = serializedObject.FindProperty("_maxDamage");
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(_attackDelayProperty);
            EditorGUILayout.PropertyField(_modeProperty);

            DamageType mode = (DamageType)_modeProperty.intValue;
            switch (mode)
            {
                case DamageType.Static:
                    EditorGUILayout.PropertyField(_damageProperty);
                    break;
                case DamageType.Random:
                    EditorGUILayout.PropertyField(_minDamageProperty);
                    EditorGUILayout.PropertyField(_maxDamageProperty);
                    break;
                default:
                    break;
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}
