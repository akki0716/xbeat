using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ScoreInfo
{
	//プロパティはJSON化できない&privateはJSON化できないのでSerializeFieldをつける
	[SerializeField]
	private string musicName;

	[SerializeField]
	private string artistName;

	[SerializeField]
	private string difficalty;

	[SerializeField]
	private double level;

	[SerializeField]
	private string dispBPM;

	[SerializeField]
	private string scorePathEasy;

	[SerializeField]
	private string soundPath;

	[SerializeField]
	private string jacketPath;

	public string MusicName
	{
		get
		{
			return musicName;
		}

		set
		{
			musicName = value;
		}
	}

	public string ArtistName
	{
		get
		{
			return artistName;
		}

		set
		{
			artistName = value;
		}
	}

	public double Level
	{
		get
		{
			return level;
		}

		set
		{
			level = value;
		}
	}

	public string DispBPM
	{
		get
		{
			return dispBPM;
		}

		set
		{
			dispBPM = value;
		}
	}

	public string ScorePathEasy
	{
		get
		{
			return scorePathEasy;
		}

		set
		{
			scorePathEasy = value;
		}
	}

	public string Difficalty
	{
		get
		{
			return difficalty;
		}

		set
		{
			difficalty = value;
		}
	}

	public string SoundPath
	{
		get
		{
			return soundPath;
		}

		set
		{
			soundPath = value;
		}
	}

	public string JacketPath
	{
		get
		{
			return jacketPath;
		}

		set
		{
			jacketPath = value;
		}
	}
}
