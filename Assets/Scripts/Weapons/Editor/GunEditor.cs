using UnityEditor;
using static Weapons.Weapon;

namespace Weapons.Editor
{
    [CustomEditor(typeof(Gun))]
    public class GunEditor : UnityEditor.Editor
    {
        private SerializedProperty _attackDelayProperty;
        private SerializedProperty _modeProperty;

        private SerializedProperty _damageProperty;

        private SerializedProperty _minDamageProperty;
        private SerializedProperty _maxDamageProperty;

        private SerializedProperty _bulletProperty;
        private SerializedProperty _shotPointProperty;

        private SerializedProperty _speedProperty;
        private SerializedProperty _ttlProperty;
        private SerializedProperty _ammoCapacityProperty;

        private void OnEnable()
        {
            _attackDelayProperty = serializedObject.FindProperty("_attackDelay");
            _modeProperty = serializedObject.FindProperty("_mode");

            _damageProperty = serializedObject.FindProperty("_damage");

            _minDamageProperty = serializedObject.FindProperty("_minDamage");
            _maxDamageProperty = serializedObject.FindProperty("_maxDamage");

            _bulletProperty = serializedObject.FindProperty("_bullet");
            _shotPointProperty = serializedObject.FindProperty("_shotPoint");

            _speedProperty = serializedObject.FindProperty("_speed");
            _ttlProperty = serializedObject.FindProperty("_ttl");
            _ammoCapacityProperty = serializedObject.FindProperty("_ammoCapacity");
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(_attackDelayProperty);
            EditorGUILayout.PropertyField(_modeProperty);

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

            EditorGUILayout.PropertyField(_bulletProperty);
            EditorGUILayout.PropertyField(_shotPointProperty);

            EditorGUILayout.PropertyField(_speedProperty);
            EditorGUILayout.PropertyField(_ttlProperty);
            EditorGUILayout.PropertyField(_ammoCapacityProperty);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
