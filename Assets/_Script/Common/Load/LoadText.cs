using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

public class LoadText : MonoBehaviour {
	[SerializeField] ErrorShow errorShow;


	/// <summary>
	/// filepathを元にファイルを読み込む
	/// </summary>
	/// <param name="filePath"></param>
	public string[] TextLoad ( string filePath)
	{
		string text = "";//ファイルの中身(1行にまるごと)
		string[] separatedText = null;//ファイルの中身(配列1行にファイルの1行)

		//FileInfo file = new FileInfo(filePath);

		try
		{
			using (StreamReader sr = new StreamReader(filePath , Encoding.UTF8))
			{
				text = sr.ReadToEnd();
				separatedText = text.Split('\n');
			}
		}
		catch (Exception e)
		{
			if (!File.Exists(filePath))
			{
				errorShow.FileLoadError(filePath);
			}

			//ErrorMessage.GetComponent<Text>().text = "ファイルが読み込めませんでした。Music.Jsonの確認または\n" +
			//	"フォルダ名、pxbp及びmp3ファイル名は全てMusic.Jsonで指定した「ファイル名」で統一して下さい。 \n"
			//	+ filepath;
			//Debug.Log("ファイル読み込みエラー " + filepath);


		}
		//if (separatedText != null) { DebugShowText(separatedText); }
		return separatedText;
	}


	//file_separatedの中身を見る
	private void DebugShowText (string[] separatedText )
	{
		foreach (var item in separatedText)
		{
			Debug.Log("行  " + item);
		}
	}


	/// <summary>
	/// 複数行のテキストを1行にして返す
	/// </summary>
	/// <param name="SeparateText">改行で区切られ配列になったテキスト</param>
	/// <param name="format">改行やタブを削除して返すか</param>
	/// <returns></returns>
	public string MargeText (string[] separateText , bool format)
	{
		string margeText = null;
		foreach (var line in separateText)
		{
			margeText = margeText + line;
		}
		if (format)
		{
			//改行、タブを削除 空白は削除してしまうと意味が変わるので削除しない
			var reg = new Regex("\r\n|\n|\r|\t");
			margeText = reg.Replace(margeText , "");
		}
		return margeText;
	}


}
