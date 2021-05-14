using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DeviceDmxManager : MonoBehaviour
{

  protected List<int> startDmxChannelList = new List<int>();
  protected int perDmxChannel = 16;
  protected List<GameObject> objectList = new List<GameObject>();
  protected int deviceTotal;
  public void Initialize(DataDeviceDmx data)
  {
    perDmxChannel = data.perDmxChannel;
    deviceTotal = data.deviceDmxInfomationArray.Length;
    Clear();
    GameObject obj = Util.LoadAsset("Device/Dmx/", data.prefabName);
    foreach (var info in data.deviceDmxInfomationArray)
    {
      startDmxChannelList.Add(info.startDmxChannel);

      if (obj != null)
      {
        objectList.Add(Instantiate(obj));
        objectList[objectList.Count - 1].transform.parent = transform;
        objectList[objectList.Count - 1].transform.position = new Vector3(info.posX, info.posY, info.posZ);
        objectList[objectList.Count - 1].transform.eulerAngles = new Vector3(info.rotX, info.rotY, info.rotZ);
      }
      else Debug.LogError("loadAsset is failed !");
    }
    SetupManager();
  }
  public abstract void ApplyDmx(int ch, int val);

  protected abstract void SetupManager();

  public void Clear()
  {
    if (objectList == null) return;
    if (objectList.Count == 0) return;
    for (int i = 0; i < objectList.Count; i++)
    {
      Destroy(objectList[i]);
    }
    objectList.Clear();
    startDmxChannelList.Clear();
  }
}
