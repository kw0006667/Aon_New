using UnityEngine;
using System.Collections;

public class MissionComplete_Page4 : MonoBehaviour
{
    public GameObject TargetObject;
    public LayerMask Mask;

    private RaycastHit hit;
    private Ray ray;

    private string colliderCurrent;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (this.TargetObject)
        {
            this.ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
            if (Physics.Raycast(this.ray, out this.hit, 100, Mask))
            {
                this.colliderCurrent = this.hit.collider.name;
                if (this.hit.collider.Equals(this.TargetObject.collider))
                    Application.LoadLevel("page5");
            }
        }
    }

    void OnGUI()
    {

    }
}
