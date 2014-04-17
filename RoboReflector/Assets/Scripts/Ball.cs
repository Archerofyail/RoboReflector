using UnityEngine;

public class Ball : MonoBehaviour 
{

	void Start () 
	{
	
	}
	
	void Update () 
	{
	
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.transform.tag == "Wall")
		{
			CamShake.intensity += 0.2f;
		}
	}
}

