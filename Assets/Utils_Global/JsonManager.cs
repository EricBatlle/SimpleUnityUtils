using System;
using System.IO;
using UnityEditor;
using UnityEngine;

[Serializable]
public class JsonFormatException : Exception
{
	public JsonFormatException()
	{
		Debug.Log("JsonFormat not valid, should start with '" + JsonManager.JSON_OBJECT_FIRST_BRACKET + "' for JsonObjects or '" + JsonManager.JSON_OBJECT_FIRST_BRACKET + "' for JsonArrayObjects");
	}
}

public static class JsonManager
{
	public static char JSON_OBJECT_FIRST_BRACKET = '{';
	public static char JSON_ARRAY_FIRST_BRACKET = '[';

	//Wrapper made it to deal with Nested jsons
	[Serializable]
	private class Wrapper<T>
	{
		public T[] array = null;
	}

	public static string GetJsonContentAsString(string jsonFileName)
	{
		string path = Application.dataPath + "/" + jsonFileName + ".json";
		return File.ReadAllText(path);
	}

	#region Deserialize
	public static T[] DeserializeFromJsonArray<T>(string jsonString)
	{
		char jsonFirstCharacter = jsonString[0];

		//Return deserialized object only if the string is in json format
		if (jsonFirstCharacter != JSON_ARRAY_FIRST_BRACKET)
			throw new JsonFormatException();
		else
		{
			string newJson = "{ \"array\": " + jsonString + "}";
			Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
			return wrapper.array;
		}
	}
	public static T DeserializeFromJson<T>(string jsonString)
	{
		if (string.IsNullOrEmpty(jsonString))
			return default;

		char jsonFirstCharacter = jsonString[0];

		//Return deserialized object only if the string is in json format
		if (jsonFirstCharacter != JSON_OBJECT_FIRST_BRACKET)
			throw new JsonFormatException();
		else
			return JsonUtility.FromJson<T>(jsonString);
	}
	#endregion

	#region Serialize
	public static string SerializeToJson<T>(T objectToSerialize)
	{
		return JsonUtility.ToJson(objectToSerialize);
	}
	public static string SerializeToJsonArray<T>(T[] arrayToSerialize)
	{
		Wrapper<T> wrapper = new Wrapper<T>();
		wrapper.array = arrayToSerialize;
		return JsonUtility.ToJson(wrapper);
	}
	#endregion

	#region Write/Read Json files
	//Write to JSON
	public static void WriteJSONFile(string fileName, string content, string storingPath = "Assets", bool updateExistingFile = false)
	{
		//Check if storingPath exists, if not, create the directories and subdirectories
		if (!Directory.Exists(storingPath))
		{
			Debug.Log(string.Format("Path directory {0} does not exist, created now.", storingPath));
			Directory.CreateDirectory(storingPath);
		}

		//Give the fileName an index in case the name already exists file--> file_1
		string completePath = storingPath + "/" + fileName + ".json";
		string newFilename = fileName + "_";
		int i = 0;
		
		if(!updateExistingFile)
		{
			while (File.Exists(completePath))
			{
				fileName = newFilename + i;
				completePath = storingPath + "/" + fileName + ".json";
				i++;
			}
		}

		//Write the JSON file
		StreamWriter writer = new StreamWriter(completePath, false);
		writer.Write(content);
		writer.Close();
		Debug.Log(string.Format("Created new JSON file {0} in {1}", fileName, completePath));
#if UNITY_EDITOR
		AssetDatabase.Refresh();
#endif
	}

	//Read from JSON
	public static string ReadJSONFile(string filePath)
	{
		string content = "";
		try
		{
			using (StreamReader reader = new StreamReader(filePath))
			{
				content = reader.ReadToEnd();
				return content;
			}
		}
		catch (Exception e)
		{
			// Let the user know what went wrong.
			Console.WriteLine($"The file {filePath} could not be read:");
			Console.WriteLine(e.Message);
		}
		return content;
	}
	public static string ReadJSONFile(TextAsset textAsset)
	{
		return textAsset.text;
	}
	#endregion
}
