using UnityEngine;
using System.Collections;

public class TouchHeart : MonoBehaviour
{
    public GameObject[] Holes;
    public LayerMask Mask;

    private RaycastHit hit;
    private Ray ray;

    private int randomNumber;
    private int count;

    // Use this for initialization
    void Start()
    {
        Random.seed = System.Guid.NewGuid().GetHashCode();
        this.randomNumber = Random.Range(0, this.Holes.Length - 1);
        GameObject tempObject = this.Holes[0];
        this.Holes[0] = this.Holes[this.randomNumber];
        this.Holes[this.randomNumber] = tempObject;

        this.count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.MetroPlayerARM)
        {
            this.ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
            if (Physics.Raycast(this.ray, out this.hit, 100, this.Mask) && Input.touches[0].phase.Equals(TouchPhase.Began))
            {
                if (this.hit.collider.Equals(this.collider) && this.count < this.Holes.Length)
                {
                    Vector3 pos = new Vector3(this.hit.point.x, this.hit.point.y, 12);
                    this.Holes[this.count].transform.position = pos;
                    this.count++;
                }
            }
        }
        else
        {
            Debug.Log("Current device is not mobile. (Windows 8 RT, Android or iOS)");
        }
    }
}
