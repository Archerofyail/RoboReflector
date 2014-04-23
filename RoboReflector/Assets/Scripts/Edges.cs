using UnityEngine;

public class Edges : MonoBehaviour 
{

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.transform.tag == "Laser")
		{
			other.gameObject.SetActive(false);
		}
		if (other.transform.tag == "Ball")
		{
			ScoreManager.ScoreMultiplier -= 0.5f;
		}
	}
}

