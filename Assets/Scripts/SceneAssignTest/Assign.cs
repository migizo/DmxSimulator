using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assign : MonoBehaviour
{
  [SerializeField]
  private GameObject prefab;

  private List<Transform> transformList = new List<Transform>();
  void Start()
  {
    // InstantiateSimpleOne();
    InstantiateGridXY(10, 10, 0.9f, 1);

    // loadn(sample)
    string prjStr = Util.LoadJson(Application.dataPath + "/Json/project_SAMPLE.json");
    DataProject prjData = JsonUtility.FromJson<DataProject>(prjStr);

    // save
    foreach (var device in prjData.deviceDmxArray)
    {
      DataDeviceDmxInfomation[] infoArray = new DataDeviceDmxInfomation[transformList.Count];
      int perDmxChannel = device.perDmxChannel;
      Debug.Log(perDmxChannel);
      for (int i = 0; i < transformList.Count; i++)
      {
        infoArray[i] = new DataDeviceDmxInfomation();
        infoArray[i].startDmxChannel = 1 + perDmxChannel * i;
        infoArray[i].posX = transformList[i].position.x;
        infoArray[i].posY = transformList[i].position.y;
        infoArray[i].posZ = transformList[i].position.z;
        infoArray[i].rotX = transformList[i].eulerAngles.x;
        infoArray[i].rotY = transformList[i].eulerAngles.y;
        infoArray[i].rotZ = transformList[i].eulerAngles.z;
      }
      device.deviceDmxInfomationArray = infoArray;
    }
    Util.SaveJson(Application.dataPath + "/Json/project_Export.json", prjData, false);
  }

  void Update()
  {

  }

  void InstantiateSimpleOne()
  {
    Instantiate(prefab);
  }

  void InstantiateGridXY(int numX, int numY, float margin, float height)
  {
    Vector3 minPos = new Vector3((float)numX * -0.5f * margin, height, (float)numY * -0.5f * margin);
    for (int i = 0; i < numY; i++)
    {
      for (int j = 0; j < numX; j++)
      {
        Vector3 pos = new Vector3(minPos.x + j * margin, minPos.y, minPos.z + i * margin);
        GameObject obj = Instantiate(prefab, pos, Quaternion.identity);
        transformList.Add(obj.transform);
      }
    }
  }
}
