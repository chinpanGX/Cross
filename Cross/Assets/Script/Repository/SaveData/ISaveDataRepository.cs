using System;
using App.Title;
using Core.SaveData;
using UnityEngine;

namespace Repository.SaveData
{
    public interface ISaveDataRepository
    {
        bool Load();
        void Register(string userName);
        void Save(PlayerProfile source);
        PlayerProfile Get();
    }

    public class PlayerProfileSaveDataLocalRepository : ISaveDataRepository
    {
        private readonly EncryptedPlayerPrefs playerPrefs;
        private PlayerProfile profile;

        public PlayerProfileSaveDataLocalRepository(EncryptedPlayerPrefs playerPrefs)
        {
            this.playerPrefs = playerPrefs;
        }
        
        public PlayerProfile Get()
        {
            return profile;
        }
        
        public bool Load()
        {
            if (!playerPrefs.TryLoad(PlayerProfileConstant.UserProfileKey, out var value))
                return false;

            var info = value.Split(",");
            var userId = info[0];
            var userName = info[1];
            var beginGameTime = DateTime.Parse(info[2]);
            var lastLoginTime = DateTime.Now;
            profile = new PlayerProfile(userId, userName, beginGameTime, lastLoginTime);
            return true;
        }
        
        public void Register(string registerUserName)
        {
            var userName = registerUserName;
            var userId = Guid.NewGuid().ToString();
            var beginGameTime = DateTime.Now;
            var lastLoginTime = DateTime.Now;
            profile = new PlayerProfile(userId, userName, beginGameTime, lastLoginTime); 
            Save(profile);
        }

        public void ApplyLoginTime()
        {
            var distPlayerProfile =
                new PlayerProfile(profile.UserId, profile.UserName, profile.BeginGameTime, DateTime.Now);
            profile = distPlayerProfile;
            Save(profile);
        }
        
        public void Save(PlayerProfile source)
        {
            var userId = profile.UserId;
            var beginGameTime = profile.BeginGameTime;
            
            var dest = new PlayerProfile(userId, source.UserName, beginGameTime, source.LastLoginTime);
            profile = dest;
            var value = profile.ToSaveText();
            playerPrefs.Save(PlayerProfileConstant.UserProfileKey, value);
        }
    }
}
