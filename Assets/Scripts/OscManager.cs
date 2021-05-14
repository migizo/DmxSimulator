using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using OscJack;

public class OscManager : MonoBehaviour
{
  bool enableOscTest = true;
  OscServer server;
  Queue<KeyValuePair<int, int>> oscDmxQueue = new Queue<KeyValuePair<int, int>>();

  DataConfigManager dataConfigManager;
  DmxManager dmxManager;

  void Awake()
  {
    dataConfigManager = GameObject.Find("DataConfigManager").GetComponent<DataConfigManager>();
    dmxManager = GameObject.Find("DmxManager").GetComponent<DmxManager>();
  }
  public void Setup()
  {
    // protocol: Osc
    server = new OscServer(dataConfigManager.Get().oscPort); // Port number
    server.MessageDispatcher.AddCallback(
      "/dmx",
      (string address, OscDataHandle data) =>
        {
          // if (oscDmxList.ContainsKey(data.GetElementAsInt(0))) return;
          oscDmxQueue.Enqueue(new KeyValuePair<int, int>(data.GetElementAsInt(0), data.GetElementAsInt(1)));
        }
    );
  }

  public void Dispose()
  {
    if (server != null) server.Dispose();
  }

  private void OnDestroy()
  {
    Dispose();
  }

  // Update is called once per frame
  private void Update()
  {
    while (oscDmxQueue.Count != 0)
    {
      var pair = oscDmxQueue.Dequeue();
      int ch = pair.Key;
      int val = pair.Value;
      dmxManager.Apply(ch, val);
    }
  }
}
