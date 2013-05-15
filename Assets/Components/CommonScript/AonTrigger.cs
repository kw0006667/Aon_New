using UnityEngine;
using System.Collections;

public class AonTrigger : MonoBehaviour
{
    public GameObject AonBlink;
    public GameObject AonFound;
    public Vector3 TargetArea = new Vector3(4.554856f, 3.295128f, 12);
    public LayerMask Mask;

    private RaycastHit hit;
    private Ray ray;

    private TextureAnimation aonBlinkTextureAnimation;
    private TextureAnimation aonShineTextureAnimation;
    private bool isMoving;
    private float offestValueY;

    // Use this for initialization
    void Start()
    {
        this.isMoving = false;
        this.aonShineTextureAnimation = this.AonFound.GetComponent<TextureAnimation>();
        this.offestValueY = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.MetroPlayerARM)
        {
            this.ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
            if (Physics.Raycast(this.ray, out this.hit, 100, this.Mask) && Input.touches[0].phase == TouchPhase.Ended)
            {
                if (this.hit.collider.Equals(this.collider))
                {
                    this.AonBlink.GetComponent<MeshRenderer>().enabled = true;
                    this.aonBlinkTextureAnimation = this.AonBlink.GetComponent<TextureAnimation>();
                    this.aonBlinkTextureAnimation.PlayAnimation(true, this);
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
        if (this.isMoving)
        {
            this.AonBlink.transform.position = Vector3.MoveTowards(this.AonBlink.transform.position + new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), 0) * Time.deltaTime * 10, this.TargetArea, Time.deltaTime * 5);
            if (this.AonBlink.transform.position.Equals(this.TargetArea))
            {
                this.isMoving = false;
                this.aonShineTextureAnimation.PlayAnimation(true, this);
                this.enabled = false;
            }
        }
    }

    public void MovingTrigger()
    {
        this.isMoving = true;
        this.GetComponent<MeshRenderer>().enabled = false;
    }
}
