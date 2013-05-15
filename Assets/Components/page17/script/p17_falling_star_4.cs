using UnityEngine;
using System.Collections;

public class p17_falling_star_4 : MonoBehaviour
{

    public float m_fSpeed;
    public float m_fYangle;
    public float m_fZangle;
    public float m_fElapsedTime;
    public float m_fScale;
    public Vector3 m_Direction;
    public Vector3 m_Position;
    public float m_fx, m_fy;


    void Start()
    {
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;
        m_fElapsedTime += dt;
        if (m_fElapsedTime <= m_fSpeed)
        {
            transform.position = new Vector3(m_Position.x, m_Position.y, m_Position.z);

            float tmp = 13.4f * (1.0f - Mathf.Cos(0.5f * Mathf.PI * m_fElapsedTime / m_fSpeed));
            transform.Translate(m_Direction * tmp);
            m_fx = transform.position.x; m_fy = transform.position.y;
            // Y 軸超過 -2.4 代表流星已經從下方離開畫面, 讓其重新設定
            // X 軸超過 5.5 代表流星已經從右方離開畫面, 讓其重新設定		
            if (m_fx >= 5.5f || m_fy <= -2.4f)
            {
                m_fElapsedTime = m_fSpeed;
            }
        }
        else
        {
            // RESET ROTATION AND SCALE
            transform.position = new Vector3(m_Position.x, m_Position.y, m_Position.z);
            this.transform.Rotate(0, 0, -m_fZangle);
            this.transform.Rotate(0, -m_fYangle, 0);
            transform.localScale += new Vector3(m_fScale, 0, m_fScale);
            Reset();
        }
    }

    void Reset()
    {
        //隨機產生流星的位置, Y 座標設定成 4.9, 就會在場景的上方
        // X 軸在 -6.5 到 2 之間, Z 固定在 13 的位置
        // Y 軸座標在 4.9 與 6.1 之間, 達成出現的延遲效果

        // 產生流動的速度, 1.5 秒到 4 秒之間走完整個畫面
        // 位移量以 (-4.4 5.25 13) 到 (5.5, -3.4 , 13) 距離約 13.4 為依據
        m_fSpeed = Random.Range(4.5f, 6.0f);
        m_fYangle = Random.Range(-30.0f, -40.0f);

        m_Position = new Vector3(Random.Range(-6.5f, 2.0f), Random.Range(4.9f, 6.1f), 13.0f);
        m_fZangle = Random.Range(2.5f, 25.0f);
        this.transform.Rotate(0, m_fYangle, 0);
        this.transform.Rotate(0, 0, m_fZangle);
        //m_Direction = new Vector3(1, 0, 0);
        m_fElapsedTime = 0.0f;
        m_fScale = Random.Range(0.01f, 0.1f);
        transform.localScale -= new Vector3(m_fScale, 0, m_fScale);
        transform.position = new Vector3(m_Position.x, m_Position.y, m_Position.z);
    }
}
