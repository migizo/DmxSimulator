using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DmxManager : MonoBehaviour
{
  List<DeviceDmxManager> deviceDmxManagerList = new List<DeviceDmxManager>();

  public void Setup(DataDeviceDmx[] deviceArray)
  {
    Clear();
    foreach (var device in deviceArray)
    {
      GameObject manager = new GameObject(device.managerScriptName);
      DeviceDmxManager script = manager.AddComponent(Type.GetType(device.managerScriptName)) as DeviceDmxManager;
      if (script == null)
      {
        Debug.LogWarning(Type.GetType(device.managerScriptName));
      }
      deviceDmxManagerList.Add(script);
      deviceDmxManagerList[deviceDmxManagerList.Count - 1].Initialize(device);
    }
  }

  public void Apply(int ch, int val)
  {
    // Debug.Log("[dmx]: " + ch + ", " + val);
    foreach (DeviceDmxManager deviceManager in deviceDmxManagerList)
    {
      deviceManager.ApplyDmx(ch, val);
    }
  }

  void Clear()
  {
    if (deviceDmxManagerList == null) return;
    if (deviceDmxManagerList.Count == 0) return;
    for (int i = 0; i < deviceDmxManagerList.Count; i++)
    {
      Destroy(deviceDmxManagerList[i].gameObject);
    }
    deviceDmxManagerList.Clear();
  }
}
