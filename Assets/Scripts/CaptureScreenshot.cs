using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureScreenshot : MonoBehaviour
{
    [SerializeField] private int superSize = 2;
    private int screenshotIndex = 0;
    public void Capture()
    {
        ScreenCapture.CaptureScreenshot($"Screenshot{screenshotIndex}.png", superSize);
        screenshotIndex++;
    }
}
