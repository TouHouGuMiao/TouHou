using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesManager
{
    private static ResourcesManager _instance=null;
    public static ResourcesManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new ResourcesManager();
            }
            return _instance;
        }
    }

    private string path = "UI/Panel";

    public GameObject LoadPanel(string name)
    {
      return LoadPrefab(name,path);
    }


    private string bulletPath = "BulletPrefab";
    public GameObject LoadBullet(string name)
    {
        GameObject go = LoadPrefab(name, bulletPath);
        return go;
    }

    private GameObject LoadPrefab(string name,string path)
    {
        string m_path = path + "/" + name;
        GameObject go = Resources.Load(m_path, typeof(GameObject)) as GameObject;
        if (go == null)
        {
            Debug.LogError(m_path + "is null");
            return null;
        }
        return go;
    }

    string atlasPath = "Atlas";



   public UIAtlas LoadAtlas(string name)
    {
        GameObject atlasGO = LoadPrefab(name, atlasPath);
        if (atlasGO == null)
        {
            Debug.LogError("atlas is null" + name);
            return null;
        }
        UIAtlas m_Atlas = atlasGO.GetComponent<UIAtlas>();
        return m_Atlas;
    }

   public GameObject LoadHeroModel(string name)
    {
        GameObject heroGo = LoadPrefab(name, "HeroPrefab");
        if (heroGo == null)
        {
            Debug.LogError("HeroPrefab is null");
            return null;
        }
        return heroGo;
    }
}
