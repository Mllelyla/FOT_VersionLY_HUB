﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BackToMenu : MonoBehaviour 
{
	[Header("Background Stuff")]
	public List<MoveCloud> cloudsToMove;
	public FadeInOutBoth titleFade;
	public FadeInOutSprite solidBGFade;
	public SpriteRenderer solidBGSprite;


	[Header("Button Fade Attributes")]
	public bool fadeBtnIn;
	public float fadeSpeed;
	private float btnWaitTimer;
	public float btnFadeInWait;

	[Header("Reset Button")]
	public Button resetBtn;
	public Image resetBtnImg;
	public TextMeshProUGUI resetBtnTMP;

	[Header("Play Button")]
	public Button playBtn;
	public Image btnImg;
	public TextMeshProUGUI btnTMP;

	[Header("To Turn Off")]
	public List<GameObject> levelButtons;
	public Image myImg;
	public Button myBtn;

	[Header("References")]
	public Hub hubScript;
	public MainMenu mainMenuScript;
	public GlowPlayAnim glowPlayAnimScript;

	[Header("Back To Menu Button")]
	public Button backToMenuBtn;
	public FadeInOutBoth backToMenuFadeScript;


	void Start () 
	{
		backToMenuBtn.onClick.AddListener(GoToMenu);

		//myImg = this.GetComponent<Image>();
		//myBtn = this.GetComponent<Button>();
	}
	


	void Update ()  
	{
		// -- Fade Menu Buttons In -- //
		if (fadeBtnIn)
		{
			btnWaitTimer += Time.deltaTime;

			if (btnWaitTimer >= btnFadeInWait) 
			{
				if (mainMenuScript.btnAlpha < 1) 
				{ 
					mainMenuScript.btnAlpha += fadeSpeed; 

					// - Play Button Fade In - //
					btnImg.color = new Color(1, 1, 1, Mathf.SmoothStep(0f, 1f, mainMenuScript.btnAlpha));
					btnTMP.color = new Color(0.03f, 0.03f, 0.03f, Mathf.SmoothStep(0f, 1f, mainMenuScript.btnAlpha));

					// - Reset Button Fade In - //
					resetBtnImg.color = new Color(1, 1, 1, Mathf.SmoothStep(0f, 1f, mainMenuScript.btnAlpha));
					resetBtnTMP.color = new Color(0.03f, 0.03f, 0.03f, Mathf.SmoothStep(0f, 1f, mainMenuScript.btnAlpha));

					if (mainMenuScript.btnAlpha >= 1)
					{
						playBtn.enabled = true;
						resetBtn.enabled = true;
						fadeBtnIn = false;
						hubScript.ResetHubSeasons();
						Debug.Log("Reseting hub seasons.");
						glowPlayAnimScript.ResetGlow(); // call this on all the glows (all unlocked seasons)
						btnWaitTimer = 0f;
						this.gameObject.SetActive(false);
					}
				}
			}
		}

		// - Only Enable Click When Fully Visible - //
		if (myImg.color.a < 0.95f) { myBtn.enabled = false; }
		else { myBtn.enabled = true; }
	}



	void GoToMenu ()
	{
		//Debug.Log("Presssing Back To Menu Button");

		// - TO TURN OFF IMMEDIATELY - //
		foreach(GameObject levelButton in levelButtons)
		{
			levelButton.SetActive(false);
		}

		backToMenuBtn.enabled = false;

		// - MAKE THE CLOUDS CLOSE - //
		foreach(MoveCloud cloud in cloudsToMove)
		{
			cloud.moveIn = true;
		}

		// - FADE OUT BACKTOMENU BTN - //
		backToMenuFadeScript.FadeOut();

		// - FADE IN TITLE - //
		titleFade.FadeIn();

		// - FADE IN SOLID BACKGROUND - //
		solidBGFade.FadeIn();

		// - FADE IN ALL BUTTONS - //
		fadeBtnIn = true;	
	}
}
