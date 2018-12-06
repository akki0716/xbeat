using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DispMessage : MonoBehaviour {

	[SerializeField] private TextMeshProUGUI msg;
	[SerializeField] private ScrollRect scrollRect;
	[SerializeField] private ContentSizeFitter contentSizeFitter;

	public string Message { get; set; }

	public void ShowMesseage (int msgLevel , string dispMessage)
	{
		dispMessage = EditMessage(msgLevel , dispMessage);
		Message += dispMessage;
		msg.text = Message;
		contentSizeFitter.SetLayoutVertical();
		scrollRect.verticalNormalizedPosition = 0;
		if (msgLevel == 3)
		{
			Debug.Log(Message);
			Message += "\n上記エラーを解消してからアプリを再起動してください。";
			msg.text = Message;
			throw new Exception("エラーが発生しました。\n内容:" + dispMessage);
		}
	}

	private string EditMessage ( int msgLevel ,string message )
	{
		if (msgLevel == 1)//情報
		{
			return message = message.Insert(0 , "<color=#ffffff>[Info]</color>:");
		}
		else if (msgLevel == 2)//注意
		{
			return message = message.Insert(0, "<color=#efe700>[Notice]</color>:");
		}
		else if (msgLevel == 3)//エラー
		{
			return message = message.Insert(0 , "<color=#be0000>[Error]</color>:");
		}
		else if (msgLevel == 4)
		{
			return message;
		}
		return null;
	}

}
