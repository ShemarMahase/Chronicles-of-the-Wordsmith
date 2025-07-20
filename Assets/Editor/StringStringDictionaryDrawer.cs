using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

// --- Editor Folder ---
// Assets/Editor/StringStringDictionaryDrawer.cs

[CustomPropertyDrawer(typeof(StringStringDictionary))]
public class StringStringDictionaryDrawer : PropertyDrawer
{
    private const float LineHeight = 18f;
    private const float ButtonWidth = 25f;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        // Get the serialized properties for keys and values lists
        SerializedProperty keysProperty = property.FindPropertyRelative("_keys");
        SerializedProperty valuesProperty = property.FindPropertyRelative("_values");

        // Calculate height for each element, plus space for the header and add button
        float height = LineHeight; // For the foldout header
        if (property.isExpanded)
        {
            height += (Mathf.Max(keysProperty.arraySize, valuesProperty.arraySize) * LineHeight); // For each item
            height += LineHeight; // For the Add button
        }
        return height;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        SerializedProperty keysProperty = property.FindPropertyRelative("_keys");
        SerializedProperty valuesProperty = property.FindPropertyRelative("_values");

        // Draw the foldout header
        Rect foldoutRect = new Rect(position.x, position.y, position.width, LineHeight);
        property.isExpanded = EditorGUI.Foldout(foldoutRect, property.isExpanded, label);

        if (property.isExpanded)
        {
            // Indent content
            EditorGUI.indentLevel++;
            position.y += LineHeight;

            int currentSize = Mathf.Max(keysProperty.arraySize, valuesProperty.arraySize);

            // Draw each key-value pair
            for (int i = 0; i < currentSize; i++)
            {
                // Ensure lists are synchronized for drawing
                if (keysProperty.arraySize <= i) keysProperty.arraySize = i + 1;
                if (valuesProperty.arraySize <= i) valuesProperty.arraySize = i + 1;

                Rect elementRect = new Rect(position.x, position.y + (i * LineHeight), position.width, LineHeight);

                // Calculate width for key, value, and remove button
                float keyWidth = (elementRect.width - ButtonWidth) * 0.45f; // 45% for key
                float valueWidth = (elementRect.width - ButtonWidth) * 0.55f; // 55% for value

                Rect keyRect = new Rect(elementRect.x, elementRect.y, keyWidth, LineHeight);
                Rect valueRect = new Rect(elementRect.x + keyWidth + 5f, elementRect.y, valueWidth - 5f, LineHeight); // 5f padding
                Rect removeButtonRect = new Rect(elementRect.x + elementRect.width - ButtonWidth, elementRect.y, ButtonWidth, LineHeight);

                // Draw key field
                EditorGUI.PropertyField(keyRect, keysProperty.GetArrayElementAtIndex(i), GUIContent.none);

                // Draw value field
                EditorGUI.PropertyField(valueRect, valuesProperty.GetArrayElementAtIndex(i), GUIContent.none);

                // Draw remove button
                if (GUI.Button(removeButtonRect, "-"))
                {
                    keysProperty.DeleteArrayElementAtIndex(i);
                    valuesProperty.DeleteArrayElementAtIndex(i);
                    // Adjust loop counter as array size changed
                    currentSize--;
                    i--;
                }
            }

            // Draw Add button
            Rect addButtonRect = new Rect(position.x + position.width - ButtonWidth, position.y + (currentSize * LineHeight), ButtonWidth, LineHeight);
            if (GUI.Button(addButtonRect, "+"))
            {
                keysProperty.arraySize++;
                valuesProperty.arraySize++;
                // Initialize new elements if necessary (e.g., to empty strings)
                keysProperty.GetArrayElementAtIndex(keysProperty.arraySize - 1).stringValue = "";
                valuesProperty.GetArrayElementAtIndex(valuesProperty.arraySize - 1).stringValue = "";
            }

            EditorGUI.indentLevel--;
        }

        EditorGUI.EndProperty();
    }
}