using System;
using App.Title;
using Core.SaveData;
using UnityEngine;

namespace Repository.SaveData
{
    public interface ISaveDataRepository
    {
        void Register(string userName);
        void ApplyLoginTime();
        bool UserRegistered { get; }
        PlayerProfile PlayerProfile { get; }
    }

    public class PlayerProfileSaveDataLocalRepository : ISaveDataRepository
    {
        private readonly EncryptedPlayerPrefs playerPrefs;
        private PlayerProfile profile;
        private bool userRegistered;
        
        public PlayerProfile PlayerProfile => profile; 
        public bool UserRegistered => userRegistered;

        public PlayerProfileSaveDataLocalRepository(EncryptedPlayerPrefs playerPrefs)
        {
            this.playerPrefs = playerPrefs;
            Load();
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
        
        private void Load()
        {
            if (!playerPrefs.TryLoad(PlayerProfileConstant.UserProfileKey, out var value))
            {
                userRegistered = false;
                return;    
            }
            
            var info = value.Split(",");
            var userId = info[0];
            var userName = info[1];
            var beginGameTime = DateTime.Parse(info[2]);
            var lastLoginTime = DateTime.Now;
            profile = new PlayerProfile(userId, userName, beginGameTime, lastLoginTime);
            userRegistered = true;
        }
        
        void Save(PlayerProfile source)
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
