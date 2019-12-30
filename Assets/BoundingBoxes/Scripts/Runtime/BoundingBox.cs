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

        private bool _displayBoundingBox = true;
        public float UpRange = 5;
        public float DownRange = -5;
        public float LeftRange = -5;
        public float RightRange = 5;

        public Vector3 Center;

        [SerializeField, HideInInspector] private Color _labelTextColor = Color.black;

        [SerializeField, HideInInspector] private Color _fillColor = new Color(0.5f, 0.5f, 0.5f, 0.1f);

        [SerializeField, HideInInspector] private Color _outlineColor = new Color(0, 0, 0, 1);

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
            _labelTextColor = col;
        }

        /// <summary>
        /// Gets the color of the label text.
        /// </summary>
        /// <returns>The text color.</returns>
        public Color GetTextColor()
        {
            return _labelTextColor;
        }

        /// <summary>
        /// Sets the color of the outline.
        /// </summary>
        /// <param name="line">Line.</param>
        public void SetOutlineColor(Color line)
        {
            _outlineColor = line;
        }

        /// <summary>
        /// Gets the color of the outline.
        /// </summary>
        /// <returns>The outline color.</returns>
        public Color GetOutlineColor()
        {
            return _outlineColor;
        }

        /// <summary>
        /// Sets the color of the fill.
        /// </summary>
        /// <param name="fill">Fill.</param>
        public void SetFillColor(Color fill)
        {
            _fillColor = fill;
        }

        /// <summary>
        /// Gets the color of the fill.
        /// </summary>
        /// <returns>The fill color.</returns>
        public Color GetFillColor()
        {
            return _fillColor;
        }

        /// <summary>
        /// Whether bounding box is visible
        /// </summary>
        /// <returns>The visible.</returns>
        public bool Visible()
        {
            return _displayBoundingBox;
        }

        /// <summary>
        /// Sets if bounding box is visible.
        /// </summary>
        /// <param name="val">If set to <c>true</c> value.</param>
        public void SetVisible(bool val)
        {
            _displayBoundingBox = val;
        }

        /// <summary>
        /// Sets up range.
        /// </summary>
        /// <param name="val">Value.</param>
        public void SetUpRange(float val)
        {
            if (val > Center.y)
            {
                UpRange = val - Center.y;
            }
        }

        /// <summary>
        /// Sets down range.
        /// </summary>
        /// <param name="val">Value.</param>
        public void SetDownRange(float val)
        {
            if (val < Center.y)
            {
                DownRange = val - Center.y;
            }
        }

        /// <summary>
        /// Sets the left range.
        /// </summary>
        /// <param name="val">Value.</param>
        public void SetLeftRange(float val)
        {
            if (val < Center.x)
            {
                LeftRange = val - Center.x;
            }
        }

        /// <summary>
        /// Sets the right range.
        /// </summary>
        /// <param name="val">Value.</param>
        public void SetRightRange(float val)
        {
            if (val > Center.x)
            {
                RightRange = val - Center.x;
            }
        }

        /// <summary>
        /// Returns the Top Left point.
        /// </summary>
        /// <returns>The left.</returns>
        public Vector3 TopLeft()
        {
            return new Vector3(Center.x + LeftRange, Center.y + UpRange);
        }

        /// <summary>
        /// Gets the left range.
        /// </summary>
        /// <returns>The left range.</returns>
        public float GetLeftRange()
        {
            return Center.x + LeftRange;
        }

        /// <summary>
        /// Returns the Top Right point.
        /// </summary>
        /// <returns>The right.</returns>
        public Vector3 TopRight()
        {
            return new Vector3(Center.x + RightRange, Center.y + UpRange);
        }

        /// <summary>
        /// Gets the right range.
        /// </summary>
        /// <returns>The right range.</returns>
        public float GetRightRange()
        {
            return Center.x + RightRange;
        }            

        /// <summary>
        /// Returns the Bottom Left point.
        /// </summary>
        /// <returns>The left.</returns>
        public Vector3 BottomLeft()
        {
            return new Vector3(Center.x + LeftRange, Center.y + DownRange);
        }

        /// <summary>
        /// Returns the Bottom Right point.
        /// </summary>
        /// <returns>The right.</returns>
        public Vector3 BottomRight()
        {
            return new Vector3(Center.x + RightRange, Center.y + DownRange);
        }

        /// <summary>
        /// Gets down range.
        /// </summary>
        /// <returns>The down range.</returns>
        public float GetDownRange()
        {
            return Center.y + DownRange;
        }

        /// <summary>
        /// Returns the Left Middle point
        /// </summary>
        /// <returns>The middle.</returns>
        public Vector3 LeftMiddle()
        {
            float yMiddle = Center.y + (UpRange + DownRange);
            return new Vector3(Center.x + LeftRange, yMiddle);
        }

        /// <summary>
        /// Returns the Right Middle point.
        /// </summary>
        /// <returns>The middle.</returns>
        public Vector3 RightMiddle()
        {
            float yMiddle = Center.y + (UpRange + DownRange);
            return new Vector3(Center.x + RightRange, yMiddle);
        }

        /// <summary>
        /// Returns the Top Middle point.
        /// </summary>
        /// <returns>The middle.</returns>
        public Vector3 TopMiddle()
        {
            float xMiddle = Center.x + (RightRange + LeftRange);
            return new Vector3(xMiddle, Center.y + UpRange);
        }

        /// <summary>
        /// Gets up range.
        /// </summary>
        /// <returns>The up range.</returns>
        public float GetUpRange()
        {
            return Center.y + UpRange;
        }

        /// <summary>
        /// Returns the Bottom Middle point.
        /// </summary>
        /// <returns>The middle.</returns>
        public Vector3 BottomMiddle()
        {
            float xMiddle = Center.x + (RightRange + LeftRange);
            return new Vector3(xMiddle, Center.y + DownRange);
        }

        /// <summary>
        /// Basic float comparison to determine if two floats are nearly equal.
        /// </summary>
        /// <returns><c>true</c>, if equal within epsilon, <c>false</c> otherwise.</returns>
        /// <param name="a">The alpha component.</param>
        /// <param name="b">The blue component.</param>
        /// <param name="epsilon">Epsilon.</param>
        private bool NearlyEqual(float a, float b, float epsilon)
        {
            float absA = Mathf.Abs(a);
            float absB = Mathf.Abs(b);
            float diff = Mathf.Abs(a - b);

            if (System.Math.Abs(a - b) < epsilon)
            { // shortcut, handles infinities
                return true;
            }

            else if (System.Math.Abs(a) < epsilon || System.Math.Abs(b) < epsilon || absA + absB < float.MinValue)
            {
                // a or b is zero or both are extremely close to it
                // relative error is less meaningful here
                return diff < (epsilon * float.MinValue);
            }
            else
            { // use relative error
                return diff / (absA + absB) < epsilon;
            }
        }
    }
}