using UnityEngine;

public class Ball : MonoBehaviour
{
	public Collider2D touchTrigger { get; private set; }
	void Start ()
	{
		touchTrigger = GetComponentInChildren<Collider2D>();
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.transform.tag == "Wall")
		{
			CamShake.intensity += 0.2f;
		}
	}
}

