using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
	private List<Robot> robots;
	public Robot[] roboPrefabs;
	public GameObject explosion;
	public int minEnemiesPerRound;
	public int maxEnemiesPerRound;

	void Start()
	{
		robots = new List<Robot>();
		StartGame.GameStartEventHandler += SpawnRound;
		StartCoroutine(CheckForRobots());
	}

	IEnumerator CheckForRobots()
	{
		while (true)
		{
			if (robots.All(robot => robot == null))
			{
				SpawnRound();
			}
			yield return new WaitForSeconds(0.1f);
		}
	}

	void SpawnRound()
	{
		var enemiesToSpawn = Random.Range(minEnemiesPerRound, maxEnemiesPerRound);
		for (int i = 0; i <= enemiesToSpawn; i++)
		{
			robots.Add((Robot)
				Instantiate(roboPrefabs[Random.Range(0, roboPrefabs.Length)],
					((Vector2) transform.position) + Random.insideUnitCircle * 4, Quaternion.identity));
			robots[i].explosion = explosion;
		}
		StartGame.GameStartEventHandler -= SpawnRound;

	}
}

