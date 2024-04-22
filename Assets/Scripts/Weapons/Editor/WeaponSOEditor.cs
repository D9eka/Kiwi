using System;
using UnityEditor;
using static Weapons.WeaponSO;

namespace Weapons.Editor
{
    [CustomEditor(typeof(WeaponSO))]
    public class WeaponSOEditor : UnityEditor.Editor
    {
        private SerializedProperty _nameProperty;
        private SerializedProperty _descriptionProperty;
        private SerializedProperty _iconProperty;
        private SerializedProperty _priceProperty;

        private SerializedProperty _attackDelayProperty;
        private SerializedProperty _damageTypeUIProperty;
        private SerializedProperty _damageTypeProperty;
        private SerializedProperty _damageProperty;
        private SerializedProperty _minDamageProperty;
        private SerializedProperty _maxDamageProperty;

        private SerializedProperty _typeProperty;
        private SerializedProperty _ammoCapacityProperty;
        private SerializedProperty _bulletSpeedProperty;
        private SerializedProperty _bulletTTLSecondsProperty;

        private SerializedProperty _maxAmountProperty;
        private SerializedProperty _thrownDelayProperty;
        private SerializedProperty _destroyTypeProperty;
        private SerializedProperty _TTLSecondsProperty;


        private void OnEnable()
        {
            _nameProperty = serializedObject.FindProperty("_name");
            _descriptionProperty = serializedObject.FindProperty("_description");
            _iconProperty = serializedObject.FindProperty("_icon");
            _priceProperty = serializedObject.FindProperty("_price");

            _attackDelayProperty = serializedObject.FindProperty("_attackDelay");
            _damageTypeProperty = serializedObject.FindProperty("_damageType");
            _damageTypeUIProperty = serializedObject.FindProperty("_damageTypeUI");
            _damageProperty = serializedObject.FindProperty("_damage");
            _minDamageProperty = serializedObject.FindProperty("_minDamage");
            _maxDamageProperty = serializedObject.FindProperty("_maxDamage");

            _typeProperty = serializedObject.FindProperty("_type");
            _ammoCapacityProperty = serializedObject.FindProperty("_ammoCapacity");
            _bulletSpeedProperty = serializedObject.FindProperty("_bulletSpeed");
            _bulletTTLSecondsProperty = serializedObject.FindProperty("_bulletTTLSeconds");

            _maxAmountProperty = serializedObject.FindProperty("_maxAmount");
            _thrownDelayProperty = serializedObject.FindProperty("_thrownDelay");
            _destroyTypeProperty = serializedObject.FindProperty("_destroyType");
            _TTLSecondsProperty = serializedObject.FindProperty("_TTLSeconds");

        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(_nameProperty);
            EditorGUILayout.PropertyField(_descriptionProperty);
            EditorGUILayout.PropertyField(_iconProperty);
            EditorGUILayout.PropertyField(_priceProperty);

            EditorGUILayout.PropertyField(_attackDelayProperty);
            EditorGUILayout.PropertyField(_damageTypeProperty);
            EditorGUILayout.PropertyField(_damageTypeUIProperty);
            WeaponDamageType damageType = (WeaponDamageType)_damageTypeProperty.intValue;
            switch (damageType)
            {
                case WeaponDamageType.Static:
                    EditorGUILayout.PropertyField(_damageProperty);
                    break;
                case WeaponDamageType.Random:
                    EditorGUILayout.PropertyField(_minDamageProperty);
                    EditorGUILayout.PropertyField(_maxDamageProperty);
                    break;
                default:
                    throw new NotImplementedException();
            }

            EditorGUILayout.PropertyField(_typeProperty);
            WeaponType type = (WeaponType)_typeProperty.intValue;
            switch (type)
            {
                case WeaponType.Melee:
                    break;
                case WeaponType.Gun:
                    EditorGUILayout.PropertyField(_ammoCapacityProperty);
                    EditorGUILayout.PropertyField(_bulletSpeedProperty);
                    EditorGUILayout.PropertyField(_bulletTTLSecondsProperty);
                    break;
                case WeaponType.Trap:
                    EditorGUILayout.PropertyField(_maxAmountProperty);
                    EditorGUILayout.PropertyField(_thrownDelayProperty);
                    EditorGUILayout.PropertyField(_destroyTypeProperty);
                    TrapDestroyType destroyType = (TrapDestroyType)_destroyTypeProperty.intValue;
                    switch (destroyType)
                    {
                        case TrapDestroyType.AfterAttack:
                            break;
                        case TrapDestroyType.TimeLimit:
                            EditorGUILayout.PropertyField(_TTLSecondsProperty);
                            break;
                        default:
                            throw new NotImplementedException();
                    }
                    break;
                default:
                    throw new NotImplementedException();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
