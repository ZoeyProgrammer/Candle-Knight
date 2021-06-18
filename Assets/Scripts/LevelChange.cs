using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChange : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			CharacterMovement player = other.gameObject.GetComponent<CharacterMovement>();
			if (player != null && player.isLit)
			{
				GameManger.currentLevel++;
				LevelController controller = FindObjectOfType<LevelController>();
				if (controller != null)
					controller.LoadLevel();
			}
			else
			{
				//Whatever should happen when the flame is not LIT here
			}
		}
	}
}
