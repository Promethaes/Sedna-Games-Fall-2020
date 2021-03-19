using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
[Serializable, PostProcess(typeof(ToonShadingPostProcess), PostProcessEvent.BeforeStack, "Custom/Toon Shading")]
public sealed class ToonShading : PostProcessEffectSettings
{
    [Tooltip("Color")]
    public ColorParameter color = new ColorParameter{value = new Color(1,1,1,1)};
    [Tooltip("Rim Color")]
    public ColorParameter rimcolor = new ColorParameter{value = new Color(1,1,1,1)};
    [Range(0,1), Tooltip("Rim Amount")]
    public FloatParameter rimamount = new FloatParameter {value = 0.716f};
    [Range(0,1), Tooltip("Rim Threshold")]
    public FloatParameter rimthreshold = new FloatParameter {value = 0.1f};
    [Tooltip("Specular Color")]
    public ColorParameter specularcolor = new ColorParameter{value = new Color(.9f,.9f,.9f,1)};
    [Tooltip("Glossiness Amount")]
    public FloatParameter glossiness = new FloatParameter {value = 32f};
    [Tooltip("Ambient Color")]
    public ColorParameter ambientcolor = new ColorParameter{value = new Color(.4f,.4f,.4f,1)};
}

public sealed class ToonShadingPostProcess : PostProcessEffectRenderer<ToonShading>
{
    public override void Render(PostProcessRenderContext context)
    {
        var sheet = context.propertySheets.Get(Shader.Find("Custom/Toon Shading Test"));
        sheet.properties.SetColor("_Color", settings.color);
        sheet.properties.SetColor("_RimColor", settings.rimcolor);
        sheet.properties.SetFloat("_RimAmount", settings.rimamount);
        sheet.properties.SetFloat("_RimThreshold", settings.rimthreshold);
        sheet.properties.SetColor("_SpecularColor", settings.specularcolor);
        sheet.properties.SetFloat("_Glossiness", settings.glossiness);
        sheet.properties.SetColor("_AmbientColor", settings.ambientcolor);
        context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
    }
}