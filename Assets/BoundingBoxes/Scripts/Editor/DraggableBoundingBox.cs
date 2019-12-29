#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using BoundingBoxes;

namespace EditorUtilities
{
    /// <summary>
    /// Display a draggable bounding box when enabled.
    /// </summary>
    [CustomEditor(typeof(BoundingBox), true)]
    public class DraggableBoundingBox : Editor
    {
        readonly GUIStyle style = new GUIStyle();
        private void OnEnable()
        {
            style.fontStyle = FontStyle.Bold;
            style.normal.textColor = Color.white;
            style.alignment = TextAnchor.UpperCenter;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            BoundingBox b = target as BoundingBox;
            bool tmpDisplay = b.Visible();
            b.SetVisible(GUILayout.Toggle(b.Visible(), "Edit Bounding Box"));
            if (tmpDisplay != b.Visible())
            {
                SceneView.RepaintAll();
            }

            if (GUILayout.Button("Reset center to parent"))
            {
                b.Center = b.transform.position;
                SceneView.RepaintAll();
            }

            if (GUILayout.Button("Reset Box to defaults"))
            {
                b.LeftRange = -5;
                b.UpRange = 5;
                b.RightRange = 5;
                b.DownRange = -5;
                b.Center = b.transform.position;
                SceneView.RepaintAll();
            }

        }

        public void OnSceneGUI()
        {
            BoundingBox b = target as BoundingBox;
            if (!b.enabled || !b.Visible())
            {
                return;
            }

            Vector3 pos = b.Center;


            string labelText = b.name + " Bounding Box";
            Handles.Label(new Vector3(b.Center.x+b.TopLeft().x, b.Center.y+b.TopLeft().y), labelText);

            Vector3[] verts = {
                    b.TopLeft(),
                    b.BottomLeft(),
                    b.BottomRight(),
                    b.TopRight(),
            };


            Handles.DrawSolidRectangleWithOutline(verts, new Color(0.5f, 0.5f, 0.5f, 0.1f), new Color(0, 0, 0, 1));

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
        }
    }
}
#endif