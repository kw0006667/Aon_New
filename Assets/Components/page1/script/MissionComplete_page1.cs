using UnityEngine;
using System.Collections;

public class MissionComplete_page1 : MonoBehaviour
{
    public GameObject TargetObject;
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
            this.ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
            if (Physics.Raycast(this.ray, out this.hit, 100, this.Mask) && Input.touches[0].phase == TouchPhase.Ended)
            {
                if (this.hit.collider.Equals(this.TargetObject.collider))
                {
                    Application.LoadLevel(2);
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
        if (Debug.isDebugBuild)
        {
            GUI.Label(new Rect(Screen.currentResolution.width / 2 - 200, Screen.currentResolution.height / 2 - 50, 200, 50), "Current touchCollider = " + this.hit.collider.name);
            Debug.Log("Current touchCollider = " + this.hit.collider.name);
        }
        
    }
}
