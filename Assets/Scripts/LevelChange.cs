using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChange : MonoBehaviour
{
    [SerializeField] public string nextScene;

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			CharacterMovement player = other.gameObject.GetComponent<CharacterMovement>();
			if (player != null && player.isLit)
			{
				SceneManager.LoadSceneAsync(nextScene);
			}
			else
			{
				//Whatever should happen when the flame is not LIT here
			}
		}
	}
}
