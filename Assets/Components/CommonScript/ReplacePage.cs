using UnityEngine;
using System.Collections;

public class ReplacePage : MonoBehaviour
{
    public Object PrevPage;
    public Object NextPage;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (this.PrevPage != null)
        {
            if (Input.touchCount >= 2)
            {
                if (Input.GetTouch(0).deltaPosition.x > 10 && Input.GetTouch(1).deltaPosition.x > 10)
                {
                    Application.LoadLevel(this.PrevPage.name);
                }
            }
        }
        if (this.NextPage != null)
        {
            if (Input.touchCount >= 2)
            {
                if (Input.GetTouch(0).deltaPosition.x > -10 && Input.GetTouch(1).deltaPosition.x > -10)
                {
                    Application.LoadLevel(this.NextPage.name);
                }
            }
        }
    }
}
