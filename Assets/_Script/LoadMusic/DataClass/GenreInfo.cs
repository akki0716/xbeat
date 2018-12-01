using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ジャンル情報を持つ
[Serializable]
public class GenreInfo  {

	[SerializeField]
	private string name;

	[SerializeField]
	private List<MusicInfo> songs;

	public string Name
	{
		get
		{
			return this.name;
		}

		set
		{
			this.name = value;
		}
	}

	public List<MusicInfo> Songs
	{
		get
		{
			return this.songs;
		}

		set
		{
			this.songs = value;
		}
	}
}
