using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IEmtorManager : MonoBehaviour
{
    public static IEmtorManager Instance;

    private void Awake()
    {
        Instance = this;
    }


}
