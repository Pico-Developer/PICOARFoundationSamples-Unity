using System.Collections;
using System.Collections.Generic;
using Unity.XR.PXR;
using UnityEngine;

public class CameraEffectTest : MonoBehaviour
{
    public Texture2D lutTex;

    private int row = 0;
    private int col = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetColortemp(float x)
    {
        PXR_MixedReality.SetVideoSeeThroughEffect(PxrLayerEffect.Colortemp, x, 1);
    }
    public void SetBrightness(float x)
    {
        PXR_MixedReality.SetVideoSeeThroughEffect(PxrLayerEffect.Brightness, x, 1);
    }
    public void SetSaturation(float x)
    {
        PXR_MixedReality.SetVideoSeeThroughEffect(PxrLayerEffect.Saturation, x, 1);
    }
    public void SetContrast(float x)
    {
        PXR_MixedReality.SetVideoSeeThroughEffect(PxrLayerEffect.Contrast, x, 1);
    }
    public void SetLutRow(float x)
    {
        if (lutTex)
        {
            row = (int)(lutTex.width * x);
            PXR_MixedReality.SetVideoSeeThroughLut(lutTex, row, col);
        }
    }
    public void SetLutCol(float x)
    {
        if (lutTex)
        {
            col = (int)(lutTex.height * x);
            PXR_MixedReality.SetVideoSeeThroughLut(lutTex, row, col);
        }
    }
}
