using UnityEngine;
using System.Collections;

public class p24_CloudRotation : MonoBehaviour {
    public float m_fDdegree;
    public float m_fSpeed;
    public Vector2 tp;
    public bool m_bAutoRotating;
    public float m_fRotTime;
    public void Stop()
    {
        m_bAutoRotating = false;
        m_fRotTime = 0;
    }
    public void AutoRotating(float fSpeed)
    { // fSpeed:degree/sec
        m_fSpeed = fSpeed;
        m_bAutoRotating = true;
    }
    public void AutoRotatingDiff(float fSpeed)
    { // fSpeed:degree/sec
        float t = Random.Range(0.75f, 1.0f);
        m_fSpeed = fSpeed*t;
        m_bAutoRotating = true;
    }

    public void RotateWithDegree(float fdeg)
    {
        transform.Rotate(0.0f, fdeg, 0.0f);
    }

    public void RotateWithDiff(float fdeg)
    {
        float t = Random.Range(0.25f, 2.5f);
        transform.Rotate(0.0f, fdeg * t, 0.0f);
    }

	// Use this for initialization
	void Start () {
        m_bAutoRotating = false;
        m_fRotTime = 0;		
	}
	
	// Update is called once per frame
	void Update () {
        if (m_bAutoRotating)
        {
            float dt = Time.deltaTime;
            transform.Rotate(0.0f, 2.5f * m_fSpeed, 0.0f); // 固定速度 * 2.5度
            float t = Mathf.Pow(m_fRotTime, 1.15f);
            if (m_fSpeed > 0) {
                m_fSpeed = m_fSpeed - 0.0625f * t;
                if (m_fSpeed < 0) m_fSpeed = 0;
            }
            else {
                m_fSpeed = m_fSpeed + 0.0625f * t;
                if (m_fSpeed > 0) m_fSpeed = 0;
            }
            m_fRotTime += dt;
            if (Mathf.Abs(m_fSpeed) < 0.05f)
            { // 讓自動旋轉停止
                m_bAutoRotating = false;
                m_fRotTime = 0;
            }
        }	
	}
    void OnGUI()
    {
        //GUI.Label(new Rect(Screen.currentResolution.width / 2 + 150, Screen.currentResolution.height / 2 - 300, 200, 50), m_fSpeed.ToString());
        //GUI.Label(new Rect(Screen.currentResolution.width / 2 + 150, Screen.currentResolution.height / 2 -250, 200, 50), m_PrevPos.ToString());
        //GUI.Label(new Rect(Screen.currentResolution.width / 2 + 150, Screen.currentResolution.height / 2 - 200, 200, 50), m_EndPos.ToString());
    }
}
