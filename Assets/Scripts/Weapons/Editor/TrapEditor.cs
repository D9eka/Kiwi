using UnityEditor;
using static Weapons.Weapon;

namespace Weapons.Editor
{
    [CustomEditor(typeof(Trap))]
    public class TrapEditor : UnityEditor.Editor
    {
        private SerializedProperty _attackDelayProperty;
        private SerializedProperty _modeProperty;

        private SerializedProperty _damageProperty;

        private SerializedProperty _minDamageProperty;
        private SerializedProperty _maxDamageProperty;

        private SerializedProperty _thrownTrapProperty;
        private SerializedProperty _thrownTrapAttackDelayProperty;
        private SerializedProperty _throwForceProperty;

        private SerializedProperty _maxAmountProperty;

        private void OnEnable()
        {
            _attackDelayProperty = serializedObject.FindProperty("_attackDelay");
            _modeProperty = serializedObject.FindProperty("_mode");

            _damageProperty = serializedObject.FindProperty("_damage");

            _minDamageProperty = serializedObject.FindProperty("_minDamage");
            _maxDamageProperty = serializedObject.FindProperty("_maxDamage");

            _thrownTrapProperty = serializedObject.FindProperty("_thrownTrap");
            _thrownTrapAttackDelayProperty = serializedObject.FindProperty("_thrownTrapAttackDelay");
            _throwForceProperty = serializedObject.FindProperty("_throwForce");

            _maxAmountProperty = serializedObject.FindProperty("_maxAmount");
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(_attackDelayProperty);
            EditorGUILayout.PropertyField(_modeProperty);

            Weapon.DamageType mode = (Weapon.DamageType)_modeProperty.intValue;
            switch (mode)
            {
                case Weapon.DamageType.Static:
                    EditorGUILayout.PropertyField(_damageProperty);
                    break;
                case Weapon.DamageType.Random:
                    EditorGUILayout.PropertyField(_minDamageProperty);
                    EditorGUILayout.PropertyField(_maxDamageProperty);
                    break;
                default:
                    break;
            }

            EditorGUILayout.PropertyField(_thrownTrapProperty);
            EditorGUILayout.PropertyField(_thrownTrapAttackDelayProperty);
            EditorGUILayout.PropertyField(_throwForceProperty);

            EditorGUILayout.PropertyField(_maxAmountProperty);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
