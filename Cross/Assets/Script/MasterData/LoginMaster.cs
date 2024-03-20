using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MasterData
{
    [CreateAssetMenu(fileName = "LoginMaster", menuName = "MasterData/Login")]
    public class LoginMaster : ScriptableObject
    {
        [SerializeField] private int refreshLoginHour;
        [SerializeField] private int refreshLoginMinute;
        [SerializeField] private int refreshLoginSecond;
        [SerializeField] private string refreshLoginMessage;

        public int RefreshLoginHour => refreshLoginHour;
        public int RefreshLoginMinute => refreshLoginMinute;
        public int RefreshLoginSecond => refreshLoginSecond;
        public string RefreshLoginMessage => refreshLoginMessage;
    }
}
