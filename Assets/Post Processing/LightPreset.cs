using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPreset
{
    public Color sunColor {get; set;}

    public Color ppColor {get; set;}
    public Color ambiantColor {get; set;}

    public Color fogColor {get; set;}

    public float sunIntensity {get; set;}

    public float sunAngleX {get; set;}

    public float ppSaturation {get; set;}

    public float fogEnd {get; set;}

    public LightPreset(float sunIntensity, float sunAngleX, Color sunColor, Color ppColor, float ppSaturation, Color ambiantColor, Color fogColor, float fogEnd)
    {
        this.sunColor = sunColor;
        this.ppColor = ppColor;
        this.ambiantColor = ambiantColor;
        this.fogColor = fogColor;
        this.sunIntensity = sunIntensity;
        this.sunAngleX = sunAngleX;
        this.ppSaturation = ppSaturation;
        this.fogEnd = fogEnd;
    }
}
