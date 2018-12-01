using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CreateJSON : MonoBehaviour {
	[SerializeField] LoadUtil loadUtil;

	public void CreateJson ( List<GenreInfo> genres )
	{
		AllGenre allGenre = new AllGenre
		{
			Genres = genres
		};
		string json = JsonUtility.ToJson(allGenre);

		string filePath = loadUtil.GetFilePath(LoadUtil.FolderType.System);
		filePath += loadUtil.musicDBfile;

		File.WriteAllText(filePath , json);
	}
}
