using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class FadeManager : MonoBehaviour
{
  private static FadeManager _instance;
  public static FadeManager instance
  {
    get
    {
      if (!_instance)
      {
        var go = new GameObject("FadeManager");
        DontDestroyOnLoad(go);
        _instance = go.AddComponent<FadeManager>();
      }
      return _instance;
    }
  }
  private void Awake()
  {
    if (_instance == null) _instance = this;
    else Destroy(this);
  }

  public IEnumerator FadeIn(int frameNum)
  {
    float delta = 1.0f / frameNum;
    CanvasGroup canvasGroup = GameObject.Find("Canvas (Fade)").GetComponent<CanvasGroup>();
    for (float f = 1.0f; f >= 0.0f; f -= delta)
    {
      canvasGroup.alpha = f; // black level
      yield return null;
    }
  }

  public IEnumerator FadeOut(int frameNum)
  {
    float delta = 1.0f / frameNum;
    CanvasGroup canvasGroup = GameObject.Find("Canvas (Fade)").GetComponent<CanvasGroup>();
    for (float f = 0.0f; f <= 1.0f; f += delta)
    {
      canvasGroup.alpha = f; // black level
      yield return null;
    }
  }

  public IEnumerator FadeIn(int frameNum, UnityAction callback)
  {
    float delta = 1.0f / frameNum;
    CanvasGroup canvasGroup = GameObject.Find("Canvas (Fade)").GetComponent<CanvasGroup>();
    for (float f = 1.0f; f >= 0.0f; f -= delta)
    {
      canvasGroup.alpha = f; // black level
      yield return null;
    }
    callback();
  }

  public IEnumerator FadeOut(int frameNum, UnityAction callback)
  {
    float delta = 1.0f / frameNum;
    CanvasGroup canvasGroup = GameObject.Find("Canvas (Fade)").GetComponent<CanvasGroup>();
    for (float f = 0.0f; f <= 1.0f; f += delta)
    {
      canvasGroup.alpha = f; // black level
      yield return null;
    }
    callback();
  }



}