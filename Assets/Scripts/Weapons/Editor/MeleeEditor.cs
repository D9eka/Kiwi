using UnityEditor;
using static Weapons.Weapon;

namespace Weapons.Editor
{
    [CustomEditor(typeof(Melee))]
    public class MeleeEditor : WeaponEditor
    {
        private SerializedProperty _attackDelayProperty;
        private SerializedProperty _modeProperty;

        private SerializedProperty _damageProperty;

        private SerializedProperty _minDamageProperty;
        private SerializedProperty _maxDamageProperty;

        private SerializedProperty _attackRangeProperty;
        private SerializedProperty _soundProperty;

        private void OnEnable()
        {
            _attackDelayProperty = serializedObject.FindProperty("_attackDelay");
            _modeProperty = serializedObject.FindProperty("_mode");

            _damageProperty = serializedObject.FindProperty("_damage");

            _minDamageProperty = serializedObject.FindProperty("_minDamage");
            _maxDamageProperty = serializedObject.FindProperty("_maxDamage");

            _attackRangeProperty = serializedObject.FindProperty("_attackRange");
            _soundProperty = serializedObject.FindProperty("_sound");
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(_attackDelayProperty);
            EditorGUILayout.PropertyField(_modeProperty);
            EditorGUILayout.PropertyField(_soundProperty);
            WeaponDamageType mode = (WeaponDamageType)_modeProperty.intValue;
            switch (mode)
            {
                case WeaponDamageType.Static:
                    EditorGUILayout.PropertyField(_damageProperty);
                    break;
                case WeaponDamageType.Random:
                    EditorGUILayout.PropertyField(_minDamageProperty);
                    EditorGUILayout.PropertyField(_maxDamageProperty);
                    break;
                default:
                    break;
            }

            EditorGUILayout.PropertyField(_attackRangeProperty);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
