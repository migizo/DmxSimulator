using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedStripDeviceDmxManager : DeviceDmxManager
{

  List<MeshRenderer> rendererList = new List<MeshRenderer>();
  protected override void SetupManager()
  {
    foreach (GameObject obj in objectList)
    {
      for (int i = 0; i < perDmxChannel / 3; i++)
      {
        rendererList.Add(obj.transform.GetChild(i).GetComponent<MeshRenderer>());
      }
    }
    foreach (MeshRenderer renderer in rendererList)
    {
      renderer.material.EnableKeyword("_EMISSION");
    }
  }
  public override void ApplyDmx(int ch, int val)
  {
    for (int i = 0; i < deviceTotal; i++)
    {
      if (startDmxChannelList[i] <= ch && ch < startDmxChannelList[i] + perDmxChannel)
      {
        int relativeChannel = ch - (startDmxChannelList[i]);
        float nVal = (float)val / 255.0f;
        int perLedNum = perDmxChannel / 3;
        for (int j = 0; j < perLedNum; j++)
        {
          Color color = rendererList[i * perLedNum + j].material.GetColor("_EmissionColor");
          if (relativeChannel == j * 3 + 0) color.r = nVal;
          if (relativeChannel == j * 3 + 1) color.g = nVal;
          if (relativeChannel == j * 3 + 2) color.b = nVal;
          rendererList[i * perLedNum + j].material.SetColor("_EmissionColor", color);
        }
      }
    }
  }
}
