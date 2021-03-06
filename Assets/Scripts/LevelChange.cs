using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChange : MonoBehaviour
{
    [SerializeField] string nextScene;

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			SceneManager.LoadSceneAsync(nextScene);
		}
	}
}
