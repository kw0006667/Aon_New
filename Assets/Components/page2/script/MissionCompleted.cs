using UnityEngine;
using System.Collections;

public class MissionCompleted : MonoBehaviour
{
    public GameObject TargetObject;
    public Rect[] TargetArea;
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
            this.ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            if (Physics.Raycast(this.ray, out this.hit, 100, this.Mask) && Input.touches[0].phase == TouchPhase.Ended)
            {

                if (this.hit.collider.Equals(this.TargetObject.collider) && this.touchInArea(this.hit))
                {
                    Application.LoadLevel("page3");
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
            string str = string.Format("Input.GetTouche(0).position = ({0}, {1}).", this.ray.GetPoint(100).x, this.ray.GetPoint(100).y);
            GUI.Label(new Rect(50, 50, 200, 100), str);
        }

    }

    private bool touchInArea(RaycastHit hit)
    {
        foreach (var area in this.TargetArea)
        {
            if (area.Contains(hit.point))
                return true;
        }
        return false;
    }
}
