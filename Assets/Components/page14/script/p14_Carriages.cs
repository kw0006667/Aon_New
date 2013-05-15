using UnityEngine;
using System.Collections;

public class p14_Carriages : MonoBehaviour {
    public float m_fdegree;

public void SetDegree(float deg) {
        m_fdegree = deg;
        transform.Rotate(0, -m_fdegree, 0);
    }
 	// Use this for initialization
	void Start () {
        m_fdegree = 0; // 預設為0度
	}
	
	// Update is called once per frame
	void Update () {
        
	}
}
