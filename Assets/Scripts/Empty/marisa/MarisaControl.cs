using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarisaControl : CharacterPropBase {
    public Transform target;

    private GameObject prefab;
    private Transform point;
    private float m_HP;
	// Use this for initialization
	void Start () {
  

        prefab = ResourcesManager.Instance.LoadBullet("StarBullet");
        point = transform.FindRecursively("point");
        MarisaSkillManager.Instance.InitMarisaSkills();
        m_HP = HP;
    }
	
	// Update is called once per frame
	void Update () {


        if (target == null)
        {
            FindPlayerByTag();
        }


	}

   

    private void FindPlayerByTag()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }


    #region 帧事件方法调用
    public void UsePuGong()
    {
        MarisaSkillManager.Instance.ShowPuGong(target,point);
    }
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        BulletBase m_Base = other.GetComponent<BulletBase>();
        if (m_Base == null)
        {
            return;
        }

        if (m_Base.m_Type == BulletBase.BulletTpye.playerBullet)
        {
            float injured = m_Base.injured;
            injured = injured * defenseLV;
            m_HP = BattleCommoUIManager.Instance.UpdataHP_Boss("marisa", m_HP, injured, -1);
            GameObject.Destroy(m_Base.gameObject);
        }
    }
}
