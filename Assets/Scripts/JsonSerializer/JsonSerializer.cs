using UnityEngine;
using System.Collections;
using System;
using System.IO;

public static class JsonSerializer {

	public static bool ToFile (object obj, string fileName) {
		return ToFile (obj, null, fileName);
	}

	public static bool ToFile (object obj, string password, string fileName) {
		bool success = false;
		string pureJson = JsonUtility.ToJson (obj, false);
		if (!string.IsNullOrEmpty (password))
			pureJson = AES.Encrypt (pureJson, password);
		string filePath = Path.Combine (Application.dataPath, fileName);
		try {
			File.WriteAllText (filePath, pureJson);
			success = true;
		} catch (Exception ex) {
			Debug.LogException (ex);
		}
		return success;
	}

	public static T FromFile<T> (string fileName) {
		return FromFile<T> (null, fileName);
	}

	public static T FromFile<T> (string password, string fileName) {
		T result = default(T);
		string filePath = Path.Combine (Application.dataPath, fileName);
		try {
			string pureJson = File.ReadAllText (filePath);
			if (!string.IsNullOrEmpty (password))
				pureJson = AES.Decrypt(pureJson, password);
			result = JsonUtility.FromJson<T> (pureJson);
		} catch (Exception ex) {
			Debug.LogException (ex);
		}
		return result;
	}

}
