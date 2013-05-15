using UnityEngine;
using System.Collections;

public class TextureAnimation : MonoBehaviour
{
    public float Speed;     // Play speed
    public float frameRate; // Play rate
    public Vector2 TextureOffest; // Every frame's offest
    public bool IsOffest;
    public bool IsPlay;     // Is play or not
    public bool IsNeedCallback;
    public LayerMask Mask;

    private Material m_mat; // Current Material
    private Vector2 m_offest;
    private int totalCount;
    private int count;
    private bool isPlaying;

    private RaycastHit hit;
    private Ray ray;

    private Object aonTrigger;

    // Use this for initialization
    void Start()
    {
        this.m_mat = this.renderer.material;
        this.count = 0;
        this.totalCount = this.TextureOffest.x >= this.TextureOffest.y
                            ? (int)(1.0f / this.TextureOffest.x)
                            : (int)(1.0f / this.TextureOffest.y);
        if (this.IsOffest)
            this.totalCount += 1;
        this.m_offest = this.m_mat.GetTextureOffset("_MainTex");
        this.isPlaying = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.IsPlay)
        {
            if (!this.isPlaying)
            {
                this.m_mat.SetTextureOffset("_MainTex", Vector2.zero);
                this.m_offest = this.m_mat.GetTextureOffset("_MainTex");
                InvokeRepeating("TextureOffset", this.Speed, this.frameRate);
                this.isPlaying = true;
            }
            if (this.m_mat)
            {
                if (this.count == this.totalCount - 1)
                {
                    CancelInvoke("TextureOffset");
                    this.IsPlay = false;
                    this.isPlaying = false;
                    this.count = 0;
                    if (this.IsNeedCallback)
                    {
                        ((AonTrigger)this.aonTrigger).MovingTrigger();
                    }

                }
            }
        }
        if (Debug.isDebugBuild)
        {
            Debug.Log(this.totalCount + " : " + this.count);
        }

    }

    void TextureOffset()
    {
        this.count++;
        this.m_mat.SetTextureOffset("_MainTex", this.m_offest + this.TextureOffest);
        this.m_offest = this.m_mat.GetTextureOffset("_MainTex");

    }

    public void PlayAnimation(bool _isPlay, Object _aon)
    {
        this.aonTrigger = _aon;
        this.GetComponent<MeshRenderer>().enabled = true;
        this.IsPlay = _isPlay;
    }
}
