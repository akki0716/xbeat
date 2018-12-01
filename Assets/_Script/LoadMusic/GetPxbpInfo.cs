using MiniJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GetPxbpInfo : MonoBehaviour {
	[SerializeField] LoadPxbp loadPxbp;
	[SerializeField] CreateMessage createMessage;

	private const string catEASY = "EASY";
	private const string catSTANDARD = "STANDARD";
	private const string catHARD = "HARD";
	private const string catMASTER = "MASTER";
	private const string catUNLIMITED = "UNLIMITED";

	enum Difficalty
	{
		EASY = 1, STANDARD, HARD, MASTER, UNLIMITED
	}
	//maxDifficaltyの判断用に難易度を数値で持っておく
	private int maxDifficalty;


	/*-----------MusicInfo用--------------*/
	public const string musicName = "musicName";
	public const string artistName = "artistName";
	public const string dispBPM = "dispBPM";
	public const string levelEasy = "levelEasy";
	public const string levelStanderd = "levelStanderd";
	public const string levelHard = "levelHard";
	public const string levelMaster = "levelMaster";
	public const string levelUnlimited = "levelUnlimited";
	public const string scorePathEasy = "scorePathEasy";
	public const string scorePathStanderd = "scorePathStanderd";
	public const string scorePathHard = "scorePathHard";
	public const string scorePathMaster = "scorePathMaster";
	public const string scorePathUnlimited = "scorePathUnlimited";


	/*-----------pxbpで定義されている内容--------------*/
	public const string musicLevel = "musicLevel";







	/*
	難易度歯抜け問題→pxbpListに入れながら、最大難易度を検出する
		
	同難易度複数譜面問題→検知したら警告しつつ上書きする
		
		*/


	public Dictionary<string , string> GetInfo ( List<string> pxbpFiles )
	{
		Dictionary<string , string> pxbpInfo = new Dictionary<string , string>();

		List<Dictionary<string , object>> pxbpList = new List<Dictionary<string , object>>();
		//maxDificaltyint = Difficalty;

		for (int i = 0 ; i < pxbpFiles.Count ; i++)
		{
			string pxbp = loadPxbp.PxbpLoad(pxbpFiles[i]);

			Dictionary<string , object> pxbpjson =  Json.Deserialize(pxbp) as Dictionary<string , object>;
			maxDifficalty = GetMaxDifficalty((string)pxbpjson["category"]);
			pxbpList.Add(pxbpjson);
		}
		
		//pxbpInfo.Add("musicName" , json["musicName"].Get<string>());

		//最初に最大難易度かどうか判定とかしてそれでもって情報を詰めていく
		/*
		最大難易度か判定する
		その難易度のキーが入っていたらエラーを出しつつ上書きする
		
		*/
		
		foreach (var pxbp in pxbpList)
		{
			switch (DifficaltyToInt((string)pxbp["category"]))
			{
				case (int)Difficalty.EASY:
					pxbpInfo.Add(levelEasy , (string)pxbp[musicLevel]);
					pxbpInfo.Add(scorePathEasy,"a");

					break;
				case (int)Difficalty.STANDARD:
					break;
				case (int)Difficalty.HARD:
					break;
				case (int)Difficalty.MASTER:
					break;
				case (int)Difficalty.UNLIMITED:
					break;
				default:
					break;
			}



			//最大難易度だったら
			if (JudgeMaxDifficalty((string)pxbp["category"]))
			{
				//既にmusicNameが入っている(=既に最大難易度の譜面がある)
				if (pxbpInfo.ContainsKey(musicName))
				{
					//createMessage.CreateErrMsg(CreateMessage.Errcode.multipleDifficalty , (string)pxbp[musicName]);
					pxbpInfo.Remove(musicName);
					pxbpInfo.Remove(artistName);
				}
				pxbpInfo.Add(musicName , (string)pxbp[musicName]);
				pxbpInfo.Add(artistName , (string)pxbp[artistName]);
				pxbpInfo.Add(dispBPM , GetDispBPM((IList)pxbp["bpmList"]));
			}

			
			
		}

	











		return pxbpInfo;
	}


	private string ToUpper (string str )
	{
		return str.ToUpper();
	}


	private int DifficaltyToInt (string category)
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




	private int GetMaxDifficalty (string category)
	{
		//強制的に大文字変換
		category = ToUpper(category);

		//難易度を数字に変換
		int difficalty = DifficaltyToInt(category);

		//数字で判断
		if (difficalty > maxDifficalty)
		{
			return difficalty;
		}
		return maxDifficalty;
	}


	private bool JudgeMaxDifficalty (string category )
	{
		category = ToUpper(category);

		if (maxDifficalty == DifficaltyToInt(category))
		{
			return true;
		}
		return false;
	}


	private string GetDispBPM (IList bpmList)
	{
		string dispBPM = null;
		double maxBPM = -1;
		double minBPM = -1;
		List<double> bpmList2 = new List<double>();
		foreach (var item in bpmList)
		{
			IDictionary<string , object> bpmItem = (IDictionary<string , object>)item;
			bpmList2.Add((double)bpmItem["value"]);
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



}
