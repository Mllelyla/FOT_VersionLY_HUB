﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlideInHelpBird : MonoBehaviour 
{
	public Transform hiddenHelpBirdPos;
	public Transform shownHelpBirdPos;

	public GameObject shadow;
	public float shadowAlpha;

	public GameObject txtBubble;
	private float txtBubAlpha;
	public float txtBubFadeTime;

	public GameObject riddleBtnObj;
	private Button riddleBtn;
	private float riddleBtnAlpha;
	public float riddleBtnFadeTime;
	private bool riddleShow;
	private GameObject riddleCurntActive;

	public GameObject closeMenuOnClick;
	public GameObject blockClickingOnEggs;

	public List<GameObject> riddleHints;

	public bool moveUp = false;

	public float speed;

	private float totalDist;
	private float distLeft;
	private float distPercent;
	


	void Start ()
	{
		totalDist = Vector2.Distance(hiddenHelpBirdPos.position, shownHelpBirdPos.position);
		riddleBtn = riddleBtnObj.GetComponent<Button>();
		riddleBtn.onClick.AddListener(ShowRiddleText);
	}



	void Update ()
	{
		distLeft = Vector2.Distance(this.transform.position, shownHelpBirdPos.position);
		distPercent = (totalDist - distLeft) / totalDist;

		if (moveUp == true)
		{
			this.transform.position = Vector3.MoveTowards(this.transform.position, shownHelpBirdPos.position, speed * Time.deltaTime);

			if (shadowAlpha < 1) { shadowAlpha = distPercent; }
			shadow.GetComponent<Image>().color = new Color(1,1,1, shadowAlpha);
		}
		
		if (moveUp == false)
		{
			closeMenuOnClick.SetActive(false);
			this.transform.position = Vector3.MoveTowards(this.transform.position, hiddenHelpBirdPos.position, speed * Time.deltaTime);

			if (riddleCurntActive) { riddleCurntActive.SetActive(false); }

			if (shadowAlpha > 0) { shadowAlpha = distPercent; }
			shadow.GetComponent<Image>().color = new Color(1,1,1, shadowAlpha);
		}

		if (moveUp == false && Vector2.Distance(this.transform.position, hiddenHelpBirdPos.position) <= 0.1f)
		{
			blockClickingOnEggs.SetActive(false);
		}


		if (moveUp == true && Vector2.Distance(this.transform.position, shownHelpBirdPos.position) <= 0.1f)
		{
			blockClickingOnEggs.SetActive(true);

			if (txtBubAlpha < 1) { txtBubAlpha += Time.deltaTime * txtBubFadeTime; }
			txtBubble.GetComponent<Image>().color = new Color(1,1,1, txtBubAlpha);

			if (txtBubAlpha >= 1)
			{
				if (!riddleBtnObj.activeSelf) 
				{ 
					riddleBtnObj.SetActive(true); 
					riddleBtn.enabled = true;
				}
			}
		}
		else
		{
			if (txtBubAlpha > 0) { txtBubAlpha -= Time.deltaTime * txtBubFadeTime; }
			txtBubble.GetComponent<Image>().color = new Color(1,1,1, txtBubAlpha);
			if (riddleBtnObj.activeSelf) { riddleBtnObj.SetActive(false); }
		}


		if (riddleBtnAlpha > 0f)
		{
			riddleBtnAlpha -= Time.deltaTime * riddleBtnFadeTime;
			riddleBtnObj.GetComponent<Image>().color = new Color(1,1,1, riddleBtnAlpha);
		}
		else if (riddleShow && riddleBtnAlpha <= 0)
		{
			riddleShow = false;
			int random = Random.Range(0, riddleHints.Count);
			riddleHints[random].SetActive(true);
			riddleCurntActive = riddleHints[random];
		}

	}



	public void MoveBirdUp () 
	{
		if (moveUp == false)
		{moveUp = true;}
		else if (moveUp == true)
		{moveUp = false;}
	}



	public void ShowRiddleText ()
	{
		riddleBtnAlpha = 1.05f;
		riddleBtn.enabled = false;	
		riddleShow = true;
		closeMenuOnClick.SetActive(true);
	}
}
