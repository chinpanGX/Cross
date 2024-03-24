using System;
using App.Partial;
using Core.SaveData;

namespace App.Title
{
    internal static class PlayerProfileConstant
    {
        public static readonly string UserProfileKey = "UserInfo";
    }
    
    public readonly struct PlayerProfile
    {
        public readonly string UserId;
        public readonly string UserName;
        public readonly DateTime BeginGameTime;
        public readonly DateTime LastLoginTime;

        public PlayerProfile(string userId, string userName, DateTime beginGameTime, DateTime lastLoginTime)
        {
            UserId = userId;
            UserName = userName;
            BeginGameTime = beginGameTime;
            LastLoginTime = lastLoginTime;
        }
        
        public string ToSaveText()
        {
            var registerValue = UserId + "," + UserName + "," + BeginGameTime + "," + LastLoginTime;
            return registerValue;
        }
    }
} 