using UnityEngine;
using System.Collections;

public class p14_FerrisWheel : MonoBehaviour {
    public float m_fdegree; // 目前轉動的角度
    public GameObject[]  m_CarObject = new GameObject[8];
    public p14_Carriages[] m_CarScript = new p14_Carriages[8];
    public Vector2 m_PrevPos;
    public Vector2 m_CurrPos;
    public Vector2 m_TPDelta;
    public Vector2 m_FWCenter;
    public Ray touchRay;
    public bool m_bMovinWheel;

    public float m_t;


	// Use this for initialization
	void Start () {
        m_bMovinWheel = false;
        m_fdegree = 0; // 預設為0度	
        m_CarObject[0] = GameObject.Find("aonp14_i_fun11");
        m_CarObject[1] = GameObject.Find("aonp14_i_fun12");
        m_CarObject[2] = GameObject.Find("aonp14_i_fun13");
        m_CarObject[3] = GameObject.Find("aonp14_i_fun14");
        m_CarObject[4] = GameObject.Find("aonp14_i_fun15");
        m_CarObject[5] = GameObject.Find("aonp14_i_fun16");
        m_CarObject[6] = GameObject.Find("aonp14_i_fun17");
        m_CarObject[7] = GameObject.Find("aonp14_i_fun18");
        for( int i = 0 ; i < 8 ; i++ ) 
            m_CarScript[i] = m_CarObject[i].GetComponent<p14_Carriages>();

        m_FWCenter.x = -2.56767f; m_FWCenter.y = 0.5497255f;
	}
	
	// Update is called once per frame
	void Update () {
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.MetroPlayerARM)
        {
            touchRay = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began ) {
                if (touchRay.origin.x >= -4.8f && touchRay.origin.x <= 0.6f && touchRay.origin.y >= -1.5f && touchRay.origin.y <= 3.5f)
                { // 起點落在摩天輪的範圍內
                    m_PrevPos.x = touchRay.origin.x;
                    m_PrevPos.y = touchRay.origin.y;
                    m_bMovinWheel = true;
                }
            }
            else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) {
                // 根據拖曳的順逆時方向來設定轉動的方向, 根據移動的差距來計算轉動的角度
                if (touchRay.origin.x >= -4.8f && touchRay.origin.x <= 0.6f && touchRay.origin.y >= -1.5f && touchRay.origin.y <= 3.5f)
                {
                    m_CurrPos.x = touchRay.origin.x;
                    m_CurrPos.y = touchRay.origin.y;
                    float px, py, nx, ny, t;
                    px = m_PrevPos.x - m_FWCenter.x; // 摩天輪中心指向前一個 TP 的向量
                    py = m_PrevPos.y - m_FWCenter.y;
                    nx = m_CurrPos.x - m_FWCenter.x; // 摩天輪中心指向目前 TP 的向量
                    ny = m_CurrPos.y - m_FWCenter.y;
                    m_t = px * ny - py * nx; // (px,py) 與 (nx, ny) 的外積, > 0 代表逆時針, < 0 代表順時針
                    m_TPDelta = Input.GetTouch(0).deltaPosition;
                    if (m_t >= 0) m_fdegree = -m_TPDelta.SqrMagnitude()*1.5f;
                    else m_fdegree = m_TPDelta.SqrMagnitude() * 1.5f;
                    transform.Rotate(0, m_fdegree, 0);
                    for (int i = 0; i < 8; i++) m_CarScript[i].SetDegree(m_fdegree); // 有呼叫才需要轉動一次, 採用一步到位的方式執行?
                    m_fdegree = 0;
                    m_PrevPos = m_CurrPos;
                }
                else {
                    if (m_bMovinWheel) m_bMovinWheel = false;
                }
            }
            else {
                if( m_bMovinWheel ) m_bMovinWheel = false;
            }
        }
	}

    void OnGUI()
    {
        //GUI.Label(new Rect(Screen.currentResolution.width / 2+150, Screen.currentResolution.height / 2-300, 200, 50), m_CurrPos.ToString());
        //GUI.Label(new Rect(Screen.currentResolution.width / 2 + 150, Screen.currentResolution.height / 2 -250, 200, 50), m_PrevPos.ToString());
        //GUI.Label(new Rect(Screen.currentResolution.width / 2 + 150, Screen.currentResolution.height / 2 -200, 200, 50), m_t.ToString());
    }
}

