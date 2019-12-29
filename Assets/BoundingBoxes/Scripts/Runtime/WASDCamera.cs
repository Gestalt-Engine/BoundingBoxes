using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BoundingBoxes
{
    /// <summary>
    /// WASDCamera provides a basic 2D camera movement script
    // that utilizes the bounding box for its allowed movement range
    /// </summary>
    [AddComponentMenu("Scripts/WASDCamera")]
    public class WASDCamera : BoundingBox
    {
        public bool CameraLocked = false;
        public Camera cam;

        public void Start()
        {
            if (cam == null){
                cam = Camera.main;
            }
        }

        private void FixedUpdate()
        {
            if (!CameraLocked)
            {
                float xAxisValue = Input.GetAxis("Horizontal");
                float yAxisValue = Input.GetAxis("Vertical");
                Vector3 cameraMove = new Vector3(xAxisValue, yAxisValue, 0.0f);

                if (cam != null)
                {
                    cam.transform.Translate(cameraMove);

                    if (cam.transform.position.x < GetLeftRange())
                    {
                        cam.transform.position = new Vector3(GetLeftRange(), cam.transform.position.y, cam.transform.position.z);
                    }
                    if (cam.transform.position.x > GetRightRange())
                    {
                        cam.transform.position = new Vector3(GetRightRange(), cam.transform.position.y, cam.transform.position.z);
                    }
                    if (cam.transform.position.y > GetUpRange())
                    {
                        cam.transform.position = new Vector3(cam.transform.position.x, GetUpRange(), cam.transform.position.z);
                    }
                    if (cam.transform.position.y < GetDownRange())
                    {
                        cam.transform.position = new Vector3(cam.transform.position.x, GetDownRange(), cam.transform.position.z);
                    }

                }
            }
        }

    }
}