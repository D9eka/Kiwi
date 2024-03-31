using Components.Health;
using UnityEditor;
using UnityEngine;
using Weapons;
using static Weapons.ThrownTrap;

namespace Weapons.Editor
{
    [CustomEditor(typeof(ThrownTrap))]
    public class ThrownTrapEditor : UnityEditor.Editor
    {
        private SerializedProperty _attackRangeProperty;

        private SerializedProperty _modeProperty;
        private SerializedProperty _ttlProperty;

        private void OnEnable()
        {
            _attackRangeProperty = serializedObject.FindProperty("_attackRange");

            _modeProperty = serializedObject.FindProperty("_mode");
            _ttlProperty = serializedObject.FindProperty("_ttl");
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(_attackRangeProperty);

            EditorGUILayout.PropertyField(_modeProperty);

            DestroyType mode = (DestroyType)_modeProperty.intValue;
            switch (mode)
            {
                case DestroyType.TimeLimit:
                    EditorGUILayout.PropertyField(_ttlProperty);
                    break;
                default:
                    break;
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}
