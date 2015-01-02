using UnityEngine;
using System.Collections;

public class ParticleColorizer : MonoBehaviour
{

	private ParticleSystem ps = null;

	void Start()
	{
		ps = this.GetComponent<ParticleSystem>();
		ps.Play();
	}


	void Update()
	{
		Color color = new Color(
								0.75f + 0.25f*Mathf.Sin(Time.time * 1.0f),
								0.75f + 0.25f*Mathf.Cos(Time.time * 1.4f),
								0.75f + 0.25f*Mathf.Sin(Time.time * 1.3f)
								);

		ps.startColor = color;
	}
}
