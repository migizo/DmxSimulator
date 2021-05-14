using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;

public class Main : MonoBehaviour
{
  private void Start()
  {
    GameObject.Find("GuiStateManager").GetComponent<SimulatorGuiStateManager>().FadeToStart();
  }
}
