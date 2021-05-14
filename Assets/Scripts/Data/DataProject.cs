using UnityEngine;
using System.Collections;

[System.Serializable]
public class DataProject
{
  public string projectName;
  public string stageModelPath;
  public DataDeviceDmx[] deviceDmxArray;
  public DataDeviceMidi[] deviceMidiArray;
}

// ------------------
// DMX
// ------------------
[System.Serializable]
public class DataDeviceDmx
{
  public string managerScriptName;
  public string prefabName;
  public int perDmxChannel;
  public DataDeviceDmxInfomation[] deviceDmxInfomationArray;
}

[System.Serializable]
public class DataDeviceDmxInfomation
{
  public int startDmxChannel;
  public float posX;
  public float posY;
  public float posZ;
  public float rotX;
  public float rotY;
  public float rotZ;
}

// ------------------
// MIDI
// ------------------
[System.Serializable]
public class DataDeviceMidi
{
  public string managerScriptName;
  public string prefabName;
  public DataDeviceMidiInfomation[] deviceMidiInfomationArray;
}

[System.Serializable]
public class DataDeviceMidiInfomation
{
  public int channel;
  public float posX;
  public float posY;
  public float posZ;
  public float rotX;
  public float rotY;
  public float rotZ;
}