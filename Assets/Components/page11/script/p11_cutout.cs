using UnityEngine;
using System.Collections;

public class p11_cutout : MonoBehaviour
{
    public float Speed;
    public LayerMask Mask;


    private Ray ray;
    private RaycastHit hit;

    private float pressValue;

    private bool isPress;
    private bool isLock;

    // Use this for initialization
    void Start()
    {
        this.pressValue = 0.01f;
        this.isPress = false;
        this.isLock = false;

        this.renderer.material.SetFloat("_Cutoff", this.pressValue);
    }

    // Update is called once per frame
    void Update()
    {

        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.MetroPlayerARM)
        {
            this.ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
            if (Physics.Raycast(this.ray, out this.hit, 100, this.Mask) && Input.touches[0].phase == TouchPhase.Began)
            {
                this.isPress = true;
            }
            else if (Physics.Raycast(this.ray, out this.hit, 100, this.Mask) && Input.touches[0].phase == TouchPhase.Stationary)
            {
                print(this.renderer.material.GetFloat("_Cutoff"));
                this.renderer.material.SetFloat("_Cutoff", this.pressValue);
                if (this.pressValue < 0.9f)
                {
                    this.pressValue += Time.deltaTime * 0.3f;
                }

                if (this.pressValue >= 0.9f)
                {
                    this.isLock = true;
                }

            }
            else if (Physics.Raycast(this.ray, out this.hit, 100, this.Mask) && Input.touches[0].phase == TouchPhase.Ended)
            {
                this.isPress = false;
                if (this.isLock)
                {
                    this.GetComponent<AonTrigger>().enabled = true;
                }
            }


        }
        else
        {
            Debug.Log("Current device is not mobile. (Windows 8 RT, Android or iOS)");
        }


    }

    void FixedUpdate()
    {
        if (!this.isPress && !this.isLock)
        {
            if (this.pressValue > 0.01f)
            {
                this.renderer.material.SetFloat("_Cutoff", this.pressValue);

                this.pressValue -= Time.deltaTime * 0.3f;
            }

        }
    }
}
