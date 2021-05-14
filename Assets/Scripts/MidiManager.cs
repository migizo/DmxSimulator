using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidiManager : MonoBehaviour
{
  List<DeviceMidiManager> deviceMidiManagerList = new List<DeviceMidiManager>();
  void Start()
  {
    MidiJack.MidiMaster.noteOnDelegate = NoteOn;
    //     // MidiMaster.noteOffDelegate (channel, noteNumber)
    //     // MidiMaster.knobDelegate (channel, knobNumber, konbValue) // control change
  }

  public void Setup(DataDeviceMidi[] deviceArray)
  {
    Clear();
    foreach (var device in deviceArray)
    {
      GameObject manager = new GameObject(device.managerScriptName);
      var _type = Type.GetType(device.managerScriptName);
      DeviceMidiManager script = manager.AddComponent(_type) as DeviceMidiManager;
      if (script == null)
      {
        Debug.LogWarning(Type.GetType(device.managerScriptName));
      }
      deviceMidiManagerList.Add(script);
      deviceMidiManagerList[deviceMidiManagerList.Count - 1].Initialize(device);
    }
  }

  public void NoteOn(MidiJack.MidiChannel channel, int note, float velocity/* 0.0~1.0 */)
  {
    foreach (DeviceMidiManager deviceMidiManager in deviceMidiManagerList)
    {
      // Debug.Log("[Midi]: " + (int)channel + 1 + ", " + note + ", " + velocity);
      deviceMidiManager.ApplyMidi((int)channel + 1, note, velocity);
    }
  }

  void Clear()
  {
    if (deviceMidiManagerList == null) return;
    if (deviceMidiManagerList.Count == 0) return;
    for (int i = 0; i < deviceMidiManagerList.Count; i++)
    {
      Destroy(deviceMidiManagerList[i].gameObject);
    }
    deviceMidiManagerList.Clear();
  }
}
