using UnityEngine;

public class Destroyer : MonoBehaviour
{
	public float destructionTime = 3;
	void Start () 
	{
		Destroy(gameObject, destructionTime);
	}

}

