using UnityEngine;
using System.Collections;

public class BrokenMask : MonoBehaviour
{
    public Texture Brokens;
    public Texture Frontend;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnGUI()
    {
        GUI.BeginGroup(new Rect(0, 0, Screen.currentResolution.width, Screen.currentResolution.height));
        {
            GUI.DrawTexture(new Rect(0, 0, Screen.currentResolution.width, Screen.currentResolution.height), this.Brokens);

            GUI.BeginGroup(new Rect(0, 0, Screen.currentResolution.width, Screen.currentResolution.height));
            {
                GUI.DrawTexture(new Rect(0, 0, Screen.currentResolution.width, Screen.currentResolution.height), this.Frontend);
            }
        }
        GUI.EndGroup();
    }
}
