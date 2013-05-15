using UnityEngine;
using System.Collections;
using System;

public class TextureBrush : MonoBehaviour
{
    public Material AonMaterial;
    public Texture2D BrushStencil;
    public string tagFilter;
    public LayerMask Mask;


    private Texture2D brush;
    private Texture2D tex;
    private Color[] stencilUV;
    private int i;
    private Vector2 pixelUV;

    

    // Use this for initialization
    void Start()
    {
        this.stencilUV = new Color[this.BrushStencil.width * this.BrushStencil.height];
        this.tex = Instantiate(this.AonMaterial.mainTexture) as Texture2D;
        this.AonMaterial.mainTexture = this.tex;
        //Debug.Log(this.BrushStencil.width + " : " + this.BrushStencil.height);
        Debug.Log("BrushStencil.mipmapCount = " + this.BrushStencil.mipmapCount);
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (DetectPaintable(tagFilter))
        {
            Debug.Log(pixelUV.x + " : " + pixelUV.y);
            //CreateStencil(Convert.ToInt16(pixelUV.x), Convert.ToInt16(pixelUV.y), BrushStencil);
            this.AonMaterial.mainTexture = this.tex;
        }
    }

    bool DetectPaintable(string tagFilter)
    {
        RaycastHit hit;
        if (Physics.Raycast(camera.ScreenPointToRay(Input.touches[0].position), out hit, 100, this.Mask))
        {
            if (hit.transform.gameObject.tag == tagFilter)
            {
                //if (!Input.GetMouseButton(0))
                //{
                //    return false;
                //}

                Renderer renderer = hit.collider.renderer;
                MeshCollider meshCollider = hit.collider as MeshCollider;
                if (renderer == null || renderer.sharedMaterial == null || renderer.sharedMaterial.mainTexture == null || meshCollider == null)
                {
                    return false;
                }
                pixelUV = hit.textureCoord;
                pixelUV.x *= tex.width;
                pixelUV.y *= tex.height;

                int i = 0;
                for (int px = Convert.ToInt16(pixelUV.x) - 32; px < Convert.ToInt16(pixelUV.x) + 32; px++)
                {
                    int j = 0;
                    for (int py = Convert.ToInt16(pixelUV.y) - 32; py < Convert.ToInt16(pixelUV.y) + 32; py++)
                    {
                        Color col = tex.GetPixel(px, py);
                        Color colBrush = this.BrushStencil.GetPixel(i, j);
                        //Debug.Log("r = " + colBrush.r + ", g = " + colBrush.g + ", b = " + colBrush.b + ", a = " + colBrush.a);
                        if (colBrush.a < 0.3f)
                        {
                            tex.SetPixel(px, py, Color.Lerp(new Color(col.r, col.g, col.b, col.a), new Color(colBrush.r, colBrush.g, colBrush.b, colBrush.a), 0.5f));
                        }
                        j++;
                    }
                    i++;
                }
                
                //tex.SetPixel(Convert.ToInt16(pixelUV.x), Convert.ToInt16(pixelUV.y), Color.Lerp(tex.GetPixel(Convert.ToInt16(pixelUV.x), Convert.ToInt16(pixelUV.y)), this.BrushStencil.GetPixel(Convert.ToInt16(pixelUV.x), Convert.ToInt16(pixelUV.y)), 255.0f));
                //tex.SetPixels(Convert.ToInt16(pixelUV.x) - 32, Convert.ToInt16(pixelUV.y) - 32, 64, 64, this.BrushStencil.GetPixels(Convert.ToInt16(pixelUV.x) - 32, Convert.ToInt16(pixelUV.y) - 32, 64, 64));
                
                
                //tex.SetPixels32(this.BrushStencil.GetPixels32());
                //tex.Apply();
                tex.Apply();
                return true;
            }
        }
        return false;
    }

    void CreateStencil(int x, int y, Texture2D texture)
    {
        this.AonMaterial.mainTexture = tex;
        for (int xPix = 0; xPix < texture.width; xPix++)
        {
            for (int yPix = 0; yPix < texture.height; yPix++)
            {
                stencilUV[i] = texture.GetPixel(xPix, yPix) * texture.GetPixel(xPix, yPix).a + tex.GetPixel((x - texture.width / 2) + xPix, (y - texture.height / 2) + yPix) * (1 - texture.GetPixel(xPix, yPix).a);
                i++;
            }
        }
        //Debug.Log("x = "+ x + ", y = " + y);
        i = 0;
        tex.SetPixels(x - texture.width / 2, y - texture.height / 2, texture.width, texture.height, stencilUV);
        tex.Apply();
    }
}
