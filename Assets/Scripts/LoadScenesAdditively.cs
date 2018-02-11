using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScenesAdditively : MonoBehaviour 
{
	public int[] scenesToLoad;

	private void Start()
	{
		foreach (int index in scenesToLoad)
		{
			print (SceneManager.sceneCount);
			Scene scene = SceneManager.GetSceneByBuildIndex(index);
			int numScenes = SceneManager.sceneCount;

			if (scene != null && !scene.isLoaded)
			{
				SceneManager.LoadScene("Resources/Backdrop.scene");
			}
		}
	} 
}
