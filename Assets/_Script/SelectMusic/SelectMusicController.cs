using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectMusicController : MonoBehaviour {
	[SerializeField] LoadMusicDB loadMusicDB;


	// Use this for initialization
	void Start () {
		StartProcess();
	}
	
	private void StartProcess ()
	{
		loadMusicDB.MusicDBLoad();
	}

}
