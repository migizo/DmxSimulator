using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServoMotorDeviceMidiManager : DeviceMidiManager
{
  List<Transform> transforms = new List<Transform>();
  protected override void SetupManager()
  {
    foreach (GameObject obj in objectList)
    {
      transforms.Add(obj.transform.GetChild(0).transform);
    }
  }
  public override void ApplyMidi(int ch, int note, float velocity)
  {
    for (int i = 0; i < deviceTotal; i++)
    {
      if (channels[i] == ch)
      {
        Vector3 angle = transforms[i].localEulerAngles;
        transforms[i].eulerAngles = new Vector3(velocity * 90, angle.y, angle.z);
      }
    }
  }
}
