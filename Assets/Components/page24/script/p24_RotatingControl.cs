using UnityEngine;
using System.Collections;

public class p24_RotatingControl : MonoBehaviour {
    public float m_fdegree; // �ثe��ʪ�����
    public GameObject[] m_Cloudbject = new GameObject[4];
    public p24_CloudRotation m_Cloud1Script;
    public p24_CloudRotation m_Cloud2Script;
    public p24_CloudRotation m_Cloud3Script;
    public p24_CloudRotation m_Cloud4Script;
    public Vector2 m_PrevPos;
    public Vector2 m_CurrPos;
    public Vector2 m_EndPos;
    public Vector2 m_TPDelta;
    public Vector2 m_FWCenter;
    public Ray touchRay;
    public bool m_bAutoRotating;
    public int m_iCloud; // �ثe TP ���I�쪺 cloud �s��
    public float m_fSpeed;


	// Use this for initialization
	void Start () {
        m_fdegree = 0; // �w�]��0��	
        m_Cloudbject[0] = GameObject.Find("aonp24_i_1");
        m_Cloudbject[1] = GameObject.Find("aonp24_i_2");
        m_Cloudbject[2] = GameObject.Find("aonp24_i_3");
        m_Cloudbject[3] = GameObject.Find("aonp24_i_4");
        m_Cloud1Script = m_Cloudbject[0].GetComponent<p24_CloudRotation>();
        m_Cloud2Script = m_Cloudbject[1].GetComponent<p24_CloudRotation>();
        m_Cloud3Script = m_Cloudbject[2].GetComponent<p24_CloudRotation>();
        m_Cloud4Script = m_Cloudbject[3].GetComponent<p24_CloudRotation>();
        m_FWCenter.x = 0.0f; m_FWCenter.y = 0.0f;
        m_bAutoRotating = false;
        m_iCloud = -1;
        m_fSpeed = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.MetroPlayerARM)
        {
            touchRay = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                if (touchRay.origin.x >= -4.75f && touchRay.origin.x <= 4.75f && touchRay.origin.y >= -1.5f && touchRay.origin.y <= 3.5f)
                {
                    m_PrevPos.x = touchRay.origin.x;
                    m_PrevPos.y = touchRay.origin.y;
                    // �P�_�ثeTP�Ҧb�� cloud
                    float t = m_PrevPos.x * m_PrevPos.x + m_PrevPos.y * m_PrevPos.y;
                    // cloud �����|�h(0,0)���ѦҤ��� �b�|  1.7 �� 2.5 ���ĥ|�h 
                    // 2.5 �� 2.9 ���ĤT�h  2.9 �� 3.5 ���ĤG�h, 3.5 ���~����1�h
                    // (1.7^2 = 2.89,   2.5^2 = 6.25     2.9^2 = 8.41    3.5^2 = 12.25 )
                    if (t >= 2.89 && t < 6.25) m_iCloud = 4;
                    else if (t >= 6.25 && t < 8.41) m_iCloud = 3;
                    else if (t >= 8.41 && t < 12.25) m_iCloud = 2;
                    else if (t >= 12.25) m_iCloud = 1;
                    if (m_bAutoRotating)
                    {
                        // �I�s�|�� cloud ���L�̰������
                        m_Cloud1Script.Stop();
                        m_Cloud2Script.Stop();
                        m_Cloud3Script.Stop();
                        m_Cloud4Script.Stop();
                        m_CurrPos = m_PrevPos;
                    }

                }
            }
            else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                // �ھک즲�����f�ɤ�V�ӳ]�w��ʪ���V, �ھڲ��ʪ��t�Z�ӭp����ʪ�����
                if ( touchRay.origin.x >= -4.75f && touchRay.origin.x <= 4.75f && touchRay.origin.y >= -1.5f && touchRay.origin.y <= 3.5f)
                {
                    m_CurrPos.x = touchRay.origin.x;
                    m_CurrPos.y = touchRay.origin.y;
                    float px, py, nx, ny, t;
                    px = m_PrevPos.x - m_FWCenter.x; // ���ѽ����߫��V�e�@�� TP ���V�q
                    py = m_PrevPos.y - m_FWCenter.y;
                    nx = m_CurrPos.x - m_FWCenter.x; // ���ѽ����߫��V�ثe TP ���V�q
                    ny = m_CurrPos.y - m_FWCenter.y;
                    t = px * ny - py * nx; // (px,py) �P (nx, ny) ���~�n, > 0 �N��f�ɰw, < 0 �N���ɰw
                    m_TPDelta = Input.GetTouch(0).deltaPosition;
                    if (t >= 0) m_fdegree = -m_TPDelta.SqrMagnitude() * 1.5f;
                    else m_fdegree = m_TPDelta.SqrMagnitude() * 1.5f;

                    switch (m_iCloud)
                    {
                        case 1:
                            m_Cloud1Script.RotateWithDegree(m_fdegree);
                            m_Cloud2Script.RotateWithDiff(m_fdegree);
                            m_Cloud3Script.RotateWithDiff(-m_fdegree);
                            m_Cloud4Script.RotateWithDiff(-m_fdegree);
                            break;
                        case 2:
                            m_Cloud1Script.RotateWithDiff(m_fdegree);
                            m_Cloud2Script.RotateWithDegree(m_fdegree);
                            m_Cloud3Script.RotateWithDiff(-m_fdegree);
                            m_Cloud4Script.RotateWithDiff(-m_fdegree);
                            break;
                        case 3:
                            m_Cloud1Script.RotateWithDiff(-m_fdegree);
                            m_Cloud2Script.RotateWithDiff(-m_fdegree);
                            m_Cloud3Script.RotateWithDegree(m_fdegree);
                            m_Cloud4Script.RotateWithDiff(m_fdegree);
                            break;
                        case 4:
                            m_Cloud1Script.RotateWithDiff(-m_fdegree);
                            m_Cloud2Script.RotateWithDiff(-m_fdegree);
                            m_Cloud3Script.RotateWithDiff(m_fdegree);
                            m_Cloud4Script.RotateWithDegree(m_fdegree);
                            break;
                    }
                    m_PrevPos = m_CurrPos;
                }
            }
            else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                float px, py, nx, ny, t;
                Vector2 FinalVec;
                m_EndPos.x = touchRay.origin.x;
                m_EndPos.y = touchRay.origin.y;
                FinalVec = m_EndPos - m_CurrPos;
                float fdiff = FinalVec.SqrMagnitude();               
                // �P�_���ʪ��t�׬O�_�W�L�@�w�t��, ���|��cloud �۰ʱ���
                if (fdiff > 0.25f ) {
                    // �p��t�סA �����@�Ӳ��ʪ��q, �� Update �B��ۦ����
                    m_fSpeed = fdiff;
                    px = m_CurrPos.x - m_FWCenter.x; // ���ѽ����߫��V�e�@�� TP ���V�q
                    py = m_CurrPos.y - m_FWCenter.y;
                    nx = m_EndPos.x - m_FWCenter.x; // ���ѽ����߫��V�ثe TP ���V�q
                    ny = m_EndPos.y - m_FWCenter.y;
                    t = px * ny - py * nx; // (px,py) �P (nx, ny) ���~�n, > 0 �N��f�ɰw, < 0 �N���ɰw
                    if (t >= 0) m_fdegree = -m_fSpeed*1.25f;
                    else m_fdegree = m_fSpeed * 1.25f;
                    m_bAutoRotating = true;
                    switch (m_iCloud)
                    {
                        case 1:
                            m_Cloud1Script.AutoRotating(m_fdegree);
                            m_Cloud2Script.AutoRotatingDiff(m_fdegree);
                            m_Cloud3Script.AutoRotatingDiff(-m_fdegree);
                            m_Cloud4Script.AutoRotatingDiff(-m_fdegree);
                            break;
                        case 2:
                            m_Cloud1Script.AutoRotatingDiff(m_fdegree);
                            m_Cloud2Script.AutoRotating(m_fdegree);
                            m_Cloud3Script.AutoRotatingDiff(-m_fdegree);
                            m_Cloud4Script.AutoRotatingDiff(-m_fdegree);
                            break;
                        case 3:
                            m_Cloud1Script.AutoRotatingDiff(-m_fdegree);
                            m_Cloud2Script.AutoRotatingDiff(-m_fdegree);
                            m_Cloud3Script.AutoRotating(m_fdegree);
                            m_Cloud4Script.AutoRotatingDiff(m_fdegree);
                            break;
                        case 4:
                            m_Cloud1Script.AutoRotatingDiff(-m_fdegree);
                            m_Cloud2Script.AutoRotatingDiff(-m_fdegree);
                            m_Cloud3Script.AutoRotatingDiff(m_fdegree);
                            m_Cloud4Script.AutoRotating(m_fdegree);
                            break;
                    }
                }
            }
        }	
	}
    void OnGUI()
    {
        //GUI.Label(new Rect(Screen.currentResolution.width / 2 + 150, Screen.currentResolution.height / 2 - 300, 200, 50), m_fdegree.ToString());
        //GUI.Label(new Rect(Screen.currentResolution.width / 2 + 150, Screen.currentResolution.height / 2 -250, 200, 50), m_PrevPos.ToString());
        //GUI.Label(new Rect(Screen.currentResolution.width / 2 + 150, Screen.currentResolution.height / 2 - 200, 200, 50), m_EndPos.ToString());
    }
}
