using MiniJSON;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using static CreateMusicList;
using static CreateMessage;

public class GetMusicInfo : MonoBehaviour {

	[SerializeField] CreateMessage createMessage;
	[SerializeField] LoadPxbp loadPxbp;

	public int MusicID { get; set; } = 1;

	private const string catEASY = "EASY";
	private const string catSTANDARD = "STANDARD";
	private const string catHARD = "HARD";
	private const string catMASTER = "MASTER";
	private const string catUNLIMITED = "UNLIMITED";

	/*----------------pxbpの各キー定義------------------*/
	public const string musicName = "musicName";
	public const string artistName = "artistName";
	public const string dispBPM = "dispBPM";
	public const string category = "category";
	public const string musicLevel = "musicLevel";
	public const string bpmList = "bpmList";
	/*----------------その他のキー定義------------------*/
	public const string scorePath = "scorePath";
	public const string musicPath = "musicPath";
	public const string JacketPath = "JacketPath";

	enum Difficalty
	{
		EASY = 1, STANDARD, HARD, MASTER, UNLIMITED
	}
	//maxDifficaltyの判断用に難易度を数値で持っておく
	private int maxDifficalty;

	//フォルダ名と同名の音源ファイル、ジャケットファイル
	private string baseMusicPath;
	private string baseJacketPath;

	public MusicInfo GetInfo ( FileInfo[] musicFolderFiles, string musicFolderName)
	{
		MusicInfo musicInfo = new MusicInfo();

		Dictionary<string , string> ResourcePath = GetResourcePath(musicFolderFiles , musicFolderName);

		List<Dictionary<string , object>> pxbpList = new List<Dictionary<string , object>>();

		pxbpList = GetPxbpList(musicFolderFiles , musicFolderName);

		foreach (var pxbp in pxbpList)
		{
			ScoreInfo scoreInfo = new ScoreInfo();

			if (pxbp.ContainsKey(musicName))
			{
				scoreInfo.MusicName = (string)pxbp[musicName];
			}

			if (pxbp.ContainsKey(artistName))
			{
				scoreInfo.ArtistName = (string)pxbp[artistName];
			}

			if (pxbp.ContainsKey(category))
			{
				string difficalty = (string)pxbp[category];
				scoreInfo.Difficalty = difficalty.ToUpper();
			}

			if (pxbp.ContainsKey(musicLevel))
			{
				string level = (string)pxbp[musicLevel];
				scoreInfo.Level = double.Parse(level);
			}

			if (pxbp.ContainsKey(bpmList))
			{
				scoreInfo.DispBPM = GetDispBPM((IList)pxbp[bpmList]);
			}

			scoreInfo.ScorePath = (string)pxbp[scorePath];

			if (pxbp.ContainsKey(musicPath))
			{
				scoreInfo.MusicPath = (string)pxbp[musicPath];
			}
			else
			{
				scoreInfo.MusicPath = ResourcePath["MusicPath"];
			}

			if (pxbp.ContainsKey(JacketPath))
			{
				scoreInfo.JacketPath = (string)pxbp[musicPath];
			}
			else
			{
				if (ResourcePath.ContainsKey("JacketPath"))
				{
					scoreInfo.JacketPath = ResourcePath["JacketPath"];
				}
				else
				{
					scoreInfo.JacketPath = "-1";
				}
			}

			//曲名と同名の譜面ファイルならそれを代表として曲名とアーティスト名を拾う
			if (pxbp.ContainsKey("dispMusicName"))
			{
				musicInfo.MusicName = (string)pxbp["dispMusicName"];
			}
			if (pxbp.ContainsKey("dispArtistName"))
			{
				musicInfo.ArtistName = (string)pxbp["dispArtistName"];
			}


			AddMusicInfo(scoreInfo , (string)pxbp[category] , ref musicInfo);

		}

		if (musicInfo.MusicName == null)
		{
			createMessage.CreateMsg((int)MsgCodeError.noMainScore , musicFolderName);
		}

		musicInfo.MusicID = MusicID;
		MusicID++;

		return musicInfo;
	}


