using UnityEngine;
using System.Collections;

public class TouchMoveScaleBroken : MonoBehaviour
{
    public GameObject Brokens;
    public GameObject Aon;
    public Vector3 AonLimitPosition;
    public LayerMask Mask;

    // 是否只是只能向上，否則只能向下
    public bool isTop;

    private RaycastHit hit;
    private Ray ray;

    private Collider currentTouchesCollider;

    private Vector3 originalPosition;

    private float offestAonValue;
    private Vector3 originalScaleBrokens;

    private float scaleValue;

    // Use this for initialization
    void Start()
    {
        this.originalPosition = this.transform.position;
        if (this.Brokens != null)
        {
            this.originalScaleBrokens = this.Brokens.transform.localScale;
        }
        this.currentTouchesCollider = null;
        this.scaleValue = 0.8f;

        if (this.Brokens != null && this.Aon != null)
        {
            Color color = this.Brokens.renderer.material.GetColor("_Color");
            this.Brokens.renderer.material.SetColor("_Color", new Color(color.r, color.g, color.b, 0));
            color = this.Aon.renderer.material.GetColor("_Color");
            this.Aon.renderer.material.SetColor("_Color", new Color(color.r, color.g, color.b, 0));
        }
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
            else if (Physics.Raycast(this.ray, out this.hit, 100, this.Mask) && Input.touches[0].phase == TouchPhase.Moved)
            {
                if (this.currentTouchesCollider)
                {
                    float offest = Input.touches[0].deltaPosition.y;
                    Vector3 pos = this.currentTouchesCollider.gameObject.transform.position;

                    if (this.isTop)
                    {
                        this.currentTouchesCollider.gameObject.transform.position = new Vector3(pos.x, this.ray.origin.y, pos.z);
                        if (this.currentTouchesCollider.gameObject.transform.position.y <= this.originalPosition.y)
                        {
                            this.currentTouchesCollider.gameObject.transform.position = this.originalPosition;
                        }
                        else if (this.currentTouchesCollider.gameObject.transform.position.y <= this.AonLimitPosition.y)
                        {
                            this.currentTouchesCollider.gameObject.transform.position = this.AonLimitPosition;
                        }

                        if (this.Brokens != null && this.Aon != null)
                        {
                            this.offestAonValue = this.originalPosition.y - pos.y;
                            float percent = this.offestAonValue / (this.originalPosition.y - this.AonLimitPosition.y);
                            //float addValue = 0;
                            //if (offest > 0)
                            //    addValue = 0.01f;
                            //else if (offest < 0)
                            //    addValue = -0.01f;

                            //this.Brokens.transform.localScale = new Vector3(this.Brokens.transform.localScale.x, this.Brokens.transform.localScale.y, this.Brokens.transform.localScale.z);

                            //this.Brokens.transform.localScale = new Vector3(this.originalScaleBrokens.x + this.offestAonValue * this.scaleValue, this.originalScaleBrokens.y + this.offestAonValue * this.scaleValue, this.originalScaleBrokens.z + this.offestAonValue * this.scaleValue * 0.56f);
                            Color color = this.Brokens.renderer.material.GetColor("_Color");
                            this.Brokens.renderer.material.SetColor("_Color", new Color(color.r, color.g, color.b, percent));
                            color = this.Aon.renderer.material.GetColor("_Color");
                            this.Aon.renderer.material.SetColor("_Color", new Color(color.r, color.g, color.b, percent));
                        }
                    }
                    else
                    {
                        this.currentTouchesCollider.gameObject.transform.position = new Vector3(pos.x, this.ray.origin.y, pos.z);

                        if (this.currentTouchesCollider.gameObject.transform.position.y >= this.originalPosition.y)
                        {
                            this.currentTouchesCollider.gameObject.transform.position = this.originalPosition;
                        }
                        else if (this.currentTouchesCollider.gameObject.transform.position.y <= this.AonLimitPosition.y)
                        {
                            this.currentTouchesCollider.gameObject.transform.position = this.AonLimitPosition;
                        }

                        if (this.Brokens != null && this.Aon != null)
                        {
                            this.offestAonValue = this.originalPosition.y - pos.y;
                            float percent = this.offestAonValue / (this.originalPosition.y - this.AonLimitPosition.y);
                            //float addValue = 0;
                            //if (offest > 0)
                            //    addValue = 0.01f;
                            //else if (offest < 0)
                            //    addValue = -0.01f;

                            //this.Brokens.transform.localScale = new Vector3(this.Brokens.transform.localScale.x, this.Brokens.transform.localScale.y, this.Brokens.transform.localScale.z);
                            //this.Brokens.transform.localScale = new Vector3(this.originalScaleBrokens.x + this.offestAonValue * this.scaleValue, this.originalScaleBrokens.y + this.offestAonValue * this.scaleValue, this.originalScaleBrokens.z + this.offestAonValue * this.scaleValue * 0.56f);
                            Color color = this.Brokens.renderer.material.GetColor("_Color");
                            this.Brokens.renderer.material.SetColor("_Color", new Color(color.r, color.g, color.b, percent));
                            color = this.Aon.renderer.material.GetColor("_Color");
                            this.Aon.renderer.material.SetColor("_Color", new Color(color.r, color.g, color.b, percent));
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
            Debug.Log(this.Brokens.renderer.material.GetColor("_Color"));
        }
    }

    void OnGUI()
    {

    }
}
