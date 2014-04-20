using System.Collections;
using System.Globalization;
using System.Linq;
using UnityEngine;

public class Robot : MonoBehaviour
{
	public int minHitsToTake = 1;
	public int maxHitsToTake = 4;
	public int scoreWorth = 200;
	
	public GameObject explosion;
	public GameObject laserShot;
	public float shotCheckFrequency = 4f;
	public int objectsInPool = 4;
	protected int hitsToTake;
	protected SpriteRenderer spriteRenderer;
	private readonly Vector2 relativeLaunchPos = new Vector2(0.35f, -0.44f);
	public UILabel healthLabel;

	private GameObject[] lasers;

	void Start()
	{
		lasers = new GameObject[objectsInPool];
		for (int i = 0; i < lasers.Length; i++)
		{
			lasers[i] = (GameObject)Instantiate(laserShot, transform.position, Quaternion.identity);
			lasers[i].SetActive(false);
		}
		healthLabel = GetComponentInChildren<UILabel>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		hitsToTake = Random.Range(minHitsToTake, maxHitsToTake);
		SetHealth();
		Invoke("StartFiring", Random.Range(0f, 5f));
		
	}

	void OnDestroy()
	{
		foreach (var laser in lasers)
		{
			Destroy(laser);
		}
		BallManager.OnBallResetEventHandler -= OnBallReset;
	}

	void OnBallReset(int newCount)
	{
		foreach (var laser in lasers)
		{
			laser.SetActive(false);
		}
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.transform.tag == "Ball")
		{
			HitByBall(other);
		}
	}

	protected virtual void HitByBall(Collision2D other)
	{
		hitsToTake--;
		ScoreManager.IncreaseScore(scoreWorth);
		if (hitsToTake <= 0)
		{
			ScoreManager.IncreaseScore(scoreWorth);
			if (!explosion)
			{
				explosion = (GameObject)Resources.Load("Explosion");
			}
			other.rigidbody.AddForce(other.contacts[0].normal.normalized * 3);
			Instantiate(explosion, transform.position, Quaternion.identity);
			Log.LogMessage("Robot Destroyed");
			OnDeath();
			Destroy(gameObject, 0.01f);
		}
		else
		{
			SetHealth();
		}
		CamShake.intensity += 0.2f;
	}

	protected virtual void OnDeath(){}

	void StartFiring()
	{
		StartCoroutine("FireBullet");
	}

	IEnumerator FireBullet()
	{
		float timer = 0;
		while (true)
		{
			if (!BallManager.IsLaunching)
			{
				timer += Time.deltaTime;
				if (((int) timer) % ((int) shotCheckFrequency) == 0)
				{
					if (Random.Range(0, 101) <= 1)
					{
						var activeLaser = lasers.FirstOrDefault(laser => !laser.activeSelf);
						if (activeLaser)
						{
							activeLaser.SetActive(true);
							activeLaser.transform.position = transform.TransformPoint(relativeLaunchPos);
							activeLaser.transform.rotation = Quaternion.identity;
							activeLaser.rigidbody2D.AddForce(-Vector2.up * 150);
						}
					}
				}
			}
			yield return null;
		}
	}

	private void SetHealth()
	{
		healthLabel.text = hitsToTake.ToString(CultureInfo.InvariantCulture);
	}
}

