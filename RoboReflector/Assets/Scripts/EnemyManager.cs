using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
	public static List<Robot> Robots { get; private set; }
	public static List<Block> Blocks { get; private set; }
	public Robot[] roboPrefabs;
	public Block block;
	public GameObject explosion;
	public int minEnemiesPerRound = 6;
	public int maxEnemiesPerRound = 10;
	public int minBlocksPerRound = 1;
	public int maxBlocksPerRound = 4;

	void Start()
	{
		Robots = new List<Robot>();
		Blocks = new List<Block>();
		StartGame.GameStartEventHandler += FirstStart;
		
	}

	IEnumerator CheckForRobots()
	{
		while (true)
		{
			if (Robots.All(robot => robot == null) || Robots.Count == 0)
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
		foreach (var o in Blocks)
		{
			Destroy(o);
		}
		Blocks.Clear();
		var blocksToSpawn = Random.Range(minBlocksPerRound, maxBlocksPerRound);
		for (int i = 0; i < blocksToSpawn; i++)
		{
			Blocks.Add(
				(Block)
					Instantiate(block, (Vector2) transform.position + new Vector2(Random.Range(-4.5f, 4.5f), Random.Range(-6f, 6f)),
						Quaternion.Euler(0, 0, Random.Range(0, 360f))));
		}

		var enemiesToSpawn = Random.Range(minEnemiesPerRound, maxEnemiesPerRound);
		for (int i = 0; i <= enemiesToSpawn; i++)
		{
			Robots.Add((Robot)
				Instantiate(roboPrefabs[Random.Range(0, roboPrefabs.Length)],
					((Vector2)transform.position) + new Vector2(Random.Range(-4.5f, 4.5f), Random.Range(-6f, 6f)), Quaternion.identity));
			Robots[i].explosion = explosion;
			if (Robots[i])
			{
				Robots[i].transform.parent = transform;
			}
		}
	}
}

