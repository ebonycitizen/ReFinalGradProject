using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public enum StylizedFogMode
{
	Linear,
	Exponential,
	ExponentialSquared
}

[Serializable]
public sealed class FogModeParameter : ParameterOverride<FogMode> { }

[Serializable]
[PostProcess(typeof(StylizedFogRenderer), PostProcessEvent.AfterStack, "Custom/StylizedFog")]
public sealed class StylizedFog : PostProcessEffectSettings
{
	// ReSharper disable FieldCanBeMadeReadOnly.Global
	[Tooltip("The fog mode."), DisplayName("Fog Mode")]
	public FogModeParameter fogMode = new FogModeParameter { value = FogMode.ExponentialSquared };

	[Range(0f, 0.1f), Tooltip("Fog density.")]
	public FloatParameter density = new FloatParameter {value = 0.01f};

	[UnityEngine.Min(0f), Tooltip("Fog start.")]
	public FloatParameter start = new FloatParameter { value = 0.0f };

	[UnityEngine.Min(0f), Tooltip("Fog end.")]
	public FloatParameter end = new FloatParameter { value = 300.0f };

	[Range(0f, 1f), Tooltip("Fog color spread.")]
	public FloatParameter spread = new FloatParameter {value = 0.0f};

    [Tooltip("The color of the fog")]
    public ColorParameter fogColor = new ColorParameter { value = Color.white };

    [Tooltip("Gradient for the fog color."), DisplayName("Gradient")]
	public TextureParameter gradientTexture = new TextureParameter { value = null };

	[Tooltip("Should the fog affect the sky?"), DisplayName("Exclude sky")]
	public BoolParameter excludeSky = new BoolParameter {value = true};

	[Tooltip("Should fog depth be distance based?"), DisplayName("Use Distance")]
	public BoolParameter useWSDistance = new BoolParameter { value = false };


	public override bool IsEnabledAndSupported(PostProcessRenderContext context)
	{
		return enabled.value 
		       && !RenderSettings.fog; // default fog should be disabled
	}
}


public class StylizedFogRenderer : PostProcessEffectRenderer<StylizedFog>
{
	private Shader shader;

	public override DepthTextureMode GetCameraFlags()
	{
		return DepthTextureMode.Depth;
	}

	public override void Init()
	{
		base.Init();
		shader = Shader.Find("Hidden/Custom/StylizedFog");
	}

	public override void Render(PostProcessRenderContext context)
	{
		var gradientTexture = settings.gradientTexture.value == null
			? RuntimeUtilities.whiteTexture
			: settings.gradientTexture.value;

		RenderSettings.fogMode = settings.fogMode;

		var sheet = context.propertySheets.Get(shader);

		if (settings.fogMode == FogMode.Linear)
		{
			sheet.DisableKeyword("STYLIZEDFOG_EXP");
			sheet.DisableKeyword("STYLIZEDFOG_EXP2");
			sheet.EnableKeyword("STYLIZEDFOG_LINEAR");
		}
		else if (settings.fogMode == FogMode.Exponential)
		{
			sheet.DisableKeyword("STYLIZEDFOG_LINEAR");
			sheet.DisableKeyword("STYLIZEDFOG_EXP2");
			sheet.EnableKeyword("STYLIZEDFOG_EXP");
		}
		else
		{
			sheet.DisableKeyword("STYLIZEDFOG_LINEAR");
			sheet.DisableKeyword("STYLIZEDFOG_EXP");
			sheet.EnableKeyword("STYLIZEDFOG_EXP2");
		}

		sheet.properties.SetVector("_FogParams", new Vector3(settings.density, settings.start, settings.end));
		sheet.properties.SetFloat("_Spread", settings.spread);
		sheet.properties.SetTexture("_FogGradient", gradientTexture);
        sheet.properties.SetVector("_FogColor", settings.fogColor);

        if (settings.useWSDistance)
		{
			sheet.EnableKeyword("STYLIZEDFOG_DISTANCE");

			// needed for world pos reconstruction
			Camera cam = context.camera;
			Transform camtr = cam.transform;

			Vector3[] frustumCorners = new Vector3[4];
			cam.CalculateFrustumCorners(new Rect(0, 0, 1, 1), cam.farClipPlane, cam.stereoActiveEye, frustumCorners);
			var bottomLeft = camtr.TransformVector(frustumCorners[1]);
			var topLeft = camtr.TransformVector(frustumCorners[0]);
			var bottomRight = camtr.TransformVector(frustumCorners[2]);

			Matrix4x4 frustumVectorsArray = Matrix4x4.identity;
			frustumVectorsArray.SetRow(0, bottomLeft);
			frustumVectorsArray.SetRow(1, bottomLeft + (bottomRight - bottomLeft) * 2);
			frustumVectorsArray.SetRow(2, bottomLeft + (topLeft - bottomLeft) * 2);

			sheet.properties.SetMatrix("_FrustumVectorsWS", frustumVectorsArray);
			sheet.properties.SetVector("_CameraWS", camtr.position);
		}
		else
		{
			sheet.DisableKeyword("STYLIZEDFOG_DISTANCE");
		}

		context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, settings.excludeSky ? 1 : 0);
	}
}
