using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoadMusicDB : MonoBehaviour {
	[SerializeField] LoadUtil loadUtil;
	[SerializeField] LoadText loadText;
	[SerializeField] AllGenre allGenre;

	public void MusicDBLoad ()
	{
		string json = GetJson();
		AllGenre allGenre = JsonUtility.FromJson<AllGenre>(json);
	}



	private string GetJson ()
	{
		string filePath = loadUtil.GetFilePath(LoadUtil.FolderType.System,null,loadUtil.musicDBfile);
		string[] json = loadText.TextLoad(filePath);
		if (json.Length == 1)
		{
			return json[0];
		}
		else
		{
			string margeJson = loadText.MargeText(json,true);
			return margeJson;
		}
	}


	
}
