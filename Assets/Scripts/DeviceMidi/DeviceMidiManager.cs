using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DeviceMidiManager : MonoBehaviour
{

  protected List<int> channels = new List<int>();
  protected List<GameObject> objectList = new List<GameObject>();
  protected int deviceTotal;
  const bool isDebug = true;
  public void Initialize(DataDeviceMidi data)
  {
    deviceTotal = data.deviceMidiInfomationArray.Length;
    Clear();

    GameObject prefab;
    if (isDebug) prefab = (GameObject)Resources.Load(data.prefabName);
    else prefab = Util.LoadAsset("Device/Midi/", data.prefabName);

    foreach (var info in data.deviceMidiInfomationArray)
    {
      channels.Add(info.channel);

      if (prefab != null)
      {
        objectList.Add(Instantiate(prefab));
        objectList[objectList.Count - 1].transform.parent = transform;
        objectList[objectList.Count - 1].transform.position = new Vector3(info.posX, info.posY, info.posZ);
        objectList[objectList.Count - 1].transform.eulerAngles = new Vector3(info.rotX, info.rotY, info.rotZ);
      }
      else Debug.LogError("loadAsset is failed !");
    }
    SetupManager();
  }
  public abstract void ApplyMidi(int ch, int note, float velocity);

  protected abstract void SetupManager();

  public void Clear()
  {
    if (objectList == null) return;
    if (objectList.Count == 0) return;
    for (int i = 0; i < objectList.Count; i++)
    {
      Destroy(objectList[i]);
    }
    channels.Clear();
    objectList.Clear();
  }
}
