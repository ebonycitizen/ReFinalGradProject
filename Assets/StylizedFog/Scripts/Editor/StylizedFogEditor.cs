using UnityEditor;
using UnityEditor.Rendering.PostProcessing;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[PostProcessEditor(typeof(StylizedFog))]
public sealed class StylizedFogEditor : PostProcessEffectEditor<StylizedFog>
{
	SerializedParameterOverride m_FogMode;
	SerializedParameterOverride m_Spread;
	SerializedParameterOverride m_Density;
	SerializedParameterOverride m_Start;
	SerializedParameterOverride m_End;
    SerializedParameterOverride m_FogColor;
    SerializedParameterOverride m_Gradient;
	SerializedParameterOverride m_ExcludeSky;
	SerializedParameterOverride m_UseWSDistance;

	public override void OnEnable()
	{
		m_FogMode = FindParameterOverride(x => x.fogMode);
		m_Spread = FindParameterOverride(x => x.spread);
		m_Density = FindParameterOverride(x => x.density);
		m_Start = FindParameterOverride(x => x.start);
		m_End = FindParameterOverride(x => x.end);
        m_FogColor = FindParameterOverride(x => x.fogColor);
        m_Gradient = FindParameterOverride(x => x.gradientTexture);
		m_ExcludeSky = FindParameterOverride(x => x.excludeSky);
		m_UseWSDistance = FindParameterOverride(x => x.useWSDistance);
	}

	public override void OnInspectorGUI()
	{
		PropertyField(m_FogMode);
		var mode = m_FogMode.value.enumValueIndex;

		if (mode == 0) // linear
		{
			PropertyField(m_Start);
			PropertyField(m_End);
		}
		else
		{
			PropertyField(m_Density);
		}

		PropertyField(m_Spread);
        PropertyField(m_FogColor);
        PropertyField(m_Gradient);
		PropertyField(m_ExcludeSky);
		PropertyField(m_UseWSDistance);
	}
}