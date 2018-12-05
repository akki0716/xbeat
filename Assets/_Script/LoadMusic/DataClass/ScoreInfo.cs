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
	private string scorePath;

	[SerializeField]
	private string musicPath;

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

	public string MusicPath
	{
		get
		{
			return musicPath;
		}

		set
		{
			musicPath = value;
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

	public string ScorePath
	{
		get
		{
			return scorePath;
		}

		set
		{
			scorePath = value;
		}
	}
}
