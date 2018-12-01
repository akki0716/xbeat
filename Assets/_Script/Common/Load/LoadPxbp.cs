using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPxbp : MonoBehaviour {
	[SerializeField] LoadText loadText;

	public string PxbpLoad (string filePath)
	{
		string[] json = loadText.TextLoad(filePath);
		if (json.Length == 1)
		{
			return json[0];
		}
		else
		{
			string margeJson = loadText.MargeText(json , true);
			return margeJson;
		}
	}

}