	private List<Dictionary<string , object>> GetPxbpList ( FileInfo[] musicFolderFiles , string musicFolderName)
	{
		List<Dictionary<string , object>> pxbpList = new List<Dictionary<string , object>>();

		//曲フォルダ内のpxbpをすべて取得
		for (int i = 0 ; i < musicFolderFiles.Length ; i++)
		{
			if (Path.GetExtension(musicFolderFiles[i].FullName) == ext_pxbp)
			{
				string pxbpJson = loadPxbp.PxbpLoad(musicFolderFiles[i].FullName);
				Dictionary<string , object> pxbp = Json.Deserialize(pxbpJson) as Dictionary<string , object>;
				maxDifficalty = GetMaxDifficalty((string)pxbp[category]);
				pxbp.Add(scorePath , musicFolderFiles[i].FullName);

				//曲名と同名の譜面ファイルならそれを代表として曲名とアーティスト名を拾う
				string fileName = Path.GetFileNameWithoutExtension(musicFolderFiles[i].FullName);
				if (fileName == musicFolderName)
				{
					pxbp.Add("dispMusicName" , (string)pxbp[musicName]);
					pxbp.Add("dispArtistName" , (string)pxbp[artistName]);
				}
				pxbpList.Add(pxbp);
			}
		}

		return pxbpList;
	}


	private string GetDispBPM ( IList bpmList )
	{
		string dispBPM = null;
		double maxBPM = -1;
		double minBPM = -1;
		List<double> bpmList2 = new List<double>();
		foreach (var item in bpmList)
		{
			IDictionary<string , object> bpmItem = (IDictionary<string , object>)item;
			object test = bpmItem["value"];
			string value = test.ToString();
			bpmList2.Add(double.Parse(value));
			Debug.Log(value);
		}
		maxBPM = bpmList2.Max();
		minBPM = bpmList2.Min();

		if (maxBPM == minBPM)
		{
			dispBPM = maxBPM.ToString();
		}
		else
		{
			dispBPM = minBPM.ToString() + " - " + maxBPM.ToString();
		}

		return dispBPM;
	}



	/// <summary>
	/// 基本(フォルダ名と同一)の音源ファイル、ジャケットファイルのパスを返す
	/// </summary>
	/// <param name="musicFolderFiles"></param>
	/// <param name="musicName"></param>
	/// <returns></returns>
	private Dictionary<string , string> GetResourcePath ( FileInfo[] musicFolderFiles, string musicName )
	{
		Dictionary<string , string> ResourcePath = new Dictionary<string, string>();
		for (int i = 0 ; i < musicFolderFiles.Length ; i++)
		{
			string ext = Path.GetExtension(musicFolderFiles[i].FullName);
			if (ext == ext_mp3 || ext == ext_ogg || ext == ext_wav)
			{
				string fileName = Path.GetFileNameWithoutExtension(musicFolderFiles[i].FullName);
				if (fileName == musicName )
				{
					if (ResourcePath.ContainsKey("MusicPath"))
					{
						createMessage.CreateMsg((int)MsgCodeError.duplicateMusic, musicName);
					}
					else
					{
						ResourcePath.Add("MusicPath" , musicFolderFiles[i].FullName);
					}
				}
			}
			else if (ext == ext_jpeg || ext == ext_jpg || ext == ext_png)
			{
				string fileName = Path.GetFileNameWithoutExtension(musicFolderFiles[i].FullName);
				if (fileName == musicName)
				{
					if (ResourcePath.ContainsKey("JacketPath"))
					{
						createMessage.CreateMsg((int)MsgCodeError.duplicateJacket, musicName);
					}
					else
					{
						ResourcePath.Add("JacketPath" , musicFolderFiles[i].FullName);
					}
				}
			}
		}
		return ResourcePath;
	}


	private int GetMaxDifficalty ( string category )
	{
		//強制的に大文字変換
		category = category.ToUpper();

		//難易度を数字に変換
		int difficalty = DifficaltyToInt(category);

		//数字で判断
		if (difficalty > maxDifficalty)
		{
			return difficalty;
		}
		return maxDifficalty;
	}


	private int DifficaltyToInt ( string category )
	{
		switch (category)
		{
			case catEASY:
				return (int)Difficalty.EASY;
			case catSTANDARD:
				return (int)Difficalty.STANDARD;
			case catHARD:
				return (int)Difficalty.HARD;
			case catMASTER:
				return (int)Difficalty.MASTER;
			case catUNLIMITED:
				return (int)Difficalty.UNLIMITED;
			default:
				return 0;
		}
	}

	//refによってGetInfoのmusicInfoにアクセスしている
	private void AddMusicInfo (ScoreInfo scoreInfo , string difficalty , ref MusicInfo musicInfo)
	{
		difficalty = difficalty.ToUpper();
		switch (difficalty)
		{
			case catEASY:
				musicInfo.scoreListEasy.Add(scoreInfo);
				break;
			case catSTANDARD:
				musicInfo.scoreListStanderd.Add(scoreInfo);
				break;
			case catHARD:
				musicInfo.scoreListHard.Add(scoreInfo);
				break;
			case catMASTER:
				musicInfo.scoreListMaster.Add(scoreInfo);
				break;
			case catUNLIMITED:
				musicInfo.scoreListUnlimited.Add(scoreInfo);
				break;
			default:
				return;
		}
	}
}
