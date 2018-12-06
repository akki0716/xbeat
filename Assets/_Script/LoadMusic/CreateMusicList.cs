using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static CreateMessage;

public class CreateMusicList : MonoBehaviour {
	[SerializeField] LoadUtil loadUtil;
	[SerializeField] CreateMessage createMessage;
	[SerializeField] DirCheck dirCheck;
	[SerializeField] CreateJSON createJSON;
	[SerializeField] GetMusicInfo getMusicInfo;


	public const string ext_mp3 = ".mp3";
	public const string ext_ogg = ".ogg";
	public const string ext_wav = ".wav";
	public const string ext_jpeg = ".jpeg";
	public const string ext_jpg = ".jpg";
	public const string ext_png = ".png";
	public const string ext_pxbp = ".pxbp";

	public const string keySound = "SOUND";
	public const string keyScore = "SCORE";
	public const string keyJacket = "JACKET";

	[SerializeField] private List<GenreInfo> genres = new List<GenreInfo>();


	public void CreateList ()
	{
		createMessage.CreateMsg((int)MsgCodeInfo.loadStart);

		string basepath = loadUtil.GetFilePath(LoadUtil.FolderType.SongsBase);
		string syspath = loadUtil.GetFilePath(LoadUtil.FolderType.System);

		//フォルダを作ったら再起動を促す
		if (dirCheck.CheckDir(basepath , syspath)) {
			return;
		}

		DirectoryInfo baseDir = new DirectoryInfo(basepath);

		//ジャンルフォルダ一覧を取得
		DirectoryInfo[] genreFolderInfo = baseDir.GetDirectories("*" , SearchOption.TopDirectoryOnly);

		//ジャンルごとに曲を探しに行く
		for (int i = 0 ; i < genreFolderInfo.Length ; i++)
		{
			MakeGenres(genreFolderInfo[i]);
		}

		//json生成
		createJSON.CreateJson(genres);
	}


	/// <summary>
	/// ジャンルごとの曲データリストを生成
	/// </summary>
	/// <param name="genreFolderInfo"></param>
	private void MakeGenres ( DirectoryInfo genreFolderInfo )
	{
		string m = "ジャンル:" + genreFolderInfo.Name;
		createMessage.CreateMsg((int)MsgCodeInfo.serching , m);

		//各ジャンルごとの曲フォルダ一覧を取得
		DirectoryInfo[] musicFolderInfo = genreFolderInfo.GetDirectories("*" , SearchOption.TopDirectoryOnly);

		if (musicFolderInfo.Length == 0)
		{
			createMessage.CreateMsg((int)MsgCodeNotice.noMusic , genreFolderInfo.Name);
		}

		//ジャンルごとの曲一覧を定義
		List<MusicInfo> musicInfoList = new List<MusicInfo>();

		//1曲ごとに中のファイルを探索
		for (int j = 0 ; j < musicFolderInfo.Length ; j++)
		{
			musicInfoList.Add(MakeMusicInfo(musicFolderInfo[j] , musicFolderInfo[j].Name));
		}

		GenreInfo genre = new GenreInfo
		{
			Name = genreFolderInfo.Name ,
			Songs = musicInfoList
		};
		genres.Add(genre);
	}


	/// <summary>
	/// 曲ごとの情報を生成
	/// </summary>
	/// <param name="musicFolderInfo"></param>
	/// <param name="musicName"></param>
	/// <returns></returns>
	private MusicInfo MakeMusicInfo ( DirectoryInfo musicFolderInfo ,string musicName)
	{
		MusicInfo musicInfo = new MusicInfo();
		FileInfo[] musicFolderFiles = musicFolderInfo.GetFiles("*" , SearchOption.TopDirectoryOnly);

		CheckExistFile(musicFolderFiles, musicName);

		musicInfo = getMusicInfo.GetInfo(musicFolderFiles, musicName);

		return musicInfo;
	}


	/// <summary>
	/// 音源ファイルとジャケットファイルと譜面ファイルの存在チェック
	/// </summary>
	private void CheckExistFile ( FileInfo[] musicFolderFiles , string musicName)
	{
		bool existMusic = false;
		bool existJacket = false;
		bool existPxbp = false;


		for (int i = 0 ; i < musicFolderFiles.Length ; i++)
		{
			string fileName = Path.GetFileName(musicFolderFiles[i].FullName);
			//ジャケット存在チェック
			if (fileName == (musicName + ext_jpeg) || fileName == ( musicName + ext_jpg ) || fileName == ( musicName + ext_png ))
			{
				existJacket = true;
			}
			//音源ファイル存在チェック
			if (fileName == ( musicName + ext_mp3) || fileName == ( musicName + ext_ogg ) || fileName == ( musicName + ext_wav ))
			{
				existMusic = true;
			}
			//譜面ファイル存在チェック(譜面ファイル名は何でもいいので拡張子だけチェック)
			if (Path.GetExtension(musicFolderFiles[i].FullName) == ext_pxbp)
			{
				existPxbp = true;
			}
		}

		if (!existMusic)
		{
			createMessage.CreateMsg((int)MsgCodeError.noMusic , musicName);
		}
		if (!existPxbp)
		{
			createMessage.CreateMsg((int)MsgCodeError.noScore , musicName);
		}
		if (!existJacket)
		{
			createMessage.CreateMsg((int)MsgCodeNotice.noJacket , musicName);
		}
	}
	
}