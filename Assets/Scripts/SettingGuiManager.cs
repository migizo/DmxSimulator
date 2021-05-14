using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Events;

public class SettingGuiManager : MonoBehaviour
{
  Text latestProjectNameText;
  InputField oscPortInputField;
  InputField bloomIntensityInputField;
  Text informationText;
  Bloom bloom;

  DataConfigManager dataConfigManager;
  DataProjectManager dataProjectManager;

  void Awake()
  {
    latestProjectNameText = GameObject.Find("Text (Setting[Project])").GetComponent<Text>();
    oscPortInputField = GameObject.Find("InputField (Setting[Osc Port])").GetComponent<InputField>();
    bloomIntensityInputField = GameObject.Find("InputField (Setting[Bloom Intensity])").GetComponent<InputField>();
    informationText = GameObject.Find("Text (Setting[Information])").GetComponent<Text>();

    oscPortInputField.onEndEdit.AddListener(OnEndEditOscPortInputField);
    bloomIntensityInputField.onEndEdit.AddListener(OnEndEditBloomIntensityInputField);

    dataConfigManager = GameObject.Find("DataConfigManager").GetComponent<DataConfigManager>();

    // osc port
    DataConfig dataConfig = dataConfigManager.Get();
    latestProjectNameText.text = dataConfig.latestProject;
    oscPortInputField.text = dataConfig.oscPort.ToString();

    // bloom
    bloomIntensityInputField.text = dataConfig.bloomIntensity.ToString();
    PostProcessVolume volume = GameObject.Find("Post-process Volume").GetComponent<PostProcessVolume>();
    bloom = volume.profile.GetSetting<Bloom>();
    bloom.intensity.Override(dataConfig.bloomIntensity);
  }

  void Setup()
  {
    latestProjectNameText.text = dataConfigManager.Get().latestProject;  

    string str = "";
    DataProject dataProject = dataProjectManager.Get();
    str += "stageModel: \n";
    str += "    " + dataProject.stageModelPath + "\n";
    str += "\n";
    str += "device: DMX\n";
    for (int i = 0; i < dataProject.deviceDmxArray.Length; i++)
    {
      str += "    device[" + i + "]\n";
      str += "    name:    " + dataProject.deviceDmxArray[i].prefabName + "\n";
      str += "    channel: " + dataProject.deviceDmxArray[i].perDmxChannel + "\n";
      str += "    total:   " + dataProject.deviceDmxArray[i].deviceDmxInfomationArray.Length + "\n";
    }
    str += "\n";
    str += "device: MIDI\n";
    for (int i = 0; i < dataProject.deviceMidiArray.Length; i++)
    {
      str += "    device[" + i + "]\n";
      str += "    name:    " + dataProject.deviceMidiArray[i].prefabName + "\n";
      str += "    total:   " + dataProject.deviceMidiArray[i].deviceMidiInfomationArray.Length + "\n";
    }
    str += "\n";
    str += "\n";
    str += "\n";
    str += "\n";
    informationText.text = str;
  }

  void OnEndEditOscPortInputField(string text)
  {
    // config 更新
    DataConfig dataConfig = dataConfigManager.Get();
    dataConfig.oscPort = int.Parse(text);
    dataConfigManager.Save(dataConfig);
  }

  void OnEndEditBloomIntensityInputField(string text)
  {
    // bloomを取得して更新
    bloom.intensity.value = int.Parse(text);
  }
}
