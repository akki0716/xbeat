using UnityEngine;

//https://qiita.com/tricrow/items/0c9eed86f3f75cd272ba#ユーザーが対処できない例外の場合のサンプル
public class MyUncaughtExceptionHundler : MonoBehaviour
{
	void OnEnable ()
	{
		Application.logMessageReceived += HandleException;
	}

	void OnDisable ()
	{
		Application.logMessageReceived -= HandleException;
	}

	void HandleException ( string logString , string stackTrace , LogType type )
	{
		if (type == LogType.Exception)
		{
			// ※ ここでは何もしていないが、可能ならロギングもしておくと吉。
			// ※ ケースバイケースだが、非アクティブ化やDestroy()等も必要になるかもしれない。

			// エラーダイアログを表示させる
			//var dialog = GameObject.Find("ErrorDialog");
			//dialog.GetComponent<Canvas>().enabled = true;
		}
	}
}