// dnSpy decompiler from Assembly-CSharp.dll class: Version
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Version
{
	public abstract string Number { get; }

	public static int Compare(string a, string b)
	{
		int[] array = global::Version.VersionStringToInts(a);
		int[] array2 = global::Version.VersionStringToInts(b);
		for (int i = 0; i < Mathf.Max(array.Length, array2.Length); i++)
		{
			if (global::Version.VersionPiece(array, i) < global::Version.VersionPiece(array2, i))
			{
				return -1;
			}
			if (global::Version.VersionPiece(array, i) > global::Version.VersionPiece(array2, i))
			{
				return 1;
			}
		}
		return 0;
	}

	private static int VersionPiece(IList<int> versionInts, int pieceIndex)
	{
		return (pieceIndex >= versionInts.Count) ? 0 : versionInts[pieceIndex];
	}

	private static int[] VersionStringToInts(string version)
	{
		int piece;
		return (from v in version.Split(new char[]
		{
			'.'
		})
		select (!int.TryParse(v, out piece)) ? 0 : piece).ToArray<int>();
	}
}
