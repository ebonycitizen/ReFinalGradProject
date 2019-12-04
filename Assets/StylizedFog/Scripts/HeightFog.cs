using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[Serializable]
[PostProcess(typeof(HeightFogRenderer), PostProcessEvent.AfterStack, "Custom/HeightFog")]
public sealed class HeightFog : PostProcessEffectSettings
{
	// ReSharper disable FieldCanBeMadeReadOnly.Global
	[Tooltip("Fog height.")]
	public FloatParameter height = new FloatParameter { value = 5f };

	[Range(0f, 10f), Tooltip("Fog density.")]
	public FloatParameter density = new FloatParameter { value = 5f };

	[UnityEngine.Min(0.0f), Tooltip("The starting distance of the height fog")]
	public FloatParameter startDistance = new FloatParameter { value = 0f };

	[Tooltip("The color of the fog")]
	public ColorParameter fogColor = new ColorParameter {value = Color.white};

	[Tooltip("Noise texture for the fog"), DisplayName("Noise")]
	public TextureParameter noiseTexture = new TextureParameter { value = null };

	[UnityEngine.Min(0.1f), Tooltip("Noise texture scale for the fog"), DisplayName("Noise scale")]
	public FloatParameter noiseTextureScale = new FloatParameter { value = 100.0f };

	[Tooltip("Speed and direction of the noise movement"), DisplayName("Noise speed")]
	public Vector2Parameter scrollSpeed = new Vector2Parameter { value = Vector2.zero };
}

public class HeightFogRenderer : PostProcessEffectRenderer<HeightFog>
{
	private readonly Vector3[] frustumCorners = new Vector3[4];
	private Shader shader;

	public override DepthTextureMode GetCameraFlags()
	{
		return DepthTextureMode.Depth;
	}

	public override void Init()
	{
		base.Init();
		shader = Shader.Find("Hidden/Custom/HeightFog");
	}

	public override void Render(PostProcessRenderContext context)
	{
		var sheet = context.propertySheets.Get(shader);

		var noiseTexture = settings.noiseTexture.value == null
			? RuntimeUtilities.whiteTexture
			: settings.noiseTexture.value;

		Camera cam = context.camera;
		Transform camtr = cam.transform;

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

		float FdotC = camtr.position.y - settings.height;
		float paramK = FdotC <= 0.0f ? 1.0f : 0.0f;

		sheet.properties.SetVector("_HeightParams", new Vector4(settings.height, FdotC, paramK, settings.density * 0.005f));
		sheet.properties.SetVector("_FogColor", settings.fogColor);
		sheet.properties.SetVector("_ScrollSpeed", settings.scrollSpeed);
		sheet.properties.SetTexture("_FogNoise", noiseTexture);
		sheet.properties.SetFloat("_FogNoiseScale", settings.noiseTextureScale);
		sheet.properties.SetFloat("_StartDistance", -Mathf.Max(settings.startDistance, 0.0f));

		context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
	}
}