using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
[Serializable, PostProcess(typeof(OutlinePostProcess), PostProcessEvent.BeforeStack, "Custom/Outline")]
public sealed class Outline : PostProcessEffectSettings
{
    [Tooltip("Outline Thickness")]
    public IntParameter thickness = new IntParameter{value = 1};
    [Tooltip("Outline Edge")]
    public FloatParameter edge = new FloatParameter{value = 0.2f};
    [Tooltip("Outline Transition Smoothness based on distance to object")]
    public FloatParameter transitionSmoothness = new FloatParameter{value = 20.0f};
    [Tooltip("Outline Color")]
    public ColorParameter color = new ColorParameter{value = Color.black};
}

public sealed class OutlinePostProcess : PostProcessEffectRenderer<Outline>
{
    public override void Render(PostProcessRenderContext context)
    {
        var sheet = context.propertySheets.Get(Shader.Find("Custom/Outline"));
        sheet.properties.SetInt("_Thickness", settings.thickness);
        sheet.properties.SetFloat("_Edge", settings.edge);
        sheet.properties.SetFloat("_TransitionSmoothness", settings.transitionSmoothness);
        sheet.properties.SetColor("_Color", settings.color);
        context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
    }
}