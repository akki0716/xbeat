using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// genreクラスをJSON化するためだけのクラス
/// </summary>
[Serializable]
public class AllGenre {

	[SerializeField] private List<GenreInfo> genres = new List<GenreInfo>();

	public List<GenreInfo> Genres
	{
		get
		{
			return genres;
		}

		set
		{
			genres = value;
		}
	}
}
