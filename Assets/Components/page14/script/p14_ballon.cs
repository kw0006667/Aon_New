using UnityEngine;
using System.Collections;

public class p14_ballon : MonoBehaviour {
    public Vector3 m_Origin;
    public Vector2 m_Direction;
    public float m_fSpeed; // 上飄的速度
    public float m_fElapsedTime;
    public float m_fOffset;
    public float m_fMovingTime; // 完成上下位移的 也就是 360 度的時間長度
    public Vector3 m_NewPos;
    public Vector2 m_fNewDir; // 完成上下位移的 也就是 360 度的時間長度

	// Use this for initialization
    void Reset() {
        m_Origin = new Vector3(Random.Range(-4.5f, 4.5f), Random.Range(-2.2f, -2.5f), 15);
        m_fSpeed = Random.Range(0.5f, 1.5f);
        transform.position = m_Origin;
        m_Direction.x = 0.0f; m_Direction.y = 1.0f;
    }

	void Start () {
        Reset();
	}
	
	// Update is called once per frame
	void Update () {
        float dt = Time.deltaTime;
        float deg = Random.Range(30.0f, 150.0f);
        m_fNewDir.x = Mathf.Cos(Mathf.PI * deg / 180.0f);
        m_fNewDir.y = Mathf.Sin(Mathf.PI * deg /180.0f);
        m_Direction = m_Direction * 0.85f + m_fNewDir * 0.15f;
        m_Direction.Normalize();
        transform.Translate(m_Direction.x * dt * m_fSpeed, 0, m_Direction.y * dt * m_fSpeed);
        m_Origin = transform.position;
        if (transform.position.y > 4.0f) Reset();
	}
}
