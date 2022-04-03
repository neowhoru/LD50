using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenController : MonoBehaviour
{

	[SerializeField] private RectTransform fader;
	// Update is called once per frame

	private void Start()
	{
		Invoke(nameof(FadeMenuIn), 0.5f);
		
	}

	public void FadeMenuIn()
	{
		LeanTween.scale(fader, new Vector3(1,1,1), 0f);
		LeanTween.scale(fader, Vector3.zero, 0.5f).setEaseInOutQuad() .setOnComplete(() =>
		{
			fader.gameObject.SetActive(false);
		});
	}

	void Update () {
		if (Input.GetKeyDown(KeyCode.Space) || Input.touchCount > 0)
        {
	        fader.gameObject.SetActive(true);
            LeanTween.scale(fader, Vector3.zero, 0f);
            LeanTween.scale(fader, new Vector3(1,1, 1), 0.5f).setEaseInOutQuad().setOnComplete(LoadGame);
        }
    }

	public void LoadGame()
	{ 
		SceneManager.LoadScene("Level1");
	}
}
