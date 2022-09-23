using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Text
{
	public int id;
	public int messageField;
	public string messageText;
	public int length;
}
[System.Serializable]
public class Data
{
	public Text[] text;
}

