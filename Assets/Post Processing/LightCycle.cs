using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;


[ExecuteAlways]
public class LightCycle : MonoBehaviour
{

    [SerializeField] PostProcessVolume ppv;
    [SerializeField] Light sun;
    [SerializeField, Range(0,24)] public float timeOfDay;

    [SerializeField] Material sky;

    [SerializeField] string dayState;
    
    [SerializeField] float tempo = 1;

    //Lighting presets developed and fine-tuned by Dwayne. Implemented in code by Ian.
    LightPreset morning = new LightPreset(1f,5f,new Color(255,187,0)/255,new Color(200,120,69)/255,-25f,new Color(100,100,100)/255,Color.white,150f);
    LightPreset day = new LightPreset(1f,45f,new Color(255,245,215)/255,Color.white,0,new Color(90,130,120)/255,Color.white,1000f);

    LightPreset noon = new LightPreset(1.05f,90f,new Color(255,255,225)/255,Color.white,0,new Color(100,140,130)/255,Color.white,1000f);

    LightPreset dusk = new LightPreset(0.7f,175f,new Color(255,187,0)/255,new Color(200,100,69)/255,-25f,new Color(100,100,100)/255,Color.white,150f);

    LightPreset night = new LightPreset(0f,270f,new Color(255,187,0)/255,new Color(50,50,128)/255,-50f,new Color(100,100,100)/255,Color.white,150f);

    // Start is called before the first frame update
    void Start()
    {
        ppv = GameObject.Find("Post_Processor").GetComponent<PostProcessVolume>();
        sky = RenderSettings.skybox;
        sun = GameObject.Find("Sunlight").GetComponent<Light>();
        timeOfDay = 4;
    }

    // Update is called once per frame
    void Update()
    {
        if(Application.isPlaying)
        {
            timeOfDay += Time.deltaTime * tempo;
            timeOfDay %= 24;
            adjustLighting(timeOfDay/24);
        }
        else
        {
            adjustLighting(timeOfDay/24);
        }
    }

