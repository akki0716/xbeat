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
	[SerializeField] ButtonExtention buttonExtention;//長押しボタン

	bool isMoveScene = true;

	// Use this for initialization
	private void Start () {
		StartProcess();
	}
	

	private void StartProcess ()
	{
		createMusicList.CreateList();

		buttonExtention.onLongPress.AddListener(() => StopMoveScene());
		//StartCoroutine(createMessage.CountdownCoroutine(MoveScene));
		
	}

	private void MoveScene ()
	{
		if (isMoveScene)
		{
			SceneManager.LoadScene("SelectMusic");
		}
	}



	private void StopMoveScene ()
	{
		isMoveScene = false;
		//createMessage.CreateMsg(CreateMessage.Msgcode.moveCancel);
	}
	
}
