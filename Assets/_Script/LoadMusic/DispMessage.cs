using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DispMessage : MonoBehaviour {

	[SerializeField] private TextMeshProUGUI msg;

	public string Message { get; set; }

	public void ShowMesseage (int msgLevel , string message)
	{
		message = EditMessage(msgLevel , message);
		Message += message;
		msg.text = Message;
		if (msgLevel == 3)
		{
			Debug.Log(Message);
			throw new Exception("エラーが発生しました。内容は直前のログを参照。");
		}
	}

	private string EditMessage ( int msgLevel ,string message )
	{
		if (msgLevel == 1)//情報
		{
			return message = message.Insert(0 , "<color=#ffffff>[Info]:</color>");
		}
		else if (msgLevel == 2)//注意
		{
			return message = message.Insert(0, "<color=#efe700>[Notice]:</color>");
		}
		else if (msgLevel == 3)//エラー
		{
			return message = message.Insert(0 , "<color=#be0000>[Error]:</color>");
		}
		else if (msgLevel == 4)
		{
			return message;
		}
		return null;
	}

}
