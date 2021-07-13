using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class ScreenshotSaver : MonoBehaviour
    {
        [SerializeField] string filepath;
        [SerializeField] GameObject objectCaptured;
        public void saveButtonClicked()
        {
            string filename;
            filename = objectCaptured.name + ".png";
            Debug.Log("ScreenshotSaver : saveButtonClicked path " + filepath + filename );
            ScreenCapture.CaptureScreenshot(filepath + filename);
        }

        public void turnButtonClicked()
        {
            objectCaptured.transform.rotation = Quaternion.Euler(0, 60, 0) * objectCaptured.transform.rotation;
            Debug.Log("ScreenshotSaver : turnButtonClicked");
        }
    }
}
