using System.Collections;
using System.IO;
using System;
using UnityEngine.UI;
using UnityEngine;

public class ScreenCaptureManager : MonoBehaviour
{
  DataConfigManager dataConfigManager;
  void Awake() {
    dataConfigManager = GameObject.Find("DataConfigManager").GetComponent<DataConfigManager>();
  }
  private void OnApplicationQuit()
  {
    // これprojectの名前になるようにする
    // string path = Application.persistentDataPath + "/Resources/ScreenShots/" + DataConfigManager.instance.Get().latestProject + ".png";
    string path = Application.streamingAssetsPath + "/ScreenShots/" + dataConfigManager.Get().latestProject + ".png";
    StartCoroutine("CeateScreenShot", path);
  }
  IEnumerator CeateScreenShot(string path)
  {
    Texture2D screenShot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
    RenderTexture rt = new RenderTexture(screenShot.width, screenShot.height, 24);

    RenderTexture prev = Camera.main.targetTexture;
    Camera.main.targetTexture = rt;
    Camera.main.Render();
    Camera.main.targetTexture = prev;
    RenderTexture.active = rt;

    screenShot.ReadPixels(new Rect(0, 0, screenShot.width, screenShot.height), 0, 0);
    screenShot.Apply();

    byte[] bytes = screenShot.EncodeToPNG();

    File.WriteAllBytes(path, bytes);

    while (File.Exists(path) == false)
    {
      yield return null;
    }
    Camera.main.targetTexture = null;
    RenderTexture.active = null;
    Destroy(screenShot);
    Destroy(rt);
    Destroy(prev);
    // Debug.Log("CaptureScreenshot: " + path);
  }

  // deprecated
  // IEnumerator CeateScreenShot(string path)
  // {
  //   // スクリーンショットを撮る
  //   ScreenCapture.CaptureScreenshot(path);

  //   while (File.Exists(path) == false)
  //   {
  //     yield return new WaitForSeconds(1f);
  //   }
  //   Debug.Log("CaptureScreenshot: " + path);
  // }
}