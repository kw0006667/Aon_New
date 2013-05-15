using UnityEngine;
using System.Collections;

public class MissionComplete_page7 : MonoBehaviour
{
    public GameObject[] TargetObjects;
    public GameObject AonBlink;
    public LayerMask Mask;

    private RaycastHit hit;
    private Ray ray;
    private int remainCount;
    private int currentTarget;
    public int[] record;

    // Use this for initialization
    void Start()
    {
        Random.seed = System.Guid.NewGuid().GetHashCode();
        foreach (var item in this.TargetObjects)
        {
            item.GetComponent<MeshRenderer>().enabled = true;
        }
        this.currentTarget = Random.Range(0, 3);
        this.TargetObjects[this.currentTarget].GetComponent<MeshRenderer>().enabled = true;
        this.record[this.currentTarget] = 1;
        this.remainCount = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.MetroPlayerARM)
        {
            this.ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
            if (Physics.Raycast(this.ray, out this.hit, 100, this.Mask) && Input.touches[0].phase == TouchPhase.Ended)
            {
                if (this.hit.collider.Equals(this.TargetObjects[this.currentTarget].collider))
                {
                    this.AonBlink.transform.position = new Vector3(this.ray.origin.x, this.ray.origin.y, this.AonBlink.transform.position.z);
                    if (this.remainCount > 0)
                    {
                        //this.TargetObjects[this.currentTarget].GetComponent<MeshRenderer>().enabled = false;
                        this.TargetObjects[this.currentTarget].GetComponent<Animation>().PlayAnimation(true, this.TargetObjects[this.currentTarget]);
                        if (this.remainCount > 0)
                        {
                            this.currentTarget = this.GetNextRand(this.currentTarget);
                            this.TargetObjects[this.currentTarget].GetComponent<MeshRenderer>().enabled = true;
                            this.record[this.currentTarget] = 1;
                            
                            this.remainCount--;
                            if (this.remainCount == 0)
                            {
                                this.TargetObjects[this.currentTarget].GetComponent<AonTrigger>().enabled = true;
                            }
                        }
                    }
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
            GUI.Label(new Rect(Screen.currentResolution.width / 2 - 200, Screen.currentResolution.height / 2 - 200, 400, 400), this.remainCount + "\n" + this.record[0] + " " + this.record[1] + " " + this.record[2] + "\n" + this.currentTarget + "\n" + Time.deltaTime);
            GUI.Label(new Rect(Screen.currentResolution.width / 2 - 200, Screen.currentResolution.height / 2 + 200, 400, 200), this.AonBlink.transform.position.x + " " + this.AonBlink.transform.position.y + " " + this.AonBlink.transform.position.z);
        }
    }


    int GetNextRand(int _ex)
    {
        int value = 0;
        while (true)
        {
            value = Random.Range(0, 3);
            if (value == _ex || this.record[value] == 1)
            {
                continue;
            }
            else
            {
                break;
            }
        }
        return value;
    }
}
