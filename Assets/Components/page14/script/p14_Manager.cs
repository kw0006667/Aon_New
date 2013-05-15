using UnityEngine;
using System.Collections;

public class p14_Manager : MonoBehaviour
{
    public GameObject Aon_Blink;
    public LayerMask Mask;

    private RaycastHit hit;
    private Ray ray;

    private Transform blink_TR;

    // Use this for initialization
    void Start()
    {
        this.blink_TR = this.Aon_Blink.transform;   
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.MetroPlayerARM)
        {
            this.ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
            if (Physics.Raycast(this.ray, out this.hit, 100, this.Mask) && Input.touches[0].phase == TouchPhase.Ended)
            {
                this.Aon_Blink.transform.position = new Vector3(this.ray.origin.x, this.ray.origin.y, this.blink_TR.position.z);
            }
        }
        else
        {
            Debug.Log("Current device is not mobile. (Windows 8 RT, Android or iOS)");
        }
    }
}
