﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPanel : IView
{

    private UIButton ChouKaBtn;
    private UIButton CardsGroundBtn;
    private UIButton BattleBtn;
    public PlayerPanel()
    {
        m_Layer = Layer.city;
    }

    protected override void OnDestroy()
    {
       
    }

    protected override void OnHide()
    {
       
    }

    protected override void OnShow()
    {
        
    }

    protected override void OnStart()
    {
        BattleBtn = this.GetChild("BattleBtn").GetComponent<UIButton>();
        CardsGroundBtn = this.GetChild("CardBtn").GetComponent<UIButton>();
        ChouKaBtn = this.GetChild("chouKaBtn").GetComponent<UIButton>();
        AddEventDelete();
    }


    private void AddEventDelete()
    {
        EventDelegate ChouKaClick = new global::EventDelegate(OnChouKaBtnClick);
        EventDelegate BattleClick = new EventDelegate(OnBattleBtnClick);
        EventDelegate CardsClick = new EventDelegate(OnCardsGroundBtnClick);

        ChouKaBtn.onClick.Add(ChouKaClick);
        BattleBtn.onClick.Add(BattleClick);
        CardsGroundBtn.onClick.Add(CardsClick);
    }

    void OnChouKaBtnClick()
    {
        GUIManager.ShowView("ChouKaPanel");
    }

    void OnBattleBtnClick()
    {

        GameStateManager.LoadScene(3);
        GUIManager.ShowView("LoadingPanel");
        LoadingPanel.LoadingName = "BattleUIPanel";
    }

    void OnCardsGroundBtnClick()
    {
        GUIManager.ShowView("CardsPanel");
    }
}
