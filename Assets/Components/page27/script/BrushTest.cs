using UnityEngine;
using System.Collections;

public class BrushTest : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void AddToStencil(Texture2D stencil, Texture2D brush, Vector2 brushPosition, int brushSizePixels)
    {
        //Create temporary render texture
        int width = stencil.width;
        int height = stencil.height;
        RenderTexture rt = RenderTexture.GetTemporary(width, height, 0, RenderTextureFormat.ARGB32);

        //Copy existing stencil to render texture (blit sets the active RenderTexture)
        Graphics.Blit(stencil, rt);

        //Apply brush
        RenderTexture.active = rt;
        float bs2 = brushSizePixels / 2f;
        Graphics.DrawTexture(new Rect(brushPosition.x - bs2, brushPosition.y - bs2, brushSizePixels, brushSizePixels), brush);

        //Read texture back to stencil
        stencil.ReadPixels(new Rect(0, 0, width, height), 0, 0, true);
        stencil.Apply();

        RenderTexture.active = null;
        rt.Release();
    }

}
