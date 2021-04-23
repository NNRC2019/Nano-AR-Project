using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

/// <summary>
/// This class handles the instantiation of the Maze object
/// </summary>
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

	/// <summary>
	/// This function instantiatiates the Maze object and calls its coroutine to generate the layout
	/// </summary>
	private void BeginGame()
	{
		mazeInstance = Instantiate(mazePrefab) as Maze;
		//mazeInstance.transform.localPosition = new Vector3(11, -3, -5);
		//mazeInstance.transform.position = FindObjectOfType<ARCameraManager>().transform.localPosition;
		StartCoroutine(mazeInstance.Generate());
		
	}

	/// <summary>
	/// Currently Unused. This function triggers a restart of mazeinstantiation and generation
	/// </summary>
	private void RestartGame()
	{
		StopAllCoroutines();
		Destroy(mazeInstance.gameObject);
		BeginGame();
	}

}
