using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class SimulatorGuiStateManager : MonoBehaviour
{
    Canvas projectSelectCanvas;
    Canvas simulatorCanvas;
    Canvas settingCanvas;

    CanvasGroup projectSelectCanvasGroup;
    CanvasGroup simulatorCanvasGroup;
    CanvasGroup settingCanvasGroup;

    CameraManager cameraManager;
    OscManager oscManager;

    DataConfigManager dataConfigManager;
    DataProjectManager dataProjectManager;

    const int fadeFrame = 60;

    void Awake() {
      GameObject projectSelectCanvasObj = GameObject.Find("Canvas (ProjectSelect)");
      GameObject simulatorCanvasObj = GameObject.Find("Canvas (Simulator)");
      GameObject settingCanvasObj = GameObject.Find("Canvas (Setting)");

      projectSelectCanvas = projectSelectCanvasObj.GetComponent<Canvas>();
      simulatorCanvas = simulatorCanvasObj.GetComponent<Canvas>();
      settingCanvas = settingCanvasObj.GetComponent<Canvas>();

      projectSelectCanvasGroup = projectSelectCanvasObj.GetComponent<CanvasGroup>();
      simulatorCanvasGroup = simulatorCanvasObj.GetComponent<CanvasGroup>();
      settingCanvasGroup = settingCanvasObj.GetComponent<CanvasGroup>();

      projectSelectCanvas.enabled = false;
      simulatorCanvas.enabled = false;
      settingCanvas.enabled = false;

      projectSelectCanvasGroup.alpha = 0;
      simulatorCanvasGroup.alpha = 0;
      settingCanvasGroup.alpha = 0;

      oscManager = GameObject.Find("OscManager").GetComponent<OscManager>();
      cameraManager = GameObject.Find("CameraManager").GetComponent<CameraManager>();
      dataConfigManager = GameObject.Find("DataConfigManager").GetComponent<DataConfigManager>();
      dataProjectManager = GameObject.Find("DataProjectManager").GetComponent<DataProjectManager>();
    }

    //-------------------------------------------------------------
    public void FadeToStart() 
    {
      bool enableSelectProject = dataConfigManager.Get().enableSelectProject;
      if (enableSelectProject) {
        SetEnableProjectSelect(true);
      }
      else {
        SetEnableSimulator(true);
      }
      StartCoroutine(FadeManager.instance.FadeIn(fadeFrame));
    }

    //-------------------------------------------------------------
    public void FadeToSimulatorFromProjectSelect() {
      StartCoroutine(FadeManager.instance.FadeOut(fadeFrame, _FadeToSimulatorFromProjectSelect));
    }

    void _FadeToSimulatorFromProjectSelect() {
      SetEnableProjectSelect(false);

      SetEnableSimulator(true);
      StartCoroutine(FadeManager.instance.FadeIn(fadeFrame));
    }

    //-------------------------------------------------------------
    public void FadeToSettingFromSimulator() {
      StartCoroutine(FadeOutSimulator(fadeFrame, _FadeToSettingFromSimulator));
    }

    void _FadeToSettingFromSimulator(){
      SetEnableSimulator(false);
      SetEnableSetting(true);
      StartCoroutine(FadeInSetting(fadeFrame));
    }

    //-------------------------------------------------------------
    public void FadeToSimulatorFromSetting() {
      StartCoroutine(FadeOutSetting(fadeFrame, _FadeToSimulatorFromSetting));
    }

    void _FadeToSimulatorFromSetting() {
      SetEnableSetting(false);
      SetEnableSimulator(true);
      StartCoroutine(FadeInSimulator(fadeFrame));
    }

    //-------------------------------------------------------------
    public void FadeToProjectSelectFromSetting() {
      StartCoroutine(FadeManager.instance.FadeOut(fadeFrame, _FadeToProjectSelectFromSetting));
    }

    void _FadeToProjectSelectFromSetting() {
      SetEnableSimulator(false);
      SetEnableSetting(false);
      SetEnableProjectSelect(true);
      StartCoroutine(FadeManager.instance.FadeIn(fadeFrame));
    }

    //-------------------------------------------------------------
    void SetEnableProjectSelect(bool isEnable) {
      projectSelectCanvas.enabled = isEnable;
      projectSelectCanvasGroup.alpha = isEnable ? 1 : 0;
    }
    void SetEnableSimulator(bool isEnable) {
      simulatorCanvas.enabled = isEnable;
      simulatorCanvasGroup.alpha = isEnable ? 1 : 0;
      cameraManager.EnableControl(isEnable);
      if (isEnable) {
        string lastestProject = dataConfigManager.Get().latestProject;
        dataProjectManager.Load(lastestProject);
      }
      else oscManager.Dispose();
    }
    void SetEnableSetting(bool isEnable) {
      settingCanvas.enabled = isEnable;
      settingCanvasGroup.alpha = isEnable ? 1 : 0;
    }

    //-------------------------------------------------------------
    IEnumerator FadeInSimulator(int frameNum)
    {
      float delta = 1.0f / frameNum;
      for (float f = 0.0f; f <= 1.0f; f += delta)
      {
        simulatorCanvasGroup.alpha = f; // alpha level
        yield return null;
      }
    }

    IEnumerator FadeOutSimulator(int frameNum, UnityAction callback)
    {
      float delta = 1.0f / frameNum;
      for (float f = 1.0f; f >= 0.0f; f -= delta)
      {
        simulatorCanvasGroup.alpha = f; // alpha level
        yield return null;
      }
      callback();
    }

    IEnumerator FadeInSetting(int frameNum)
    {
      float delta = 1.0f / frameNum;
      for (float f = 0.0f; f <= 1.0f; f += delta)
      {
        settingCanvasGroup.alpha = f; // alpha level
        yield return null;
      }
    }

    IEnumerator FadeOutSetting(int frameNum, UnityAction callback)
    {
      float delta = 1.0f / frameNum;
      for (float f = 1.0f; f >= 0.0f; f -= delta)
      {
        settingCanvasGroup.alpha = f; // alpha level
        yield return null;
      }
      callback();
    }
}
