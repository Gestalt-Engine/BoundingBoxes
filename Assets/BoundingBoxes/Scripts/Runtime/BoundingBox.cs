using UnityEngine;
using UnityEditor;

namespace BoundingBoxes
{
        /// <summary>
        /// BoundingBox2D provides behavior for setting a rectangular bounding box
        /// and helper functions to determine its top / right / bottom /left
        /// bounds for use with a 2D camera, movement limitations, etc
        /// </summary>

    public class BoundingBox2D : MonoBehaviour
    {

        private bool DisplayBoundingBox;
        private bool DisplayBoundingBoxLabel;
        public float UpRange = 5;
        public float DownRange = -5;
        public float LeftRange = -5;
        public float RightRange = 5;

        public Vector3 Center;

        private Color LabelTextColor = Color.black;

        private Color FillColor = new Color(0.5f, 0.5f, 0.5f, 0.1f);
        private Color OutlineColor = new Color(0, 0, 0, 1);

        /// <summary>
        /// Reset this instance to defaults.
        /// </summary>
        public void Reset()
        {
            LeftRange = -5;
            UpRange = 5;
            RightRange = 5;
            DownRange = -5;
            Center = transform.position;
            SetFillColor(new Color(0.5f, 0.5f, 0.5f, 0.1f));
            SetOutlineColor(new Color(0, 0, 0, 1));
            SetTextColor(Color.black);
        }

        /// <summary>
        /// Sets the color of the label text.
        /// </summary>
        /// <param name="col">Col.</param>
        public void SetTextColor(Color col)
        {
            LabelTextColor = col;
        }

        /// <summary>
        /// Gets the color of the label text.
        /// </summary>
        /// <returns>The text color.</returns>
        public Color GetTextColor()
        {
            return LabelTextColor;
        }

        public void SetOutlineColor(Color line)
        {
            OutlineColor = line;
        }

        public Color GetOutlineColor()
        {
            return OutlineColor;
        }

        public void SetFillColor(Color fill)
        {
            FillColor = fill;
        }

        public Color GetFillColor()
        {
            return FillColor;
        }

        public bool LabelVisible()
        {
            return DisplayBoundingBoxLabel;
        }

        public void SetLabelVisible(bool val)
        {
            DisplayBoundingBoxLabel = val;
        }

        public bool Visible()
        {
            return DisplayBoundingBox;
        }

        public void SetVisible(bool val)
        {
            DisplayBoundingBox = val;
        }

        public Vector3 TopLeft()
        {
            return new Vector3(Center.x + LeftRange, Center.y + UpRange);
        }

        public float GetLeftRange()
        {
            return Center.x + LeftRange;
        }

        public Vector3 TopRight()
        {
            return new Vector3(Center.x + RightRange, Center.y + UpRange);
        }

        public float GetRightRange()
        {
            return Center.x + RightRange;
        }            

        public Vector3 BottomLeft()
        {
            return new Vector3(Center.x + LeftRange, Center.y + DownRange);
        }

        public Vector3 BottomRight()
        {
            return new Vector3(Center.x + RightRange, Center.y + DownRange);
        }

        public float GetDownRange()
        {
            return Center.y + DownRange;
        }

        public Vector3 LeftMiddle()
        {
            float yMiddle = Center.y + (UpRange + DownRange);
            return new Vector3(Center.x + LeftRange, yMiddle);
        }

        public Vector3 RightMiddle()
        {
            float yMiddle = Center.y + (UpRange + DownRange);
            return new Vector3(Center.x + RightRange, yMiddle);
        }

        public Vector3 TopMiddle()
        {
            float xMiddle = Center.x + (RightRange + LeftRange);
            return new Vector3(xMiddle, Center.y + UpRange);
        }

        public float GetUpRange()
        {
            return Center.y + UpRange;
        }

        public Vector3 BottomMiddle()
        {
            float xMiddle = Center.x + (RightRange + LeftRange);
            return new Vector3(xMiddle, Center.y + DownRange);
        }
    }
}