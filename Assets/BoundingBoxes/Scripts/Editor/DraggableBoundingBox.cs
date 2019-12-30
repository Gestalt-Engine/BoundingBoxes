#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using BoundingBoxes;
using System.Text.RegularExpressions;

namespace EditorUtilities
{
    [CustomEditor(typeof(BoundingBox2D), true)]
    public class DraggableBoundingBox2D : Editor
    {
        BoundingBox2D b;
        readonly GUIStyle style = new GUIStyle();
        readonly string LabelSuffix = "Bounding box";
        bool _boundingBoxDropdownOpen = false;

        /// <summary>
        /// Updates the style and text color.
        /// </summary>
        private void UpdateStyle()
        {
            style.fontStyle = FontStyle.Bold;
            style.normal.textColor = b.GetTextColor();
            style.alignment = TextAnchor.UpperRight;
        }

        private void OnEnable()
        {
            b = target as BoundingBox2D;
            UpdateStyle();
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            BuildPropertyEditor();

        }

        /// <summary>
        /// Builds the GUI property editor.
        /// </summary>
        public void BuildPropertyEditor()
        {
            serializedObject.Update();
            EditorGUI.BeginChangeCheck();

            bool willDoReset = false;
            bool willResetCenter = false;

            /// Toggle visibility of the bounding box in the editor
            b.SetVisible(GUILayout.Toggle(b.Visible(), "Edit Bounding Box"));

            /// Dropdown to edit bounding box visual properties 
            _boundingBoxDropdownOpen = EditorGUILayout.Foldout(_boundingBoxDropdownOpen, "Bounding Box Properties");
            if (_boundingBoxDropdownOpen)
            {

                SerializedProperty fillColor = serializedObject.FindProperty("_fillColor");
                SerializedProperty outlineColor = serializedObject.FindProperty("_outlineColor");
                SerializedProperty textColor = serializedObject.FindProperty("_labelTextColor");

                // Resets the center of the bounding box to its transform
                willResetCenter = GUILayout.Button("Reset center to parent");

                // Resets all bounding box settings to defaults
                willDoReset = GUILayout.Button("Reset Box to defaults");

                // Updates fill color of the box
                EditorGUILayout.LabelField("Fill Color");
                EditorGUILayout.PropertyField(fillColor, GUIContent.none, GUILayout.Width(127));

                // Updates the outline color of the box
                EditorGUILayout.LabelField("Outline Color");
                EditorGUILayout.PropertyField(outlineColor, GUIContent.none, GUILayout.Width(127));

                // Updates the text color of the box label
                EditorGUILayout.LabelField("Text Color");
                EditorGUILayout.PropertyField(textColor, GUIContent.none, GUILayout.Width(127));

                serializedObject.ApplyModifiedProperties();
            }

            /// Repaint scene if changes occured
            if (EditorGUI.EndChangeCheck())
            {
                if (willResetCenter)
                {
                    b.Center = b.transform.position;
                }

                if (willDoReset)
                {
                    b.Reset();
                }
                SceneView.RepaintAll();
            }
        }

        public void OnSceneGUI()
        {
            if (!b.enabled || !b.Visible())
            {
                return;
            }

            DrawBoundingBox();
            DrawBoundingBoxHandles();

        }

        /// <summary>
        /// Draws the bounding box.
        /// </summary>
        public void DrawBoundingBox()
        {
            // update the label
            UpdateStyle();
            Handles.Label(new Vector3(b.Center.x + b.TopLeft().x, b.Center.y + b.TopLeft().y), FormatBoundingBoxLabel(b.name), style);

            // build verticies
            Vector3[] verts = {
                    b.TopLeft(),
                    b.BottomLeft(),
                    b.BottomRight(),
                    b.TopRight(),
            };

            // draw rectangle
            Handles.DrawSolidRectangleWithOutline(verts, b.GetFillColor(), b.GetOutlineColor());
        }

        /// <summary>
        /// Draws the bounding box handles.
        /// </summary>
        public void DrawBoundingBoxHandles()
        {
            serializedObject.Update();
            Undo.RecordObject(b, "Modify bounding box");

            Vector3 TopLeft = Handles.PositionHandle(b.TopLeft(), Quaternion.Euler(0, 180, 0));
            b.SetLeftRange(TopLeft.x);
            b.SetUpRange(TopLeft.y);

            Vector3 BottomLeft = Handles.PositionHandle(b.BottomLeft(), Quaternion.Euler(0, 0, 180));
            b.SetLeftRange(BottomLeft.x);
            b.SetDownRange(BottomLeft.y);

            Vector3 TopRight = Handles.PositionHandle(b.TopRight(), Quaternion.identity);
            b.SetRightRange(TopRight.x);
            b.SetUpRange(TopRight.y);

            Vector3 BottomRight = Handles.PositionHandle(b.BottomRight(), Quaternion.Euler(0, 180, 180));
            b.SetRightRange(BottomRight.x);
            b.SetDownRange(BottomRight.y);

            serializedObject.ApplyModifiedProperties();
        }

        /// <summary>
        /// Formats the bounding box label.
        /// </summary>
        /// <returns>The bounding box label.</returns>
        /// <param name="name">Name.</param>
        public string FormatBoundingBoxLabel(string name)
        {
            return name + LabelSuffix;
        }

        // end class
    }
}
#endif