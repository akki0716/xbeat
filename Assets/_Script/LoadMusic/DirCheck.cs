using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static CreateMessage;

public class DirCheck : MonoBehaviour {

	[SerializeField] CreateMessage createMessage;
	[SerializeField] LoadUtil loadUtil;

	public bool CheckDir ( string bathPath ,string sysPath )
	{
		bool make = false;

		if (!Directory.Exists(bathPath))
		{
			Directory.CreateDirectory(bathPath);
			createMessage.CreateMsg((int)MsgCodeInfo.madeFolder , loadUtil.SongsDirName);
			make = true;
		}
		if (!Directory.Exists(sysPath))
		{
			Directory.CreateDirectory(sysPath);
			createMessage.CreateMsg((int)MsgCodeInfo.madeFolder , loadUtil.sysDirName);
			make = true;
		}

		string jpopDir = bathPath + loadUtil.jpopDirName + "/";
		if (!Directory.Exists(jpopDir))
		{
			Directory.CreateDirectory(jpopDir);
			createMessage.CreateMsg((int)MsgCodeInfo.madeFolder , loadUtil.jpopDirName);
			make = true;
		}
		string animeDir = bathPath + loadUtil.animeDirName + "/";
		if (!Directory.Exists(animeDir))
		{
			Directory.CreateDirectory(animeDir);
			createMessage.CreateMsg((int)MsgCodeInfo.madeFolder , loadUtil.animeDirName);
			make = true;
		}
		string varietyDir = bathPath + loadUtil.varietyDirName + "/";
		if (!Directory.Exists(varietyDir))
		{
			Directory.CreateDirectory(varietyDir);
			createMessage.CreateMsg((int)MsgCodeInfo.madeFolder , loadUtil.varietyDirName);
			make = true;
		}
		string cbDir = bathPath + loadUtil.cbDirName + "/";
		if (!Directory.Exists(cbDir))
		{
			Directory.CreateDirectory(cbDir);
			createMessage.CreateMsg((int)MsgCodeInfo.madeFolder , loadUtil.cbDirName);
			make = true;
		}
		string revDir = bathPath + "/" + loadUtil.revDirName + "/";
		if (!Directory.Exists(revDir))
		{
			Directory.CreateDirectory(revDir);
			createMessage.CreateMsg((int)MsgCodeInfo.madeFolder , loadUtil.revDirName);
			make = true;
		}

		

		if (make)
		{
			createMessage.CreateMsg((int)MsgCodeError.reboot);
		}
		return make;
	}
}
