using UnityEngine;
using UnityEditor;
[ExecuteInEditMode]
public class ShowGlobalTransform : MonoBehaviour
{
    [Header("Global Transform (Read-Only)")]
    [ReadOnly] public Vector3 globalPosition;
    [ReadOnly] public Vector3 globalRotation;
    [ReadOnly] public Vector3 globalScale;

    void Update()
    {
        globalPosition = transform.position;
        globalRotation = transform.eulerAngles;
        globalScale = transform.lossyScale;
    }
}
public class ReadOnlyAttribute : PropertyAttribute { }

#if UNITY_EDITOR

[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, label, true);
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        GUI.enabled = false;
        EditorGUI.PropertyField(position, property, label, true);
        GUI.enabled = true;
    }
}
#endif
