#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using BoundingBoxes;

namespace EditorUtilities
{
    /// <summary>
    /// Display a draggable bounding box when enabled.
    /// </summary>
    [CustomEditor(typeof(BoundingBox2D), true)]
    public class DraggableBoundingBox2D : Editor
    {
        readonly string LabelSuffix = "Bounding Box";
        readonly GUIStyle style = new GUIStyle();
        bool showProperties = false;
        bool showBoxOptions = false;

        private void OnEnable()
        {
            UpdateEditorView();

        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            BoundingBox2D b = target as BoundingBox2D;

            BuildEditor();

        }

        public void OnSceneGUI()
        {
            BoundingBox2D b = target as BoundingBox2D;
            if (!b.enabled || !b.Visible())
            {
                return;
            }

            Vector3 pos = b.Center;

            DrawBoundingBox(b);

            Vector3 TopLeft = Handles.PositionHandle(b.TopLeft(), Quaternion.Euler(0, 180, 0));
            if (TopLeft.x < b.Center.x)
            {
                b.LeftRange = TopLeft.x - b.Center.x;
            }
            if (TopLeft.y > b.Center.y)
            {
                b.UpRange = TopLeft.y - b.Center.y;
            }


            Vector3 TopRight = Handles.PositionHandle(b.TopRight(), Quaternion.identity);
            if (TopRight.y > b.Center.y)
            {
                b.UpRange = TopRight.y - b.Center.y;
            }
            if (TopRight.x > b.Center.x)
            {
                b.RightRange = TopRight.x - b.Center.x;
            }

            Vector3 BottomLeft = Handles.PositionHandle(b.BottomLeft(), Quaternion.Euler(0, 0, 180));
            if (BottomLeft.x < b.Center.x)
            {
                b.LeftRange = BottomLeft.x - b.Center.x;
            }
            if (BottomLeft.y < b.Center.y)
            {
                b.DownRange = BottomLeft.y - b.Center.y;
            }


            Vector3 BottomRight = Handles.PositionHandle(b.BottomRight(), Quaternion.Euler(0, 180, 180));
            if (BottomRight.x > b.Center.x)
            {
                b.RightRange = BottomRight.x - b.Center.x;
            }
            if (BottomRight.y < b.Center.y)
            {
                b.DownRange = BottomRight.y - b.Center.y;
            }

            //serializedObject.ApplyModifiedProperties();
        }

        /// <summary>
        /// Updates the editor view from changes to BoundingBox2D settings
        /// </summary>
        public void UpdateEditorView()
        {
            BoundingBox2D b = target as BoundingBox2D;
            style.fontStyle = FontStyle.Bold;
            style.normal.textColor = b.GetTextColor();
            style.alignment = TextAnchor.UpperRight;
        }

        /// <summary>
        /// Builds the editor settings and draws.
        /// </summary>
        public void BuildEditor()
        {
            BoundingBox2D b = target as BoundingBox2D;

            bool tmpDisplay = b.Visible();
            b.SetVisible(GUILayout.Toggle(b.Visible(), "Edit Bounding Box"));
            if (tmpDisplay != b.Visible())
            {
                SceneView.RepaintAll();
            }
            showBoxOptions = EditorGUILayout.Foldout(showBoxOptions, "Bounding Box");
            if (showBoxOptions)
            {
                // Resets the center of the bounding box to its transform
                if (GUILayout.Button("Reset center to parent"))
                {
                    b.Center = b.transform.position;
                    SceneView.RepaintAll();
                }

                // Resets all bounding box settings to defaults
                if (GUILayout.Button("Reset Box to defaults"))
                {
                    b.Reset();
                    SceneView.RepaintAll();
                }

                // Dropdown to display and edit box appearance preferences
                showProperties = EditorGUILayout.Foldout(showProperties, "Colors");
                if (showProperties)
                {
                    Color fillColor = EditorGUILayout.ColorField("Box Fill Color", b.GetFillColor());
                    b.SetFillColor(fillColor);

                    Color lineColor = EditorGUILayout.ColorField("Box Outline Color", b.GetOutlineColor());
                    b.SetOutlineColor(lineColor);

                    Color textColor = EditorGUILayout.ColorField("Box Label Text Color", b.GetTextColor());
                    b.SetTextColor(textColor);
                }
            }
        }

        /// <summary>
        /// Draws the bounding box.
        /// </summary>
        /// <param name="b">The BoundingBox2D component.</param>
        public void DrawBoundingBox(BoundingBox2D b)
        {
            // update the label
            UpdateEditorView();
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
        /// Formats the bounding box label.
        /// </summary>
        /// <returns>The bounding box label.</returns>
        /// <param name="name">Name.</param>
        public string FormatBoundingBoxLabel(string name)
        {
            return name + LabelSuffix;
        }
    }
}
#endif