using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Database
{
    public int number;
    public string name;
    public string fullName;
    public string country;
    public string quote;
    public string text;
    public string img;
    public string video;
    public string link;
    public string book;
    public string comment;
}
[System.Serializable]
public class Entry
{
    public Database[] data;
}