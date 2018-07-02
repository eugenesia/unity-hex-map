/**
 * Customise the look of hex coords in the Inspector during game mode
 * by using a Property Drawer.
 */
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(HexCoordinates))]
public class HexCoordinatesDrawer : PropertyDrawer {
	public override void OnGUI(
        Rect position, SerializedProperty property, GUIContent label) {

        // Convert to hex coords object.
        HexCoordinates coordinates = new HexCoordinates(
            property.FindPropertyRelative("x").intValue,
            property.FindPropertyRelative("z").intValue
        );

        // Set field name in left column.
        // Return an adjusted rectangle matching the space to right of label.
        position = EditorGUI.PrefixLabel(position, label);

        GUI.Label(position, coordinates.ToString());
	}
}
