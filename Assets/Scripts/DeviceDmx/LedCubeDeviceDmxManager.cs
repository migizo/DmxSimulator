using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedCubeDeviceDmxManager : DeviceDmxManager
{
  List<MeshRenderer> rendererList = new List<MeshRenderer>();
  protected override void SetupManager()
  {
    foreach (GameObject obj in objectList)
    {
      rendererList.Add(obj.transform.GetChild(0).GetComponent<MeshRenderer>());
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
        Color color = rendererList[i].material.GetColor("_EmissionColor");
        if (relativeChannel == 0) color.r = nVal;
        if (relativeChannel == 1) color.g = nVal;
        if (relativeChannel == 2) color.b = nVal;
        rendererList[i].material.SetColor("_EmissionColor", color);
      }
    }
  }
}
