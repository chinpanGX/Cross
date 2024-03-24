using System;
using System.Threading;
using Cysharp.Threading.Tasks;
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
        
        private static LoginMaster cache = null;
        public static async Awaitable<LoginMaster> Load()
        {
            if (cache != null)
                return cache;

            CancellationTokenSource cancellationTokenSource = new();
            try
            {
                cache = await Resources.LoadAsync<LoginMaster>("MasterData/LoginMaster")
                    .ToUniTask(cancellationToken: cancellationTokenSource.Token) as LoginMaster;
            }
            catch (Exception e)
            {
                cancellationTokenSource.Cancel();
                cancellationTokenSource.Dispose();
            }
            return cache;    
        }
    }

    public static class LoginMasterExtension
    {
        public static DateTime ToDateTime(this LoginMaster master, DateTime nowTime)
        {
            return new DateTime(nowTime.Year, nowTime.Month, nowTime.Day, master.RefreshLoginHour, master.RefreshLoginMinute, master.RefreshLoginSecond);            
        }
    }
}
