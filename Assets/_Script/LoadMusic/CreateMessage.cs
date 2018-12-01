using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 通常メッセージ、エラーメッセージを定義、メッセージをDispMessageに投げる
/// </summary>
public class CreateMessage : MonoBehaviour {

	[SerializeField] DispMessage dispMessage;

	/// <summary>
	/// インフォメーション用メッセージコード
	/// </summary>
	public enum MsgCodeInfo
	{
		loadStart = 1000, serching, madeFolder,
	}

	/// <summary>
	/// 注意用メッセージコード
	/// </summary>
	public enum MsgCodeNotice
	{
		moveCancel = 2000 , noSong, noJacket
	}

	/// <summary>
	/// エラー用メッセージコード
	/// </summary>
	public enum MsgCodeError
	{
		reboot = 3000, moveCancel, noSound, noScore
	}

	//各コードの範囲をintで持つ
	int[] codeInfoRange = new int[2] { 1000 , 1999 };
	int[] codeNoticeRange = new int[2] { 2000 , 2999 };
	int[] codeErrorRange = new int[2] { 3000 , 3999 };

	//enumのコード、メッセージの順
	Dictionary<int,  string> MessageDict = new Dictionary<int , string>();



	private void Awake ()
	{
		MessageDict.Add((int)MsgCodeInfo.loadStart, "曲ロード開始\n");
		MessageDict.Add((int)MsgCodeInfo.serching , "{addMsg1}の曲を検索中\n");
		MessageDict.Add((int)MsgCodeInfo.madeFolder, "{addMsg1}フォルダを作成しました。\n");

		MessageDict.Add((int)MsgCodeNotice.noSong , "ジャンル:{addMsg1}に属する曲がありません。\n");
		MessageDict.Add((int)MsgCodeNotice.noSong , "{addMsg1}のジャケットファイルがありません。\n");

		MessageDict.Add((int)MsgCodeError.reboot , "アプリを再起動してください。\n");
		MessageDict.Add((int)MsgCodeError.moveCancel , "遷移キャンセル\nアプリを再起動してください。\n");
		MessageDict.Add((int)MsgCodeError.noSound , "{addMsg1}の音源ファイルがありません。\n");
		MessageDict.Add((int)MsgCodeError.noSound , "{addMsg1}の譜面ファイルがありません。\n");
	}


	/// <summary>
	/// 各種メッセージを作って画面上に表示する。
	/// </summary>
	/// <param name="msgcode">MsgCodeの各種enumをintにキャストして渡す</param>
	/// <param name="addMsgList">追加のメッセージ。省略可。</param>
	public void CreateMsg ( int msgcode , string addMsgList = null)
	{
		if (addMsgList != null)
		{
			string [] temp = new string[1] { addMsgList };
			CreateMsg(msgcode , temp);
		}
		else
		{
			CreateMsg(msgcode , new string[0]);
		}
	}

	//こいつがメインのメソッド。
	/// <summary>
	/// 各種メッセージを作って画面上に表示する。
	/// </summary>
	/// <param name="msgCode">MsgCodeの各種enumをintにキャストして渡す</param>
	/// <param name="addMsgList"></param>
	public void CreateMsg (int msgCode , string[] addMsgList)
	{
		string message;
		if (addMsgList.Length != 0)
		{
			message = MakeMsg(msgCode , addMsgList);
		}
		else
		{
			message = MakeMsg(msgCode, new string[0]);
		}

		int msgLevel = GetMsgLevel(msgCode);
		dispMessage.ShowMesseage(msgLevel, message);
	}

	/// <summary>
	/// メッセージの定義から実際に表示するメッセージに変換する。
	/// </summary>
	/// <param name="msgcode"></param>
	/// <param name="addMsgList"></param>
	/// <returns></returns>
	private string MakeMsg ( int msgcode , string[] addMsgList)
	{
		string Msg;
		if (MessageDict.ContainsKey(msgcode))
		{
			Msg = MessageDict[msgcode];
		}
		else
		{
			Msg = "定義されていないメッセージです。";
		}

		for (int i = 0 ; i < addMsgList.Length ; i++)
		{
			string replaceTarget = "{addMsg" + (i+1).ToString() + "}";
			Msg = Msg.Replace(replaceTarget , addMsgList[i]);
		}
		return Msg;
	}



