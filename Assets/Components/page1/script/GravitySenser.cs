using UnityEngine;
using System.Collections;

public class GravitySenser : MonoBehaviour
{
    public GameObject[] Objects;
    public float MovementScale;

    private Vector3 g;

    // Use this for initialization
    void Start()
    {
        Input.gyro.enabled = true;
        foreach (var obj in this.Objects)
        {
            //obj.rigidbody.mass /= 100;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Physics.gravity = Input.acceleration * 1.0f;
        var pos = this.transform.position;
        this.g = Physics.gravity;
        //pos.y = Vector3.Dot(this.g, Vector3.down) * this.MovementScale;
        //this.transform.position = pos;

        foreach (var item in this.Objects)
        {
            Vector3 vec3 = new Vector3(float.Parse(Input.gyro.userAcceleration.x.ToString("0")), float.Parse(Input.gyro.userAcceleration.y.ToString("0")), float.Parse(Input.gyro.userAcceleration.z.ToString("0")));
            item.rigidbody.AddForce(vec3 * 50.0f, ForceMode.Impulse);
        }
        print(g.x + ", " + g.y + ", " + g.z);
    }

    void OnGUI()
    {
        //string str = string.Format(g.x.ToString("0.00") + ", " + g.y.ToString("0.00") + ", " + g.z.ToString("0.00") + "\n");
        //string str2 = string.Format("Input.gyro.attribute.eulerAngles = ({0}, {1}, {2})\nInput.gyro.enabled = {3} \nInput.gyro.gravity = ({4}, {5}, {6})\nInput.gyro.rotationRate = ({7}, {8}, {9})\nInput.gyro.userAcceleration = ({10}, {11}, {12})\n",
        //    Input.gyro.attitude.eulerAngles.x.ToString("0.00"), Input.gyro.attitude.eulerAngles.y.ToString("0.00"), Input.gyro.attitude.eulerAngles.z.ToString("0.00"),
        //    Input.gyro.enabled.ToString(),
        //    Input.gyro.gravity.x.ToString("0.00"), Input.gyro.gravity.y.ToString("0.00"), Input.gyro.gravity.z.ToString("0.00"),
        //    Input.gyro.rotationRate.x.ToString("0.00"), Input.gyro.rotationRate.y.ToString("0.00"), Input.gyro.rotationRate.z.ToString("0.00"),
        //    Input.gyro.userAcceleration.x.ToString("0.00"), Input.gyro.userAcceleration.y.ToString("0.00"), Input.gyro.userAcceleration.z.ToString("0.00"));
        //GUI.Label(new Rect(Screen.currentResolution.width / 2 - 150, Screen.currentResolution.height / 2 - 200, 300, 400), str + str2);
    }
}
