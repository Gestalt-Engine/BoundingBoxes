using UnityEngine;
using UnityEditor;

namespace BoundingBoxes
{
        /// <summary>
        /// Bounding box provides behavior for setting a rectangular bounding box
        /// and helper functions to determine its top / right / bottom /left
        /// bounds for use with a 2D camera, movement limitations, etc
        /// </summary>
        public class BoundingBox : MonoBehaviour
        {

            private bool DisplayBoundingBox;
            public float UpRange = 5;
            public float DownRange = -5;
            public float LeftRange = -5;
            public float RightRange = 5;

            public Vector3 Center;

            public bool Visible()
            {
                return this.DisplayBoundingBox;
            }

            public void SetVisible(bool val)
            {
                this.DisplayBoundingBox = val;
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