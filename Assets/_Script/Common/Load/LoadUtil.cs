using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

//ファイルをロード時に共通的に使用する処理を担当
public class LoadUtil : MonoBehaviour
{
	public enum FolderType
	{
		Songs, SongsBase, System
	}

	public readonly string sysDirName = "System";
	public readonly string SongsDirName = "Songs";
	public readonly string jpopDirName = "J-POP";
	public readonly string animeDirName = "ANIME";
	public readonly string varietyDirName = "VARIETY";
	public readonly string cbDirName = "CROSSBEATS";
	public readonly string revDirName = "REV";

	public readonly string musicDBfile = "MusicDB.json";


	/// <summary>
	/// 指定したTypeに応じたファイルパスを返す。パスは/付きで返却する。
	/// </summary>
	/// <param name="folderType">LoadUtil.FolderType</param>
	/// <param name="folderName">フォルダ名、省略可</param>
	/// <param name="fileName">ファイル名、省略可</param>
	/// <returns></returns>
	public string GetFilePath ( FolderType folderType , string folderName = null, string fileName = null)
	{
		string basePath = GetBasePath();
		string filePath = null;

		if (folderType == FolderType.Songs)//譜面、音源用(直接指定)
		{
			filePath = basePath + "/" + SongsDirName + "/" + folderName + "/" + fileName;
		}
		else if (folderType == FolderType.SongsBase)//曲フォルダ基本パス
		{
			filePath = basePath + "/" + SongsDirName + "/";
		}
		else if (folderType == FolderType.System)//設定ファイル,曲リストjson用
		{
			filePath = basePath + "/" + sysDirName + "/" + fileName;
		}

		Debug.Log(filePath);

		return filePath;

	}


	/// <summary>
	/// 機種ごとの基本的なパスを返す
	/// </summary>
	/// <returns></returns>
	private string GetBasePath ()
	{
		string filepath = null;

		switch (Application.platform)
		{
			case RuntimePlatform.OSXPlayer:
				break;
			case RuntimePlatform.WindowsPlayer:
				filepath = Application.dataPath; //exe以下のhoge_Dataを返す
				filepath += "/..";//1階層上がる
				break;
			case RuntimePlatform.IPhonePlayer:
				filepath = Application.persistentDataPath;
				break;
			case RuntimePlatform.WindowsEditor:
				filepath = Application.dataPath;
				filepath += "/../ファイル";//1階層上がる
															 //debug_text.GetComponent<Text>().text = filepath;
				break;
			case RuntimePlatform.Android:
				using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
				{
					using (AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
					{
						using (AndroidJavaObject externalFilesDir = currentActivity.Call<AndroidJavaObject>("getExternalFilesDir" , null))
						{
							filepath = externalFilesDir.Call<string>("getCanonicalPath");
							//Debug.Log("filepath " + filepath);
							//基本的に内部SD/Android/data/com.xbeats(指定してるPackage Name)/が返ってくる
						}
					}
				}
				break;

			default:
				break;
		}

		return filepath;
	}

}
