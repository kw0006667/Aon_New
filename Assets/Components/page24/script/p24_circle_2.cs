using UnityEngine;
using System.Collections;

public class p24_circle_2 : MonoBehaviour {
    public float m_fdegree;
    public float m_fspeed;
	// Use this for initialization
	void Start () {
        m_fspeed = Random.RandomRange(3.0f, 5.0f);
	
	}
	
	// Update is called once per frame
	void Update () {
        m_fdegree = Time.deltaTime;
        transform.Rotate(0.0f, m_fdegree * m_fspeed, 0.0f);	
	}
}
