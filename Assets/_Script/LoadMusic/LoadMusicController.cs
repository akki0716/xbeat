using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//このシーンの開始時処理、他シーンへの遷移を担当
public class LoadMusicController : MonoBehaviour {
	//[SerializeField] ConstDef constDef;
	//[SerializeField] LoadUtil loadUtil;
	//[SerializeField] TextLoad textLoad;
	[SerializeField] CreateMusicList createMusicList;
	[SerializeField] CreateMessage createMessage;
	[SerializeField] DispMessage dispMessage;

	[SerializeField] float waitTime;

	bool loadComplete = false;
	bool allowTransition = true;
	float span = 0;
	
	private void Start () {
		StartProcess();
	}
	

	private void StartProcess ()
	{
		createMusicList.CreateList();
		Debug.Log("Complete");
		loadComplete = true;
	}


	private void Update ()
	{
		if (loadComplete)
		{
			waitTime -= Time.deltaTime;
			span += Time.deltaTime;
			if (span >= 1 && allowTransition)
			{
				dispMessage.ShowMesseage(1 , Math.Round(waitTime) + "秒後に選曲画面に遷移します。\n");
				span = 0;
			}
			if (waitTime < -1)
			{
				Transition();
			}
		}
	}


	private void Transition ()
	{
		if (allowTransition)
		{
			SceneManager.LoadScene("SelectMusic");
		}
	}



	public void OnOK ()
	{
		if (loadComplete)
		{
			allowTransition = true;
			Transition();
		}
	}

	public void OnCancel ()
	{
		allowTransition = false;
	}
}
