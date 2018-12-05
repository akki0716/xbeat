using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MusicInfo {

	/// <summary>
	/// 曲ごとに振られる連番
	/// </summary>
	[SerializeField]
	private int musicID;

	[SerializeField]
	private string musicName;

	[SerializeField]
	private string artistName;

	[SerializeField]
	public List<ScoreInfo> scoreListEasy = new List<ScoreInfo>();

	[SerializeField]
	public List<ScoreInfo> scoreListStanderd = new List<ScoreInfo>();

	[SerializeField]
	public List<ScoreInfo> scoreListHard = new List<ScoreInfo>();

	[SerializeField]
	public List<ScoreInfo> scoreListMaster = new List<ScoreInfo>();

	[SerializeField]
	public List<ScoreInfo> scoreListUnlimited = new List<ScoreInfo>();

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

	public int MusicID
	{
		get
		{
			return musicID;
		}

		set
		{
			musicID = value;
		}
	}


	/*
	//プロパティはJSON化できない&privateはJSON化できないのでSerializeFieldをつける
	[SerializeField]
	private string musicName;

	[SerializeField]
	private string artistName;

	//どのレベルに数値が入っているかに応じてどの難易度があるかを判定する
	[SerializeField]
	private double levelEasy;

	[SerializeField]
	private double levelStanderd;

	[SerializeField]
	private double levelHard;

	[SerializeField]
	private double levelMaster;

	[SerializeField]
	private double levelUnlimited;

	[SerializeField]
	private string dispBPM;

	[SerializeField]
	private string scorePathEasy;

	[SerializeField]
	private string scorePathStanderd;

	[SerializeField]
	private string scorePathHard;

	[SerializeField]
	private string scorePathMaster;

	[SerializeField]
	private string scorePathUnlimited;

	[SerializeField]
	private string soundPath;

	[SerializeField]
	private string jacketPath;

	public string ScorePath1
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



	public string SongName
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

	public string ScorePath2
	{
		get
		{
			return scorePathStanderd;
		}

		set
		{
			scorePathStanderd = value;
		}
	}

	public string ScorePath3
	{
		get
		{
			return scorePathHard;
		}

		set
		{
			scorePathHard = value;
		}
	}

	public string ScorePath4
	{
		get
		{
			return scorePathMaster;
		}

		set
		{
			scorePathMaster = value;
		}
	}

	public string ScorePath5
	{
		get
		{
			return scorePathUnlimited;
		}

		set
		{
			scorePathUnlimited = value;
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
			return levelEasy;
		}

		set
		{
			levelEasy = value;
		}
	}


	public string MaxBPM
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

	public double LevelStanderd
	{
		get
		{
			return levelStanderd;
		}

		set
		{
			levelStanderd = value;
		}
	}

	public double LevelHard
	{
		get
		{
			return levelHard;
		}

		set
		{
			levelHard = value;
		}
	}

	public double LevelMaster
	{
		get
		{
			return levelMaster;
		}

		set
		{
			levelMaster = value;
		}
	}

	public double LevelUnlimited
	{
		get
		{
			return levelUnlimited;
		}

		set
		{
			levelUnlimited = value;
		}
	}
	*/
}
