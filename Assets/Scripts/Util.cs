using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
public class Util
{
  public static string LoadJson(string path)
  {
    string datastr = "";
    StreamReader reader;
    reader = new StreamReader(path);
    datastr = reader.ReadToEnd();
    reader.Close();
    return datastr;
  }
  public static void SaveJson<T>(string path, T data, bool isOverride = false)
  {
    var json = JsonUtility.ToJson(data);
    StreamWriter writer = new StreamWriter(path, isOverride);
    writer.WriteLine(json);
    writer.Flush();
    writer.Close();
  }

  public static GameObject LoadAsset(string folder, string name)
  {
    var assetBundle = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/AssetBundle/" + folder + name);
    GameObject obj = assetBundle.LoadAsset<GameObject>(name); // assetBundle元のpath, 名前+拡張子でも可能
    assetBundle.Unload(false); // 不要になったAssetBundleのメタ情報をアンロードする
    return obj;
  }

  public static List<string> GetAssetNames(string path) {
    List<string> nameList = new List<string>();
    string assetBundlePath = Application.streamingAssetsPath + "/AssetBundle/" + path;
    string[] names = Directory.GetFiles(assetBundlePath);
    for (int i = 0; i < names.Length; i++) {
        if (Path.GetExtension(names[i]) == "") {
          nameList.Add(Path.GetFileName(names[i]));
        }
    }
    return nameList;
  }

  public static List<string> GetFolderNames(string path) {
    List<string> nameList = new List<string>();
    string assetFolderPath = Application.streamingAssetsPath + "/AssetBundle/" + path;
    string[] names = Directory.GetDirectories(assetFolderPath);
    for (int i = 0; i < names.Length; i++) {
      nameList.Add(Path.GetFileName(names[i]));
    }
    return nameList;
  }
}
