using UnityEngine;

public class Destroyer : MonoBehaviour
{
	public float destructionTime = 3;
	protected virtual void Start () 
	{
		Destroy(gameObject, destructionTime);
	}

}

