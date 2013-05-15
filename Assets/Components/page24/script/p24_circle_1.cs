using UnityEngine;
using System.Collections;

public class p24_circle_1 : MonoBehaviour {
    public float m_fdegree;
    public float m_fspeed;
    public Vector2 tp;
	// Use this for initialization
	void Start () {
        m_fspeed = Random.RandomRange(2.0f, 4.0f);	
	}
	
	// Update is called once per frame
	void Update () {
        float tmp;
        if (Input.touchCount > 0 &&  Input.GetTouch(0).phase == TouchPhase.Moved) {
        
            // Get movement of the finger since last frame
            tp = Input.GetTouch(0).position;
            tmp = tp.x * tp.x + tp.y * tp.y;
            if (tmp >= 12)
            {
                tp = Input.GetTouch(0).deltaPosition;
                transform.Rotate(0, -tp.x * m_fspeed, 0);
            }
        }

        /*
        tp = Input.GetTouch(0).position;
        m_fdegree = Time.deltaTime;
        transform.Rotate(0.0f, m_fdegree * m_fspeed, 0.0f);*/
	}
}
