using UnityEditor;

namespace Creatures.Enemy.Editor
{
    [CustomEditor(typeof(EnemyAttack))]
    public class EnemyAttackEditor : UnityEditor.Editor
    {
        private SerializedProperty _animationTriggerProperty;
        private SerializedProperty _damageProperty;
        private SerializedProperty _cooldownTimeProperty;

        private SerializedProperty _haveDistanceProperty;
        private SerializedProperty _distanceProperty;

        private void OnEnable()
        {
            _animationTriggerProperty = serializedObject.FindProperty("_animationTrigger");
            _damageProperty = serializedObject.FindProperty("_damage");
            _cooldownTimeProperty = serializedObject.FindProperty("_cooldownTime");

            _haveDistanceProperty = serializedObject.FindProperty("_haveDistance");
            _distanceProperty = serializedObject.FindProperty("_distance");
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(_animationTriggerProperty);
            EditorGUILayout.PropertyField(_damageProperty);
            EditorGUILayout.PropertyField(_cooldownTimeProperty);

            EditorGUILayout.PropertyField(_haveDistanceProperty);
            bool haveDistance = _haveDistanceProperty.boolValue;
            if(haveDistance)
                EditorGUILayout.PropertyField(_distanceProperty);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
