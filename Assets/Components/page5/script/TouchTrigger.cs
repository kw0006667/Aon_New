using UnityEngine;
using System.Collections;

public class TouchTrigger : MonoBehaviour
{
    public GameObject[] TriggerObjects;
    public LayerMask Mask;

    private RaycastHit hit;
    private Ray ray;

    // Use this for initialization
    void Start()
    {
        foreach (var item in this.TriggerObjects)
        {
            item.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.MetroPlayerARM)
        {
            this.ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
            if (Physics.Raycast(this.ray, out this.hit, 100, this.Mask) && Input.touches[0].phase == TouchPhase.Ended)
            {
                foreach (var item in this.TriggerObjects)
                {
                    if (this.hit.collider.Equals(item.collider))
                    {
                        item.renderer.GetComponent<MeshRenderer>().enabled = true;
                    }
                }
            }
        }
        else
        {
            Debug.Log("Current device is not mobile. (Windows 8 RT, Android or iOS)");
        }
    }
}
