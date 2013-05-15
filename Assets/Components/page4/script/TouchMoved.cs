using UnityEngine;
using System.Collections;

public class TouchMoved : MonoBehaviour
{

    public LayerMask Mask;
    public bool isLeft;

    private RaycastHit hit;
    private Ray ray;

    private Collider currentTouchesCollider;

    private Vector3 originalPosition;

    // Use this for initialization
    void Start()
    {
        this.originalPosition = this.transform.position;
        this.currentTouchesCollider = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.MetroPlayerARM)
        {
            this.ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            if (Physics.Raycast(this.ray, out this.hit, 100, this.Mask) && Input.touches[0].phase == TouchPhase.Began)
            {
                if (this.hit.collider.Equals(this.collider))
                {
                    this.currentTouchesCollider = this.hit.collider;
                }
            }
            else if (Physics.Raycast(this.ray, out this.hit, 100, this.Mask) && Input.touches[0].phase == TouchPhase.Moved && this.currentTouchesCollider != null)
            {
                if (this.currentTouchesCollider)
                {
                    float offest = Input.touches[0].deltaPosition.x;
                    this.currentTouchesCollider.gameObject.transform.position += new Vector3(Input.touches[0].deltaPosition.x * 0.05f, 0, 0);
                    if (this.isLeft)
                    {
                        if (this.currentTouchesCollider.gameObject.transform.position.x >= this.originalPosition.x)
                        {
                            this.currentTouchesCollider.gameObject.transform.position = this.originalPosition;
                        }
                    }
                    else
                    {
                        if (this.currentTouchesCollider.gameObject.transform.position.x <= this.originalPosition.x)
                        {
                            this.currentTouchesCollider.gameObject.transform.position = this.originalPosition;
                        }
                    }
                }
            }
            else if (Input.touches[0].phase == TouchPhase.Ended)
            {
                this.currentTouchesCollider = null;
            }
        }
        else
        {
            Debug.Log("Current device is not mobile. (Windows 8 RT, Android or iOS)");
        }
    }

    void OnGUI()
    {

    }
}
