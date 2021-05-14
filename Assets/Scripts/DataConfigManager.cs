using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataConfigManager : MonoBehaviour
{
  private DataConfig data;
  private string configFilePath;

  void Awake()
  {
    configFilePath = Application.streamingAssetsPath + "/Json/config.json";
    string jsonstr = Util.LoadJson(configFilePath);
    data = JsonUtility.FromJson<DataConfig>(jsonstr);
  }

  public DataConfig Get()
  {
    return data;
  }

  public void Save(DataConfig _data)
  {
    data = _data;
    Util.SaveJson(configFilePath, data);
  }
}
