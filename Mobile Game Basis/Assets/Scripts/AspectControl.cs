using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class AspectControl : MonoBehaviour
{
    Camera cam;

    public Vector2 tarPixelSize = new Vector2(2.8f, 5f);
    private Vector2 tarPixelSizeFinal; //after apply sizePercentage
    public float sizePercentage = 100;
    private float oSizePercentage = 1;//to force first update
    private float tarAspectRatio;

    public enum ScreenFittingType{FitHorizontal, FitVertical, FitInner, FitOuter}
    public ScreenFittingType screenFitType;

    Vector2 resolution;

    void Start() {
        cam = GetComponent<Camera> ();
        tarAspectRatio = tarPixelSize.x / tarPixelSize.y;
        resolution = new Vector2(Screen.width, Screen.height);
    }

    void Update() {
        //run code if the resolution changes
        if (resolution.x == Screen.width && resolution.y == Screen.height && sizePercentage == oSizePercentage) {return;}
        oSizePercentage = sizePercentage;
        resolution.x = Screen.width;
        resolution.y = Screen.height;

        tarPixelSizeFinal.x = tarPixelSize.x * sizePercentage/100;
        tarPixelSizeFinal.y = tarPixelSize.y * sizePercentage/100;
        
        switch (screenFitType) {
            case ScreenFittingType.FitHorizontal://width of target size remain constant
                cam.orthographicSize = tarPixelSizeFinal.x / cam.aspect;
            break;
            case ScreenFittingType.FitVertical://height of target size remain constant
                cam.orthographicSize = tarPixelSizeFinal.y;
            break;
            case ScreenFittingType.FitInner:
                //if the aspect is thinner than target, fit vertical, else fit horizontal
                cam.orthographicSize = (cam.aspect < tarAspectRatio)? tarPixelSizeFinal.y : tarPixelSizeFinal.x / cam.aspect;
            break;
            case ScreenFittingType.FitOuter:
                //if the aspect is wider than target, fit vertical, else fit horizontal
                cam.orthographicSize = (cam.aspect > tarAspectRatio)? tarPixelSizeFinal.y : tarPixelSizeFinal.x / cam.aspect;
            break;
        }
    }
}
