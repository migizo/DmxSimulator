using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
  bool enableControl = true;

  void Update()
  {
    if (!enableControl) return;

    Camera.main.transform.position += Input.GetAxis("Mouse ScrollWheel") * Camera.main.transform.forward;
    Vector2 mouse = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
    if (Input.GetMouseButton(0))
    {
      Camera.main.transform.localEulerAngles += new Vector3(mouse.y * 4, mouse.x * 2 * -1, 0);
    }
    else if (Input.GetMouseButton(1))
    {
      Camera.main.transform.position += (Camera.main.transform.up * mouse.y * -1) + (Camera.main.transform.right * mouse.x * -1);
    }
  }

  public void EnableControl(bool isEnable)
  {
    enableControl = isEnable;
  }
}
