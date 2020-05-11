using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
public class ColorPickerWheel : MonoBehaviour
{
    public GameObject colorCube;
    private float hue, saturation, value = 1f;

    private GameObject blackWheel;

    static public ColorPickerWheel S;
    void Start()
    {
        S = this;
         if(GetComponent<VRTK_ControllerEvents>() == null)
        {
            Debug.LogError("Need VRTK_ControllerEvents component");
            return;
        }
        //colorCube = transform.Find("Model/tip/attach/ArrowClone/Arrow/ArrowObject/Tip").gameObject;
        blackWheel = transform.Find("CanvasHolder/Canvas/BlackWheel").gameObject;


        GetComponent<VRTK_ControllerEvents>().TouchpadAxisChanged += new ControllerInteractionEventHandler(DoTouchPadAxisChanged);
    }

    private void DoTouchPadAxisChanged(object sender, ControllerInteractionEventArgs e)
    {
        ChangedHueSaturation(e.touchpadAxis, e.touchpadAngle);
    }

    private void ChangedHueSaturation(Vector2 touchpadAxis, float touchpadAngle)
    {
        float normalAngle = touchpadAngle - 90;
        if(normalAngle < 0)
        {
            normalAngle = 360 + normalAngle;
        }
        //Debug.Log("ChangedHueSaturation: axis " + touchpadAxis + " angle: " + normalAngle);

        float rads = normalAngle * Mathf.PI / 180;
        float maxX = Mathf.Cos(rads);
        float maxY = Mathf.Sin(rads);

        float currX = touchpadAxis.x;
        float currY = touchpadAxis.y;

        float percentX = Mathf.Abs(currX / maxX);
        float percentY = Mathf.Abs(currY / maxY);

        this.hue = normalAngle / 360.0f;
        this.saturation = (percentX + percentY) / 2;
        //Debug.Log(this.hue + " " + this.saturation);

        UpdateColor();
    }

    private void UpdateColor()
    {
        Color color = Color.HSVToRGB(this.hue, this.saturation, this.value);
        colorCube.GetComponent<Renderer>().material.color = color;
    }
    void Update()
    {
        
    }
}
