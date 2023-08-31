using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveLoadManager : Singleton<SaveLoadManager>
{
    private string jsonFolder;

    /// <summary>
    /// 已注册的Manager
    /// </summary>
    private List<ISaveable> saveableList = new List<ISaveable>();
    /// <summary>
    /// 名字及对应的游戏数据
    /// </summary>
    private Dictionary<string, GameSaveData> saveDataDict = new Dictionary<string, GameSaveData>();

    protected override void Awake()
    {
        base.Awake();
        jsonFolder = Application.persistentDataPath + "/SAVE";
    }

    private void OnEnable()
    {
        EventHandler.StartNewGameEvent += OnStartNewGameEvent;
    }
    private void OnDisable()
    {
        EventHandler.StartNewGameEvent -= OnStartNewGameEvent;
    }
    private void OnStartNewGameEvent(int obj)
    {
        //文件路径
        var resultPath = jsonFolder + "/data.sav";
        if (File.Exists(resultPath))//新开游戏 删除旧进度
        {
            File.Delete(resultPath);
        }
    }

    public void Register(ISaveable saveable)
    {
        saveableList.Add(saveable);
    }

    /// <summary>
    /// 保存数据
    /// </summary>
    public void Save()
    {
        //清空上次数据
        saveDataDict.Clear();
        //保存当前新数据
        foreach (var saveable in saveableList)
        {
            saveDataDict.Add(saveable.GetType().Name, saveable.GenerateSaveData());
        }

        var resultPath = jsonFolder + "/data.sav";

        //序列化字典 
        var jsonData = JsonConvert.SerializeObject(saveDataDict, Formatting.Indented);

        if (!File.Exists(resultPath))
        {
            Directory.CreateDirectory(jsonFolder);
        }

        File.WriteAllText(resultPath, jsonData);
    }

    /// <summary>
    /// 读取数据
    /// </summary>
    public void Load()
    {
        var resultPath = jsonFolder + "/data.sav";

        if (!File.Exists(resultPath)) return;

        var stringData = File.ReadAllText(resultPath);

        //反序列化 以字典形式
        var jsonData = JsonConvert.DeserializeObject<Dictionary<string, GameSaveData>>(stringData);

        foreach (var saveable in saveableList)
        {
            saveable.RestoreGameData(jsonData[saveable.GetType().Name]);
        }
    }

}
