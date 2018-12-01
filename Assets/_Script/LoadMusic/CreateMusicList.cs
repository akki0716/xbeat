using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static CreateMessage;

public class CreateMusicList : MonoBehaviour {
	[SerializeField] LoadUtil loadUtil;
	[SerializeField] CreateMessage createMessage;
	[SerializeField] DirCheck dirCheck;
	[SerializeField] CreateJSON createJSON;
	[SerializeField] GetPxbpInfo getPxbpInfo;


	private const string ext_mp3 = ".mp3";
	private const string ext_ogg = ".ogg";
	private const string ext_wav = ".wav";
	private const string ext_jpeg = ".jpeg";
	private const string ext_jpg = ".jpg";
	private const string ext_png = ".png";
	private const string ext_pxbp = ".pxbp";

	private const string keySound = "SOUND";
	private const string keyScore = "SCORE";
	private const string keyJacket = "JACKET";

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
		//createMessage.CreateMsg(CreateMessage.Msgcode.serching , m , true);

		//各ジャンルごとの曲フォルダ一覧を取得
		DirectoryInfo[] musicFolderInfo = genreFolderInfo.GetDirectories("*" , SearchOption.TopDirectoryOnly);

		if (musicFolderInfo.Length == 0)
		{
			//createMessage.CreateErrMsg(CreateMessage.Errcode.noSong , genreFolderInfo.Name);
		}

		//ジャンルごとの曲一覧を定義
		List<MusicInfo> musicInfoList = new List<MusicInfo>();

		//1曲ごとに中のファイルを探索、リストに追加
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

		CheckFileExist(musicFolderFiles, musicName);

		bool dispSoundErr = false;
		bool dispJacketErr = false;
		bool dispSocreErr = false;

		for (int i = 0 ; i < musicFolderFiles.Length ; i++)
		{
			//特定の拡張子のファイルリストを取得
			Dictionary<string , string> specificFile = GetMusicInfo (musicFolderFiles);
			/*
			//音源取得
			if (specificFile.ContainsKey(keySound))
			{
				songInfo.SoundPath = specificFile[keySound];
			}
			else if(!dispSoundErr)
			{
				createMessage.CreateErrMsg(CreateMessage.Errcode.noSound, songName);
				dispSoundErr = true;
			}

			//ジャケット取得
			if (specificFile.ContainsKey(keyJacket))
			{
				songInfo.JacketPath = specificFile[keyJacket];
			}
			else if(!dispJacketErr)
			{
				createMessage.CreateErrMsg(CreateMessage.Errcode.noJacket, songName);
				dispJacketErr = true;
			}

			//譜面取得
			if (specificFile.ContainsKey(keyScore + "1"))
			{
				songInfo.ScorePath1 = specificFile[keyScore + "1"];
			}
			else if (!dispSocreErr)
			{
				createMessage.CreateErrMsg(CreateMessage.Errcode.noScore , songName);
				dispSocreErr = true;
			}
			if (specificFile.ContainsKey(keyScore + "2"))
			{
				songInfo.ScorePath2 = specificFile[keyScore + "2"];
			}
			if (specificFile.ContainsKey(keyScore + "3"))
			{
				songInfo.ScorePath3 = specificFile[keyScore + "3"];
			}
			if (specificFile.ContainsKey(keyScore + "4"))
			{
				songInfo.ScorePath4 = specificFile[keyScore + "4"];
			}
			if (specificFile.ContainsKey(keyScore + "5"))
			{
				songInfo.ScorePath5 = specificFile[keyScore + "5"];
			}
*/
		}

		return musicInfo;

		/*
		曲フォルダ内のファイル一覧を取得する
		→その中から音源ファイルとpxbpファイルとジャケット画像を選り分ける
		→songFolderFilesを投げる、拡張子に当てはまるファイルを探して当てはまるなら配列に入れる
		投げ返してそれから音源ファイル等を読み取る
		*/
	}

	
	
	//フォルダ内のファイル一覧からMusicInfoに格納する情報を返す
	private Dictionary<string , string> GetMusicInfo ( FileInfo[] songFolderFiles )
	{
		int ScoreSeq = 1;
		string _keyScore = keyScore;//連番をつけるためにグローバル変数からコピー
		Dictionary<string , string> specificFiles = new Dictionary<string , string>();
		List<string> pxbpFiles = new List<string>();

		for (int i = 0 ; i < songFolderFiles.Length ; i++)
		{
			//if (ScoreSeq == 6)
			//{
			//	//todo:譜面が多いエラー(SongInfoには5譜面分までしか確保しないので)
			//}
			switch (songFolderFiles[i].Extension)
			{
				//TODO:マルチプラットフォーム対応時の複数拡張子の対応
				//case ext_mp3:
				//case ext_wav:
				case ext_ogg:
					specificFiles.Add(keySound , songFolderFiles[i].FullName);
					break;
				case ext_jpg:
				case ext_jpeg:
				case ext_png:
					if (!specificFiles.ContainsKey(keyJacket))
					{
						specificFiles.Add(keyJacket , songFolderFiles[i].FullName);
					}
					break;
				case ext_pxbp:
					//pxbpの一覧を作る
					pxbpFiles.Add(songFolderFiles[i].FullName);


					//if (specificFiles.ContainsKey(_keyScore))//重複がある
					//{
					//	_keyScore = keyScore;
					//	_keyScore += ScoreSeq.ToString();
					//	specificFiles.Add(_keyScore , songFolderFiles[i].FullName);
					//	ScoreSeq++;
					//}
					//else
					//{
					//	_keyScore += ScoreSeq.ToString();
					//	specificFiles.Add(_keyScore , songFolderFiles[i].FullName);
					//	ScoreSeq++;
					//}

					break;
				default:
					break;
			}
		}
		//作ったpxbpの一覧を投げる
		Dictionary<string , string> pxbpInfo = getPxbpInfo.GetInfo(pxbpFiles);



		return specificFiles;
	}
	


	/// <summary>
	/// 音源ファイルとジャケットファイルと譜面ファイルの存在チェック
	/// </summary>
	private void CheckFileExist ( FileInfo[] musicFolderFiles , string musicName)
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
			//createMessage.CreateErrMsg();
		}
	}
	
}