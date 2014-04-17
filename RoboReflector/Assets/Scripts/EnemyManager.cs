using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
	private List<Robot> robots;
	public Robot roboPrefab;
	public int minEnemiesPerRound;
	public int maxEnemiesPerRound;

	void Start()
	{
		robots = new List<Robot>();
		StartGame.GameStartEventHandler += SpawnRound;
	}

	void Update()
	{

	}

	void SpawnRound()
	{
		var enemiesToSpawn = Random.Range(minEnemiesPerRound, maxEnemiesPerRound);
		for (int i = 0; i < enemiesToSpawn; i++)
		{
			robots.Add((Robot)
				Instantiate(roboPrefab, ((Vector2) transform.position) + Random.insideUnitCircle * 4, Quaternion.identity));
		}
		StartGame.GameStartEventHandler -= SpawnRound;

	}
}

