using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataProjectManager : MonoBehaviour
{
  private static DataProject data;
  private static string projectFilePath;
  private static GameObject stageModel;
  private static MidiManager midiManager;
  private static DmxManager dmxManager;
  private static OscManager oscManager;
  void Awake()
  {
    midiManager = GameObject.Find("MidiManager").GetComponent<MidiManager>();
    dmxManager = GameObject.Find("DmxManager").GetComponent<DmxManager>();
    oscManager = GameObject.Find("OscManager").GetComponent<OscManager>();
  }

  public DataProject Get()
  {
    return data;
  }

  public void Load(string path)
  {
    // set data
    projectFilePath = Application.streamingAssetsPath + "/Json/" + path + ".json";
    string jsonstr = Util.LoadJson(projectFilePath);
    data = JsonUtility.FromJson<DataProject>(jsonstr);

    // stageModel
    GameObject stageModelPrefab = Util.LoadAsset("Stage/", data.stageModelPath);
    if (stageModelPrefab != null) stageModel = Instantiate(stageModelPrefab);
    if (stageModel == null) Debug.LogError("loadAsset is failed !");

    // midi
    midiManager.Setup(data.deviceMidiArray);

    // dmx
    dmxManager.Setup(data.deviceDmxArray);

    // osc
    oscManager.Setup();
  }

  public void Save(DataProject _data)
  {
    data = _data;
    Util.SaveJson(projectFilePath, data);
  }
}
