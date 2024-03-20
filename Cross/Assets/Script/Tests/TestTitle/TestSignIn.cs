using System;
using App.Partial;
using NUnit.Framework;

namespace Test.Title
{
    public class TestSignIn
    {
        [Test]
        public void TestRegisterUserInfo()
        {
            var profile = new TestPlayerProfile();
            profile.Register();
            var a = profile.Save();
            var b = profile.UserName + "," + profile.UserId + "," + profile.BeginGameTime + "," + profile.LastLoginTime; 
            Assert.That(a, Is.EqualTo(b));
        }

        [Test]
        public void LoadUserInfo()
        {
            var profile = new TestPlayerProfile();
            profile.Register();
            var info = profile.Save();
            var word = info.Split(",");
            var date = new DateTime(2024, 3, 12, 12, 30, 0).ToShortString();
            Assert.That(word[0], Is.EqualTo("Test"));
            Assert.That(word[1], Is.EqualTo("36eea745-c142-469e-9105-b422a3f55914"));
            Assert.That(word[2], Is.EqualTo(date));
            Assert.That(word[3], Is.EqualTo(date));
        }
    }

    internal class TestPlayerProfile
    {
        public string UserId;
        public string UserName;
        public DateTime BeginGameTime;
        public DateTime LastLoginTime;
        
        public void Register()
        {
            UserName = "Test";
            UserId = "36eea745-c142-469e-9105-b422a3f55914";
            var date = new DateTime(2024, 3, 12, 12, 30, 0);
            BeginGameTime = date;
            LastLoginTime = date;
        }

        public string Save()
        {
            var registerValue = UserName + "," + UserId + "," + BeginGameTime.ToShortString() + "," + LastLoginTime.ToShortString();
            return registerValue;
        }
    }
}