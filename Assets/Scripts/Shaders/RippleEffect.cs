using System.Collections;
using UnityEngine;

public class RippleEffect : MonoBehaviour
{
    public int TextureSize = 1024;
    public RenderTexture ObjectsRT;
    private RenderTexture CurrRT, PrevRT, TempRT;
    public Shader RippleShader, AddShader;
    public Material WaterShader;
    private Material RippleMat, AddMat;

    void Start()
    {
        CurrRT = new RenderTexture(TextureSize, TextureSize, 0, RenderTextureFormat.RFloat);
        PrevRT = new RenderTexture(TextureSize, TextureSize, 0, RenderTextureFormat.RFloat);
        TempRT = new RenderTexture(TextureSize, TextureSize, 0, RenderTextureFormat.RFloat);
        RippleMat = new Material(RippleShader);
        AddMat = new Material(AddShader);

        WaterShader.SetTexture("_RippleTex", CurrRT);

        StartCoroutine(ripples());
    }

    IEnumerator ripples()
    {
        AddMat.SetTexture("_ObjectsRT", ObjectsRT);
        AddMat.SetTexture("_CurrentRT", CurrRT);
        Graphics.Blit(null, TempRT, AddMat);

        RenderTexture rt0 = TempRT;
        TempRT = CurrRT;
        CurrRT = rt0;

        RippleMat.SetTexture("_PrevRT", PrevRT);
        RippleMat.SetTexture("_CurrentRT", CurrRT);
        Graphics.Blit(null, TempRT, RippleMat);
        Graphics.Blit(TempRT, PrevRT);

        RenderTexture rt = PrevRT;
        PrevRT = CurrRT;
        CurrRT = rt;

        yield return null;
        StartCoroutine(ripples());
    }
}