    //Adjust the lighting of the level to match the defined lighting presets.
    //Uses linear interpolation to smootly trasition between the designated states.
    //Makes use of a blended skybox shader (Created by BOXPHOBIC) to transition from a morning sky to a night sky
    void adjustLighting(float timeCheck)
    {
        //Variable tracking how much time has progressed in the current "phase" of the day/night cycle as a percentage
        float timing;
        
        if(timeCheck < 0.2f && timeCheck >= 0) //morning to day
        {
            //String to show what "state" the day currently is in, for editing/testing purposes
            dayState = "dawn";
            //Calculation of the "percent" the current state has progressed 
            timing = (0.2f - (0.2f-timeCheck))/0.2f;
            //Adjust the intensity of the sun
            sun.intensity = Mathf.Lerp(morning.sunIntensity,day.sunIntensity,timing);
            //Adjust the angle of the sun
            sun.GetComponent<Transform>().localEulerAngles = Vector3.Lerp(new Vector3(morning.sunAngleX, 230, 0), new Vector3(day.sunAngleX, 230, 0), timing);
            //Adjust color of the sun
            sun.color = Color.Lerp(morning.sunColor,day.sunColor,timing);
            //Adjust post processing color filter
            ppv.profile.GetSetting<ColorGrading>().colorFilter.Override(Color.Lerp(morning.ppColor,day.ppColor,timing));
            //Adjust post processing saturation
            ppv.profile.GetSetting<ColorGrading>().saturation.Override(Mathf.Lerp(morning.ppSaturation,day.ppSaturation,timing));
            //Ensure ambient lighting mode is correctly set
            RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Flat;
            //Adjust ambient lighting color
            RenderSettings.ambientSkyColor = Color.Lerp(morning.ambiantColor,day.ambiantColor,timing);
            //Adjust ambient fog color
            RenderSettings.fogColor = Color.Lerp(morning.fogColor,day.fogColor,timing);
            //Adjust fog end distance
            RenderSettings.fogEndDistance = Mathf.Lerp(morning.fogEnd,day.fogEnd,timing);
            sky.SetFloat("_CubemapTransition", Mathf.Lerp(0.4f,0f,timing));
        }
        else if(timeCheck < 0.4f && timeCheck >= 0.2f) //day to noon
        {
            dayState = "day";
            timing = (0.2f - (0.4f-timeCheck))/0.2f;
            sun.intensity = Mathf.Lerp(day.sunIntensity,noon.sunIntensity,timing);
            sun.GetComponent<Transform>().localEulerAngles = Vector3.Lerp(new Vector3(day.sunAngleX, 230, 0), new Vector3(noon.sunAngleX, 230, 0), timing);
            sun.color = Color.Lerp(day.sunColor,noon.sunColor,timing);
            ppv.profile.GetSetting<ColorGrading>().colorFilter.Override(Color.Lerp(day.ppColor,noon.ppColor,timing));
            ppv.profile.GetSetting<ColorGrading>().saturation.Override(Mathf.Lerp(day.ppSaturation,noon.ppSaturation,timing));
            RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Flat;
            RenderSettings.ambientSkyColor = Color.Lerp(day.ambiantColor,noon.ambiantColor,timing);
            RenderSettings.fogColor = Color.Lerp(day.fogColor,noon.fogColor,timing);
            RenderSettings.fogEndDistance = Mathf.Lerp(day.fogEnd,noon.fogEnd,timing);
            sky.SetFloat("_CubemapTransition", 0f);

        }
        else if(timeCheck < 0.6f  && timeCheck >= 0.4f) //noon to dusk
        {
            dayState = "noon";
            timing = (0.2f - (0.6f-timeCheck))/0.2f;
            sun.intensity = Mathf.Lerp(noon.sunIntensity,dusk.sunIntensity,timing);
            sun.GetComponent<Transform>().localEulerAngles = Vector3.Lerp(new Vector3(noon.sunAngleX, 230, 0), new Vector3(dusk.sunAngleX, 230, 0), timing);
            sun.color = Color.Lerp(noon.sunColor,dusk.sunColor,timing);
            ppv.profile.GetSetting<ColorGrading>().colorFilter.Override(Color.Lerp(noon.ppColor,dusk.ppColor,timing));
            ppv.profile.GetSetting<ColorGrading>().saturation.Override(Mathf.Lerp(noon.ppSaturation,dusk.ppSaturation,timing));
            RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Flat;
            RenderSettings.ambientSkyColor = Color.Lerp(noon.ambiantColor,dusk.ambiantColor,timing);
            RenderSettings.fogColor = Color.Lerp(noon.fogColor,dusk.fogColor,timing);
            RenderSettings.fogEndDistance = Mathf.Lerp(noon.fogEnd,dusk.fogEnd,timing);
            sky.SetFloat("_CubemapTransition", Mathf.Lerp(0f,0.8f,timing));
        }
        else if(timeCheck < 0.8f  && timeCheck >= 0.6f) //dusk to night
        {
            dayState = "dusk";
            timing = (0.2f - (0.8f-timeCheck))/0.2f;
            sun.intensity = Mathf.Lerp(dusk.sunIntensity,night.sunIntensity,timing);
            sun.GetComponent<Transform>().localEulerAngles = Vector3.Lerp(new Vector3(dusk.sunAngleX, 230, 0), new Vector3(night.sunAngleX, 230, 0), timing);
            sun.color = Color.Lerp(dusk.sunColor,night.sunColor,timing);
            ppv.profile.GetSetting<ColorGrading>().colorFilter.Override(Color.Lerp(dusk.ppColor,night.ppColor,timing));
            ppv.profile.GetSetting<ColorGrading>().saturation.Override(Mathf.Lerp(dusk.ppSaturation,night.ppSaturation,timing));
            RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Flat;
            RenderSettings.ambientSkyColor = Color.Lerp(dusk.ambiantColor,night.ambiantColor,timing);
            RenderSettings.fogColor = Color.Lerp(dusk.fogColor,night.fogColor,timing);
            RenderSettings.fogEndDistance = Mathf.Lerp(dusk.fogEnd,night.fogEnd,timing);
            sky.SetFloat("_CubemapTransition", Mathf.Lerp(0.8f,1f,timing));
        }
        else //night to day
        {
            dayState = "night";
            timing = (0.2f - (1f-timeCheck))/0.2f;
            sun.intensity = Mathf.Lerp(night.sunIntensity,morning.sunIntensity,timing);
            sun.GetComponent<Transform>().localEulerAngles = Vector3.Lerp(new Vector3(night.sunAngleX, 230, 0), new Vector3(365, 230, 0), timing);
            sun.color = Color.Lerp(night.sunColor,morning.sunColor,timing);
            ppv.profile.GetSetting<ColorGrading>().colorFilter.Override(Color.Lerp(night.ppColor,morning.ppColor,timing));
            ppv.profile.GetSetting<ColorGrading>().saturation.Override(Mathf.Lerp(night.ppSaturation,morning.ppSaturation,timing));
            RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Flat;
            RenderSettings.ambientSkyColor = Color.Lerp(night.ambiantColor,morning.ambiantColor,timing);
            RenderSettings.fogColor = Color.Lerp(night.fogColor,morning.fogColor,timing);
            RenderSettings.fogEndDistance = Mathf.Lerp(night.fogEnd,morning.fogEnd,timing);
            sky.SetFloat("_CubemapTransition", Mathf.Lerp(1f,0.4f,timing));
        }
        print(timing);
    }
}
