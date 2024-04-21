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
        private SerializedProperty _soundProperty;

        private void OnEnable()
        {
            _attackRangeProperty = serializedObject.FindProperty("_attackRange");

            _modeProperty = serializedObject.FindProperty("_mode");
            _ttlProperty = serializedObject.FindProperty("_ttl");
            _soundProperty = serializedObject.FindProperty("_sound");
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(_attackRangeProperty);

            EditorGUILayout.PropertyField(_modeProperty);
            EditorGUILayout.PropertyField(_soundProperty);
            TrapDestroyType mode = (TrapDestroyType)_modeProperty.intValue;
            switch (mode)
            {
                case TrapDestroyType.TimeLimit:
                    EditorGUILayout.PropertyField(_ttlProperty);
                    break;
                default:
                    break;
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}
