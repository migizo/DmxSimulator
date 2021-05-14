using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Events;

public class ProjectSelectGuiManager : MonoBehaviour
{
  string currentSelectProjectName;
  List<Button> buttonList = new List<Button>();
  List<string> projectList = new List<string>();

  DataConfigManager dataConfigManager;
  DataProjectManager dataProjectManager;
  void Awake()
  {    
    Transform scrollView = GameObject.Find("Scroll View (ProjectList)").transform;
    Transform viewPort = scrollView.Find("Viewport");
    Transform contentsRoot = viewPort.transform.Find("Content");

    dataConfigManager = GameObject.Find("DataConfigManager").GetComponent<DataConfigManager>();
    dataProjectManager = GameObject.Find("DataProjectManager").GetComponent<DataProjectManager>();

    DataConfig dataConfig = dataConfigManager.Get();
    currentSelectProjectName = dataConfig.latestProject;
    GameObject prefab = contentsRoot.Find("Button").gameObject;
    for (int i = 0; i < dataConfig.projectArray.Length; i++)
    {
      GameObject btnObj;
      if (i == 0) btnObj = prefab;
      else
      {
        btnObj = Instantiate(prefab);
        btnObj.transform.SetParent(contentsRoot);
        RectTransform rect = btnObj.GetComponent<RectTransform>();
        rect.localPosition = new Vector2(0, i * rect.rect.height);
      }
      btnObj.transform.Find("Text").GetComponent<Text>().text = dataConfig.projectArray[i];
      Button btn = btnObj.GetComponent<Button>();
      var idx = i;
      btn.onClick.AddListener(() => ClickListContent(idx));

      buttonList.Add(btn);
      projectList.Add(dataConfig.projectArray[i]);
    }
    LoadCaptureImage("ScreenShots/" + currentSelectProjectName + ".png");
  }

  //-------------------------------------------------------------

  void Load()
  {
    DataConfig dataConfig = dataConfigManager.Get();
    dataConfig.latestProject = currentSelectProjectName;
    dataConfigManager.Save(dataConfig);

    string projectPath = dataConfigManager.Get().latestProject;
    dataProjectManager.Load(projectPath);
  }
  public void Enable()
  {

    currentSelectProjectName = dataConfigManager.Get().latestProject;
    LoadCaptureImage("ScreenShots/" + currentSelectProjectName + ".png");
  }

  void ClickListContent(int buttonID)
  {
    // 選択中のプロジェクトファイル名の更新
    currentSelectProjectName = projectList[buttonID];
    LoadCaptureImage("ScreenShots/" + currentSelectProjectName + ".png");
  }


  //-------------------------------------------------------------
  void LoadCaptureImage(string texPath)
  {
    StartCoroutine(DownloadTexture(texPath, OnFinishDownLoadTexture));
  }

  void OnFinishDownLoadTexture(Texture2D tex)
  {
    Sprite sp = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
    GameObject.Find("Capture Image (ProjectList)").GetComponent<Image>().sprite = sp;
  }

  public IEnumerator DownloadTexture(string path, UnityAction<Texture2D> callback)
  {
    //StreamingAssetsまでのパスと内側のパスをつなげる
    var url = System.IO.Path.Combine(Application.streamingAssetsPath, path);
    url = "file://" + url;
    using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(url))
    {
      yield return www.SendWebRequest();
      if (www.isNetworkError || www.isHttpError)
      {
        Debug.Log(www.error);
        yield break;
      }
      Texture2D myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
      //コルーチンは返り値を持てないので、Actionで返す
      callback(myTexture);
    };
  }
}
