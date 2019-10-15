using UnityEngine;

public class CFX_ParticleSystemScaler : MonoBehaviour
{

    WindZone wind;
    float startWindRadius;
    float startWindStr;

    Light _light;
    float startLightRange;
	// Use this for initialization
	void Start ()
	{
	    wind = GetComponentInChildren<WindZone>();
	    if (wind != null)
	    {
	        startWindRadius = wind.radius;
	        startWindStr = wind.windMain;
	    }

	    _light = GetComponentInChildren<Light>();
        if(_light!=null) startLightRange = _light.range;

        var particles = GetComponentsInChildren<ParticleSystem>();
	    foreach (var particle in particles)
	    {
#if UNITY_5_5_OR_NEWER
            var main = particle.main;
            main.scalingMode = ParticleSystemScalingMode.Hierarchy;
#else
            particle.scalingMode = ParticleSystemScalingMode.Hierarchy;
#endif
        }
	}
	
	// Update is called once per frame
	void Update ()
	{
	    var localScale = transform.localScale;
	    var scale = Mathf.Min(Mathf.Min(localScale.x, localScale.y), localScale.z);
	    if (wind != null)
	    {
	        wind.radius = startWindRadius*scale;
	        wind.windMain = startWindStr*scale;
	    }

        if (_light != null) _light.range = startLightRange*scale;
	}
}
