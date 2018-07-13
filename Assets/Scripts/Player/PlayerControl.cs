using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : CharacterPropBase {
    public Animator m_Animator;
    public float rate=0;
    private float tempTime;

    private TweenPosition m_TP1;
    private TweenPosition m_TP2;

    private UISlider m_Slider;
    private float m_HP;  //表示血条现有HP,而不是HP属性



    void Start ()
    {
        m_Animator = this.GetComponent<Animator>();
        GameObject yinYangYu1 = transform.FindRecursively("YinYangYu1").gameObject;
        GameObject yinYangYu2 = transform.FindRecursively("YinYangYu2").gameObject;
        m_TP1 = yinYangYu1.GetComponent<TweenPosition>();
        m_TP2 = yinYangYu2.GetComponent<TweenPosition>();
        m_HP = HP;
        TPEffectSet();
        PlayerSkillManager.Instance.InitPlayerSkillManager();

        UpDataPlayerPro();
    }
	
	void Update ()
    {
        CharacterControl();
        YinYangYuControl();

        UpDataPlayerPro();//测试用
    }

    float deltaTime = 0;
    void CharacterControl()
    {
        if (Input.GetKey(KeyCode.D))
        {

            deltaTime += 1 * Time.deltaTime;
            AnimatorStateInfo stateInfo = m_Animator.GetCurrentAnimatorStateInfo(0);
            if (deltaTime >= 0.3f)
            {
                if (stateInfo.IsName("Base Layer.idle"))
                {
                    m_Animator.SetBool("move", true);
                    m_Animator.SetBool("rightMove", true);

                }
            }
            if (stateInfo.IsName("Base Layer.idle") || stateInfo.IsName("Base Layer.Move.RightMoveLoop") || stateInfo.IsName("Base Layer.Move.RightMoveBegin"))
            {
                transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime * speed);
            }



        }

        else if (Input.GetKeyUp(KeyCode.D))
        {
            m_Animator.SetBool("rightMove", false);
            m_Animator.SetBool("move", false);
            deltaTime = 0; 
        }

        if (Input.GetKey(KeyCode.A))
        {
            AnimatorStateInfo stateInfo = m_Animator.GetCurrentAnimatorStateInfo(0);
            deltaTime += 1 * Time.deltaTime;
            if (deltaTime >= 0.3f)
            {
                if (stateInfo.IsName("Base Layer.idle"))
                {
                    m_Animator.SetBool("move", true);
                    m_Animator.SetBool("leftMove", true);
                }
            }
         
            if(stateInfo.IsName("Base Layer.idle")||stateInfo.IsName("Base Layer.Move.LeftMoveLoop"))
            {
                transform.Translate(new Vector3(-1, 0, 0) * Time.deltaTime * speed);
            }
         
        }

        else if (Input.GetKeyUp(KeyCode.A))
        {
            m_Animator.SetBool("leftMove", false);
            m_Animator.SetBool("move", false);
            deltaTime = 0;
        }

        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(new Vector3(0, 1, 0) * Time.deltaTime * speed * 0.6f); ;
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(new Vector3(0, -1, 0) * Time.deltaTime * speed*0.6f);
        }

        if (tempTime < Time.time)
        {
            tempTime = Time.time;
        }
       
        if (Input.GetKey(KeyCode.Mouse0)&&Time.time>=tempTime)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos); 
            PlayerSkillManager.Instance.ShowPuGong(mousePos, m_TP1.transform, m_TP2.transform);
            tempTime += rate;
        }

    }

    private void UpDataPlayerPro()
    {
        CharacterPropBase baseData = new CharacterPropBase();
        baseData = CharacterPropManager.Instance.GetCharcaterDataByName("reimu");
        if (baseData == null)
        {
            Debug.LogError("baseData is null");
            return;
        }
        Power = baseData.Power;
        DEF = baseData.DEF;
        DEX = baseData.DEX;
        VIT = baseData.VIT;
        Lucky = baseData.Lucky;

    }
   
    void YinYangYuControl()
    {
        m_TP1.transform.Rotate(new Vector3(0, 0, -1) * Time.deltaTime, 10);
        m_TP2.transform.Rotate(new Vector3(0, 0, -1) * Time.deltaTime, 10);
    }

    #region   阴阳玉循环特效;
    private void TP1PingPangOneStop()
    {
        SpriteRenderer m_Render = m_TP1.GetComponent<SpriteRenderer>();
        m_Render.sortingLayerName = "Bounds";
    }

    private void TP1PingPangTwoStop()
    {
        SpriteRenderer m_Render = m_TP1.GetComponent<SpriteRenderer>();
        m_Render.sortingLayerName = "warriorCenter";
    }

    private void TP2PingPangOneStop()
    {
        SpriteRenderer m_Render = m_TP2.GetComponent<SpriteRenderer>();
        m_Render.sortingLayerName = "warriorCenter";
    }

    private void TP2PingPangTwoStop()
    {
        SpriteRenderer m_Render = m_TP2.GetComponent<SpriteRenderer>();
        m_Render.sortingLayerName = "Bounds";
    }


    private void TPEffectSet()
    {
        m_TP1.PingPangOneStop += TP1PingPangOneStop;
        m_TP1.PingPangTwoStop += TP1PingPangTwoStop;

        m_TP2.PingPangOneStop += TP2PingPangOneStop;
        m_TP2.PingPangTwoStop += TP2PingPangTwoStop;
    }

    #endregion



    private void OnTriggerEnter(Collider other)
    {
        BulletBase m_Base = other.GetComponent<BulletBase>();
        if (m_Base == null)
        {
            return;
        }

        if (m_Base.m_Type == BulletBase.BulletTpye.emptyBullet)
        {
            float injured = m_Base.injured;
            injured = injured * defenseLV;
            m_HP= BattleCommoUIManager.Instance.UpdataHP_Player(m_HP, HP, injured, -1);
            GameObject.Destroy(m_Base.gameObject);
        }
    }


}
