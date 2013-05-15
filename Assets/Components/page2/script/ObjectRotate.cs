using UnityEngine;
using System.Collections;

public class ObjectRotate : MonoBehaviour
{
    public GameObject CentralObject;
    public float Speed;

    private Vector3 vec3;

    private float angle;

    // Use this for initialization
    void Start()
    {
        this.angle = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.MetroPlayerARM)
        {
            if (this.CentralObject != null)
            {
                float delta = Input.touches[0].deltaPosition.x;


                this.vec3 = this.CentralObject.transform.position;
                this.angle += delta * -this.Speed;
                if (this.angle <= 10 && this.angle >= -14)
                    this.transform.RotateAround(this.vec3, new Vector3(0.0f, 0.0f, this.vec3.z), delta * -this.Speed);
                else
                {
                    this.angle -= delta * -this.Speed;
                }
            }
        }
        else
        {
            Debug.Log("Current device is not mobile. (Windows 8 RT, Android or iOS)");
        }
        
    }

    void OnGUI()
    {
        //string str = string.Format("Input.touchCount = {0}\nDeltaPosition = {1} .", Input.touchCount, Input.GetTouch(0).deltaPosition);
        //string str2 = string.Format("Rotate = ({0},    {1},     {2})", this.transform.rotation.eulerAngles.x.ToString("f3"), this.transform.rotation.eulerAngles.y.ToString("f3"), this.transform.rotation.eulerAngles.z.ToString("f3"));
        //GUI.Label(new Rect(Screen.currentResolution.width / 2 - 150, Screen.currentResolution.height / 2 - 100, 500, 200), this.angle.ToString());
    }
}
