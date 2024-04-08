using UnityEditor;
using static Creatures.Enemy.EnemyAttack;
using static Weapons.Weapon;

namespace Creatures.Enemy.Editor
{
    [CustomEditor(typeof(EnemyAttack))]
    public class EnemyAttackEditor : UnityEditor.Editor
    {
        private SerializedProperty _animationTriggerProperty;
        private SerializedProperty _damageProperty;
        private SerializedProperty _cooldownTimeProperty;
        private SerializedProperty _attackTypeProperty;

        private SerializedProperty _distanceProperty;
        private SerializedProperty _initialPositionProperty;

        private void OnEnable()
        {
            _animationTriggerProperty = serializedObject.FindProperty("_animationTrigger");
            _damageProperty = serializedObject.FindProperty("_damage");
            _cooldownTimeProperty = serializedObject.FindProperty("_cooldownTime");
            _attackTypeProperty = serializedObject.FindProperty("_attackType");

            _distanceProperty = serializedObject.FindProperty("_distance");
            _initialPositionProperty = serializedObject.FindProperty("_initialPosition");
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(_animationTriggerProperty);
            EditorGUILayout.PropertyField(_damageProperty);
            EditorGUILayout.PropertyField(_cooldownTimeProperty);
            EditorGUILayout.PropertyField(_attackTypeProperty);

            EnemyAttackType attackType = (EnemyAttackType)_attackTypeProperty.intValue;
            switch (attackType)
            {
                case EnemyAttackType.WithDistance:
                    EditorGUILayout.PropertyField(_distanceProperty);
                    break;
                case EnemyAttackType.WithInitialPosition:
                    EditorGUILayout.PropertyField(_initialPositionProperty);
                    break;
                default:
                    break;
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
