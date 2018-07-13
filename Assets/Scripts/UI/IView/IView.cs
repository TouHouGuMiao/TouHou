﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Layer
{
    bottom =0,
    normal=10,
    city =20,
    UI=30,
}

public abstract  class IView
{
    public Layer m_Layer = Layer.normal;
    public void Start()
    {
        OnStart();
    }

    public void Show()
    {
        OnShow();
    }

    public void Hide()
    {
        OnHide();
    }

    public void Destroy()
    {
        OnDestroy();
    }


    public virtual void Update()
    {

    }




    protected abstract void OnStart();

    protected abstract void OnHide();

    protected abstract void OnShow();

    protected abstract void OnDestroy(); 
}