	/*

	/// <summary>
	/// 通常メッセージを作ってDispMessageに投げる
	/// </summary>
	/// <param name="msgcode">CreateMessageで定義されているコード</param>
	/// <param name="addMsg">追加メッセージ。省略可</param>
	/// <param name="overWrite">メッセージを上書きするか。省略可</param>
	public void CreateMsg ( Msgcode msgcode , string addMsg = null, bool overWrite = false)
	{

		string msg;
		switch (msgcode)
		{
			case Msgcode.loadStart:
				msg = "曲ロード開始\n";
				break;
			case Msgcode.serching:
				msg = addMsg + "の曲を検索中\n";
				break;
			case Msgcode.madeFolder:
				msg = addMsg + "フォルダを作成しました。\n";
				break;
			case Msgcode.restart:
				msg = "アプリを再起動してください\n";
				break;
			case Msgcode.moveCancel:
				msg = "\n遷移キャンセル\n";
				msg += "アプリを再起動してください\n";
				break;
			default:
				msg = "定義されていないメッセージです。\n";
				break;
		}
		dispMessage.ShowLoadingMesseage(msg,overWrite);
	}

	*/
	/*
	/// <summary>
	/// エラーメッセージを作ってDispMessageに投げる
	/// </summary>
	/// <param name="errcode">CreateMessageで定義されているコード</param>
	/// <param name="addMsg">追加メッセージ。省略可</param>
	/// <param name="overWrite">メッセージを上書きするか。省略可</param>
	public void CreateErrMsg ( Errcode errcode , string addMsg = null , bool overWrite = false )
	{
		string msg;
		switch (errcode)
		{
			case Errcode.noSong:
				msg = "ジャンル:"+ addMsg + "に属する曲がありません。\n";
				break;
			case Errcode.noSound:
				msg = addMsg + "の音源ファイルがありません。\n";
				break;
			case Errcode.noJacket:
				msg = addMsg + "のジャケットファイルがありません。\n";
				break;
			case Errcode.noScore:
				msg = addMsg + "の譜面ファイルがありません。\n";
				break;
			case Errcode.multipleDifficalty:
				msg = addMsg + "フォルダ内に同一難易度の譜面が入っています。\n";
				msg += "ファイル情報は後に読み込んだもので上書きされます。\n";
				break;
			default:
				msg = "定義されていないエラーメッセージです。\n";
				break;
		}
		dispMessage.ShowErrMesseage(msg , overWrite);
	}
	*/
	

		/*
	public IEnumerator CountdownCoroutine ( UnityAction callback )
	{
		int countTime = 3;
		string baseText1 = "曲ロードが終了しました。";
		string baseText2 = "秒後に画面遷移します。";
		string baseText3 = "\n画面右を長押しで遷移キャンセル";

		string text = baseText1 + countTime + baseText2 + baseText3;
		dispMessage.ShowMesseage(text , true);
		yield return new WaitForSeconds(1.0f);

		countTime--;
		text = baseText1 + countTime + baseText2 + baseText3;
		dispMessage.ShowMesseage(text , true);
		yield return new WaitForSeconds(1.0f);

		countTime--;
		text = baseText1 + countTime + baseText2 + baseText3;
		dispMessage.ShowMesseage(text , true);
		yield return new WaitForSeconds(1.0f);


		yield return new WaitForSeconds(1.0f);
		callback();
	}
	*/

	private int GetMsgLevel (int msgCode)
	{
		if (msgCode >= codeInfoRange[0] && msgCode <= codeInfoRange[1])
		{
			return 1;
		}
		else if (msgCode >= codeNoticeRange[0] && msgCode <= codeNoticeRange[1])
		{
			return 2;
		}
		else if (msgCode >= codeErrorRange[0] && msgCode <= codeErrorRange[1])
		{
			return 3;
		}
		return 4;
	}

}
