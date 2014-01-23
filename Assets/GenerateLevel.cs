using UnityEngine;
using System.Collections;

public class GenerateLevel : MonoBehaviour {

	public float levelSize = 1000.0f;

	public Material material;

	void Start()
	{
		GameObject ground = GameObject.CreatePrimitive(PrimitiveType.Cube);
		ground.transform.localScale = new Vector3(levelSize, 1.0f, levelSize);
		ground.collider.enabled = false;
		ground.renderer.material = material;
		ground.transform.Translate(new Vector3(0.0f, -2.0f, 0.0f));
	}
/*	
	void Update()
	{
	
	}
*/
}
