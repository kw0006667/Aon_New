using UnityEngine;
using System.Collections;

public class p14_Moving_horse : MonoBehaviour {

    public Vector3 m_Origin;
    public float m_fElapsedTime;
    public float m_fOffset;
    public float m_fMovingTime; // 完成上下位移的 也就是 360 度的時間長度
    public Vector3 m_NewPos;
    // Use this for initialization
    void Reset()
    {
        m_fOffset = Random.Range(0.2f, 0.35f); // 上下最大的位移量
        m_fMovingTime = Random.Range(5.0f, 8.0f); // 上下位移的總共時間
        m_fElapsedTime = 0;
    }

    void Start()
    {
        m_Origin = transform.position;
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;
        if (m_fElapsedTime <= m_fMovingTime)
        {
            float t = Mathf.Sin(2.0f * Mathf.PI * m_fElapsedTime / m_fMovingTime);
            m_NewPos = m_Origin;
            m_NewPos.y = m_Origin.y + t * m_fOffset;
            transform.position = m_NewPos;
            m_fElapsedTime += dt;
        }
        else Reset();
    }
}
