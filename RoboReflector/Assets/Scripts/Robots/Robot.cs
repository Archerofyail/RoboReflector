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
	public float minShotCheckFrequency = 2f;
	public float maxShotCheckFrequency = 6f;
	public float shotCheckFrequency;
	public int objectsInPool = 4;
	protected int hitsToTake;
	private readonly Vector2 relativeLaunchPos = new Vector2(0.35f, -0.44f);
	[SerializeField]
	private UILabel healthLabel;
	private SpriteRenderer sprite;

	private GameObject[] lasers;

	void Start()
	{
		sprite = GetComponent<SpriteRenderer>();
		sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0);
		StartCoroutine(FadeInSprite());
		shotCheckFrequency = Random.Range(minShotCheckFrequency, maxShotCheckFrequency);
		lasers = new GameObject[objectsInPool];
		for (int i = 0; i < lasers.Length; i++)
		{
			lasers[i] = (GameObject)Instantiate(laserShot, transform.position, Quaternion.identity);
			lasers[i].SetActive(false);
		}
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
		EnemyManager.RemoveRobot(this);
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
			//Log.LogMessage("Robot Destroyed");
			OnDeath();
			Destroy(gameObject, 0.01f);
		}
		else
		{
			SetHealth();
		}
		CamShake.intensity += 0.2f;
	}

	protected virtual void OnDeath() { }

	void StartFiring()
	{
		StartCoroutine("FireBullet");
	}

	IEnumerator FadeInSprite()
	{
		var spriteColor = sprite.color;
		float timer = 0.2f;
		var alphaVal = 0f;
		var startPos = transform.position;
		while (timer > 0)
		{

			timer -= Time.deltaTime;
			timer = Mathf.Clamp01(timer);
			sprite.color = new Color(spriteColor.r, spriteColor.g, spriteColor.b,
				Mathf.Lerp(0, 1, Mathf.InverseLerp(0.2f, 0, timer)));
			transform.position = startPos + (new Vector3(0, Mathf.Lerp(0.7f, 0, Mathf.InverseLerp(0.2f, 0, timer))));
			yield return null;
		}
	}

	IEnumerator FireBullet()
	{
		while (true)
		{
			if (!BallManager.IsLaunching)
			{
				if (Random.Range(0, 101) <= 25)
				{
					var activeLaser = lasers.FirstOrDefault(laser => !laser.activeSelf);
					if (activeLaser)
					{
						activeLaser.SetActive(true);
						activeLaser.transform.position = transform.TransformPoint(relativeLaunchPos);
						activeLaser.transform.rotation = Quaternion.identity;
						activeLaser.GetComponent<Rigidbody2D>().AddForce(-Vector2.up * 150);
					}
				}
			}
			yield return new WaitForSeconds(shotCheckFrequency);
		}
	}

	private void SetHealth()
	{
		healthLabel.text = hitsToTake.ToString(CultureInfo.InvariantCulture);
	}
}

