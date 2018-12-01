using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ErrorShow : MonoBehaviour {

	[SerializeField] Canvas canvas;
	GameObject ErrorText;

	 public void FileLoadError (string filePath)
	{
		string messeage = "ファイルが存在しません。\n";
		messeage += filePath;

		ErrorText = (GameObject)Resources.Load("ErrorText");
		ErrorText = Instantiate(ErrorText,Vector3.zero , Quaternion.identity, canvas.transform);
		RectTransform rectTransform = ErrorText.GetComponent<RectTransform>();
		rectTransform.anchoredPosition = new Vector2(0 , 0); 
		ErrorText.GetComponent<Text>().text = messeage;
	}



}
