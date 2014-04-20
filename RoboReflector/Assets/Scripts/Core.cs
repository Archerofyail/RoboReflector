using UnityEngine;

public class Core : MonoBehaviour
{
	public GameObject firstExplosion;
	public GameObject gameOverMenu;
	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.transform.tag == "Laser")
		{
			if (firstExplosion)
			{
				firstExplosion.SetActive(true);
				Invoke("GameOver", 1f);
			}
		}
	}

	public void GameOver()
	{
		gameOverMenu.SetActive(true);
	}
}

