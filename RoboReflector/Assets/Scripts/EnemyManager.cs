using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
	private List<Robot> robots;
	private List<GameObject> blocks;
	public Robot[] roboPrefabs;
	public GameObject block;
	public GameObject explosion;
	public int minEnemiesPerRound = 6;
	public int maxEnemiesPerRound = 10;
	public int minBlocksPerRound = 1;
	public int maxBlocksPerRound = 4;

	void Start()
	{
		robots = new List<Robot>();
		blocks = new List<GameObject>();
		StartGame.GameStartEventHandler += FirstStart;
		
	}

	IEnumerator CheckForRobots()
	{
		while (true)
		{
			if (robots.All(robot => robot == null) || robots.Count == 0)
			{
				SpawnRound();
				BallManager.IncreaseBallCount();
			}
			yield return new WaitForSeconds(0.1f);
		}
	}

	void FirstStart()
	{
		StartCoroutine(CheckForRobots());
		StartGame.GameStartEventHandler -= FirstStart;
	}

	private void SpawnRound()
	{
		foreach (var o in blocks)
		{
			Destroy(o);
		}
		blocks.Clear();
		var blocksToSpawn = Random.Range(minBlocksPerRound, maxBlocksPerRound);
		for (int i = 0; i < blocksToSpawn; i++)
		{
			blocks.Add((GameObject)Instantiate(block, (Vector2)transform.position + new Vector2(Random.Range(-4.5f, 4.5f), Random.Range(-6f, 6f)), Quaternion.Euler(0, 0, Random.Range(0, 360f))));
		}

		var enemiesToSpawn = Random.Range(minEnemiesPerRound, maxEnemiesPerRound);
		for (int i = 0; i <= enemiesToSpawn; i++)
		{
			robots.Add((Robot)
				Instantiate(roboPrefabs[Random.Range(0, roboPrefabs.Length)],
					((Vector2)transform.position) + new Vector2(Random.Range(-4.5f, 4.5f), Random.Range(-6f, 6f)), Quaternion.identity));
			robots[i].explosion = explosion;
		}
	}
}

