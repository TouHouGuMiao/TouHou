using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarisaSkillManager
{
    private static MarisaSkillManager _instance = null;

    public static MarisaSkillManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new MarisaSkillManager();
            }
            return _instance;
        }
    }

    private MarisaPuGong PuGong;
    public void InitMarisaSkills()
    {
        PuGong = new global::MarisaPuGong();
        PuGong.Init();
    }

    public void ShowPuGong(Transform target,Transform point)
    {
        PuGong.Show(target,point);
    }

}

class MarisaPuGong
{
    private GameObject PuGongBullet;
    private CharacterPropBase marisaPro;

    public void Init()
    {
        PuGongBullet = ResourcesManager.Instance.LoadBullet("StarBullet");
        marisaPro = CharacterPropManager.Instance.GetCharcaterDataByName("marisa");
    }

    public void Show(Transform target,Transform point)
    {
        StarBullet m_star = new global::StarBullet();
        GameObject go = GameObject.Instantiate(PuGongBullet) as GameObject;
        go.transform.position = point.transform.position;
        m_star = go.GetComponent<StarBullet>();
        m_star.m_Type = BulletBase.BulletTpye.emptyBullet;
        m_star.target = target;

        m_star.injured = 10F+marisaPro.bulletPower;
        m_star.injured = CRTDepent(m_star.injured);
        //临时处理
        m_star.HP = 150f;
    }

    private float CRTDepent(float injured)
    {
        float CRTCount = (float)(Random.Range(0, 1.0f));
        if (marisaPro.CRT >= CRTCount)
        {
            injured = injured * marisaPro.CRTPower;
            return injured;
        }
        else
        {
            return injured;
        }
    }
}
