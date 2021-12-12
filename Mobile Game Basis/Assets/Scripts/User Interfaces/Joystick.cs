using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Joystick : MonoBehaviour
{
    [Range(10,1000)]
    public float joystickRangeSizePercent = 100f;//determine where is the full input from RectTransform.width
    public Vector2 output = new Vector2(0,0);
    [Space(10)]
    public RectTransform rectTransform;
    public Image backImage;
    public Image frontImage;
    public Canvas canvas;
    [Space(10)]
    
    //Debugging
    public Text text1;

    void Awake() {
        //rectTransform = GetComponent<RectTransform>();
    }

    void Update() {
        if (Input.touchSupported) {
            if (Input.touchCount > 0) {
                EnableImage(true);
                //when start to touch the screen, move the image to touch location
                if (Input.GetTouch(0).phase == TouchPhase.Began) {
                    //assign new position to where finger was pressed
                    transform.position = new Vector3 (Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, transform.position.z);
                //after first touch
                } else if (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(0).phase == TouchPhase.Stationary) {
                    //frind angle using delta of current position and first touch position
                    Vector2 deltaPosition = new Vector2 (Input.GetTouch(0).position.x - transform.position.x, Input.GetTouch(0).position.y - transform.position.y);
                    float angle = Mathf.Atan2(deltaPosition.y, deltaPosition.x);
                    
                    //calculate distance of front joystick and move
                    float fullWidth = joystickRangeSizePercent/100 * rectTransform.sizeDelta.x/2;//Width at full value
                    float deltaDist = Mathf.Sqrt(Vector2.SqrMagnitude(deltaPosition));
                    deltaDist = Mathf.Clamp(deltaDist, 0, fullWidth);
                    output.x = deltaDist * Mathf.Cos(angle);
                    output.y = deltaDist * Mathf.Sin(angle);
                    frontImage.transform.localPosition = new Vector3(output.x, output.y, frontImage.transform.localPosition.z);
                    //fraction the output. from [0,fullWidth] to [0,1]
                    output.x /= fullWidth;
                    output.y /= fullWidth;

                    //text1.text = "" + Mathf.Round(output.x * 100f)/100f + " " + Mathf.Round(output.y * 100f)/100f;
                }
            } else {
                EnableImage(false);
                output = Vector2.zero;
            }
        } else {
            EnableImage(Input.GetMouseButton(0));
            if (Input.GetMouseButton(0)) {
                if (Input.GetMouseButtonDown(0)) {
                    transform.position = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, transform.position.z);
                } else {
                    //Find angle using delta position
                    Vector2 deltaPosition = new Vector2 ((Input.mousePosition.x - transform.position.x) / canvas.scaleFactor, (Input.mousePosition.y - transform.position.y) / canvas.scaleFactor);
                    float angle = Mathf.Atan2(deltaPosition.y, deltaPosition.x);

                    //calculate distance of front joystick and move
                    float fullWidth = joystickRangeSizePercent/100 * rectTransform.sizeDelta.x/2;//Width at full value
                    float deltaDist = Mathf.Sqrt(Vector2.SqrMagnitude(deltaPosition));
                    deltaDist = Mathf.Clamp(deltaDist, 0, fullWidth);
                    output.x = deltaDist * Mathf.Cos(angle);
                    output.y = deltaDist * Mathf.Sin(angle);
                    frontImage.transform.localPosition = new Vector3(output.x, output.y, frontImage.transform.localPosition.z);
                    //fraction the output. from [0,fullWidth] to [0,1]
                    output.x /= fullWidth;
                    output.y /= fullWidth;

                    text1.text = "" + canvas.scaleFactor;
                    //text1.text = "" + Mathf.Round(output.x * 100f)/100f + " " + Mathf.Round(output.y * 100f)/100f;
                }
            } else {
                output = Vector2.zero;
            }
        }
    }

    void EnableImage(bool value) {
        if (backImage != null) backImage.enabled = value;
        if (frontImage != null) frontImage.enabled = value;
    }
}
