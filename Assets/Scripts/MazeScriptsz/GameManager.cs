using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class GameManager : MonoBehaviour
{
	[SerializeField] Maze mazePrefab;
	private Maze mazeInstance;

	private void Start()
	{
		BeginGame();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			RestartGame();
		}
	}

	private void BeginGame()
	{
		mazeInstance = Instantiate(mazePrefab) as Maze;
		//mazeInstance.transform.localPosition = new Vector3(11, -3, -5);
		//mazeInstance.transform.position = FindObjectOfType<ARCameraManager>().transform.localPosition;
		StartCoroutine(mazeInstance.Generate());
		
	}

	private void RestartGame()
	{
		StopAllCoroutines();
		Destroy(mazeInstance.gameObject);
		BeginGame();
	}

}
