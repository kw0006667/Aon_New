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
            // Y �b�W�L -2.4 �N��y�P�w�g�q�U�����}�e��, ���䭫�s�]�w
            // X �b�W�L 5.5 �N��y�P�w�g�q�k�����}�e��, ���䭫�s�]�w		
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
        //�H�����ͬy�P����m, Y �y�г]�w�� 4.9, �N�|�b�������W��
        // X �b�b -6.5 �� 2 ����, Z �T�w�b 13 ����m
        // Y �b�y�Цb 4.9 �P 6.1 ����, �F���X�{������ĪG

        // ���ͬy�ʪ��t��, 1.5 ��� 4 ����������ӵe��
        // �첾�q�H (-4.4 5.25 13) �� (5.5, -3.4 , 13) �Z���� 13.4 ���̾�
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
