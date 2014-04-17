using System.Collections.Generic;
using UnityEngine;

public class DebugLog : MonoBehaviour
{
	private static Queue<string> messageQueue;
	void Start()
	{
		messageQueue = new Queue<string>();
	}

	void OnGUI()
	{
		foreach (var message in messageQueue)
		{
			GUILayout.Label(message);
		}
	}

	static Queue<string> GetQueue()
	{
		return messageQueue;
	}

	public static void LogMessage(string message)
	{
		messageQueue.Enqueue(message);
		if (messageQueue.Count > 5)
		{
			messageQueue.Dequeue();
		}
	}
}

