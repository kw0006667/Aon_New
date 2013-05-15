using UnityEngine;
using System.Collections;

public class MissionComplete_Page5 : MonoBehaviour
{
    public GameObject TargetObject;
    public GameObject ReplaceObject;
    public LayerMask Maks;

    private RaycastHit hit;
    private Ray ray;

    private Collider col;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.MetroPlayerARM)
        {
            //this.ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
            //if (Physics.Raycast(this.ray, out this.hit, 100, this.Maks) && Input.touches[0].phase.Equals(TouchPhase.Began))
            //{
            //    this.col = this.hit.collider;
            //}
            //if (Physics.Raycast(this.ray, out this.hit, 100, this.Maks) && Input.touches[0].phase.Equals(TouchPhase.Ended))
            //{
            //    if (this.TargetObject)
            //    {
            //        if (this.hit.collider.Equals(this.TargetObject.collider) && this.TargetObject.GetComponent<MeshRenderer>().enabled == true)
            //            Application.LoadLevel("page7");
            //    }

            //}

            if (this.TargetObject.GetComponent<MeshRenderer>().enabled)
            {
                this.ReplaceObject.GetComponent<MeshRenderer>().enabled = true;
                this.TargetObject.GetComponent<AonTrigger>().enabled = true;
            }
        }
        else
        {
            Debug.Log("Current device is not mobile. (Windows 8 RT, Android or iOS)");
        }
    }
}
