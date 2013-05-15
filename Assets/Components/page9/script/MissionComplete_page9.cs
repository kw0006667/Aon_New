using UnityEngine;
using System.Collections;

public class MissionComplete_page9 : MonoBehaviour
{  
	public GameObject TargetObject;
	public GameObject[] FishObjects;
	public GameObject UnaObject;
    public LayerMask Mask;
	
    private RaycastHit hit;
    private Ray ray;
	private int remainCount;
    public float[] record;
	
    // Use this for initialization
    void Start()
    {
		this.UnaObject.GetComponent<Animation>().PlayAnimation(true, this.UnaObject);
		this.FishObjects[0].GetComponent<Animation>().PlayAnimation(true, FishObjects[0]);
		this.FishObjects[1].GetComponent<Animation>().PlayAnimation(true, FishObjects[1]);
		this.FishObjects[2].GetComponent<Animation>().PlayAnimation(true, FishObjects[2]);
		this.FishObjects[3].GetComponent<Animation>().PlayAnimation(true, FishObjects[3]);
        Random.seed = System.Guid.NewGuid().GetHashCode();
        this.remainCount = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.MetroPlayerARM)
        {           
			if (Physics.Raycast(this.ray, out this.hit, 100, this.Mask) && Input.touches[0].phase == TouchPhase.Ended)
            {
				if (this.hit.collider.Equals(this.TargetObject.collider))
	            {
					this.TargetObject.GetComponent<Animation>().PlayAnimation(true, this.TargetObject);
					this.TargetObject.GetComponent<AonTrigger>().enabled = true;
				}
			}
            if (this.remainCount >= 0)
            {
				if(this.FishObjects[remainCount].transform.position.x >8.0f)
				{
					this.record[remainCount] = 0.0f;
					this.record[remainCount] = this.GetNextRand(this.record[remainCount]);
					this.FishObjects[remainCount].transform.position = new Vector3(-6.0f, this.record[remainCount], this.FishObjects[remainCount].transform.position.z);	
				}
				else
				{
					this.FishObjects[remainCount].transform.Translate(Time.deltaTime * (6.0f-4.0f*Mathf.Sin(this.GetNextRand(0.0f))), 0, 0);
				}
				this.remainCount--;
				if(this.remainCount < 0)this.remainCount = 3;
            }
			this.UnaObject.GetComponent<MeshRenderer>().enabled = true;
			this.FishObjects[0].GetComponent<MeshRenderer>().enabled = true;
			this.FishObjects[1].GetComponent<MeshRenderer>().enabled = true;
			this.FishObjects[2].GetComponent<MeshRenderer>().enabled = true;
			this.FishObjects[3].GetComponent<MeshRenderer>().enabled = true;
	     }
        else
        {
            Debug.Log("Current device is not mobile. (Windows 8 RT, Android or iOS)");
        }
    }

    void OnGUI()
    {
		
    }
	float GetNextRand(float _ex)
    {
        float value = 0.0f;
        while (true)
        {
            value = Random.Range(-1.0f, 3.0f);
            if (value == _ex)
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