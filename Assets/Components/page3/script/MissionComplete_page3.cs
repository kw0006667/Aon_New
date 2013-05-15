using UnityEngine;
using System.Collections;

public class MissionComplete_page3 : MonoBehaviour
{
    public GameObject TaretObject;
    public LayerMask Mask;

    private RaycastHit hit;
    private Ray ray;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.MetroPlayerARM)
        {
            if (this.TaretObject.renderer.material.GetColor("_Color").a >= 0.7f)
            {
                this.TaretObject.GetComponent<AonTrigger>().enabled = true;
            }
            else
            {
                this.TaretObject.GetComponent<AonTrigger>().enabled = false;
            }
        }
        else
        {
            Debug.Log("Current device is not mobile. (Windows 8 RT, Android or iOS)");
        }
    }

    void OnGUI()
    {
        if (Debug.isDebugBuild)
        {
            GUI.Label(new Rect(Screen.currentResolution.width / 2 - 100, Screen.currentResolution.height / 2 - 50, 200, 100), this.TaretObject.renderer.material.GetColor("_Color").a.ToString());
        }
    }
}
