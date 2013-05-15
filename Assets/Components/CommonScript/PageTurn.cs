using UnityEngine;
using System.Collections;

public class PageTurn : MonoBehaviour
{
    public enum SceneName
    {
        page0 = 0,
        page1 = 1,
        page2,
        page3,
        page4,
        page5,
        page7,
        page8,
        page9,
        page11,
        page14,
        page15,
        page17,
        page18,
        page19,
        page20,
        page21,
        page24,
        page27
    }

    public SceneName TurnToPage;

    private Collider touchCollider;

    public LayerMask mask;
    private RaycastHit hit;
    private Ray ray;

    // Use this for initialization
    void Start()
    {

        this.touchCollider = null;
        //this.mask = 0 + 1 + 2 + 4;
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.MetroPlayerARM)
        {
            this.ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
            if (Physics.Raycast(this.ray, out this.hit, 100, this.mask) && Input.touches[0].phase == TouchPhase.Ended)
            {
                if (this.hit.collider.Equals(this.collider))
                {
                    Application.LoadLevel(PageTurn.GetLevelName(this.TurnToPage));
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
            //GUI.Label(new Rect(Screen.currentResolution.width / 2 - 200, Screen.currentResolution.height / 2 - 50, 200, 50), "Current touchCollider = " + this.hit.collider.name + "\n this.collder =" + this.collider.name);
            //Debug.Log("Current touchCollider = " + this.hit.collider.name);
        }
    }

    public static string GetLevelName(SceneName _name)
    {
        string name = string.Empty;
        switch (_name)
        {
            case SceneName.page0:
                name = "page0";
                break;
            case SceneName.page1:
                name = "page1";
                break;
            case SceneName.page2:
                name = "page2";
                break;
            case SceneName.page3:
                name = "page3";
                break;
            case SceneName.page4:
                name = "page4";
                break;
            case SceneName.page5:
                name = "page5";
                break;
            case SceneName.page7:
                name = "page7";
                break;
            case SceneName.page8:
                name = "page8";
                break;
            case SceneName.page9:
                name = "page9";
                break;
            case SceneName.page11:
                name = "page11";
                break;
            case SceneName.page14:
                name = "page14";
                break;
            case SceneName.page15:
                name = "page15";
                break;
            case SceneName.page17:
                name = "page17";
                break;
            case SceneName.page18:
                name = "page18";
                break;
            case SceneName.page19:
                name = "page19";
                break;
            case SceneName.page20:
                name = "page20";
                break;
            case SceneName.page21:
                name = "page21";
                break;
            case SceneName.page24:
                name = "page24";
                break;
            case SceneName.page27:
                name = "page27";
                break;
            default:
                break;
        }
        return name;
    }
}
