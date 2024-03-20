using System;
using NUnit.Framework;
using R3;
using Repository.GameTime;


namespace Test.Repository
{
    public class TestLogin
    {
        [Test]
        public void TestLoginRefresh()
        {
            var nowTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 0, 0);

            var loginRefreshTime = new LoginRefreshTimeData(12, 0, 0);
            using var gameRepository = new GameTimeLocalRepository(loginRefreshTime);
            var subscribeCount = 0;
            
            gameRepository.OnRefreshLogin.Subscribe(_ =>
            {
                subscribeCount += 1;
            });
            gameRepository.Apply(new GameTimeData(nowTime));
            
            Assert.That(subscribeCount, Is.EqualTo(1));
            Assert.That(loginRefreshTime.RefreshTime, Is.EqualTo(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 0, 0)));
            Assert.That(nowTime.Subtract(loginRefreshTime.RefreshTime).ToString(), Is.EqualTo("00:00:00"));
        }
        
        [Test]
        public void TestNotYetTimeLoginRefresh()
        {
            var nowTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 11, 59, 59);

            var loginRefreshTime = new LoginRefreshTimeData(12, 0, 0);
            using var gameRepository = new GameTimeLocalRepository(loginRefreshTime);
            
            var subscribeCount = 0;
            
            gameRepository.OnRefreshLogin.Subscribe(_ =>
            {
                subscribeCount += 1;
            });

            gameRepository.Apply(new GameTimeData(nowTime));
            
            Assert.That(subscribeCount, Is.EqualTo(0));
            Assert.That(nowTime, Is.EqualTo(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 11, 59, 59)));
            Assert.That(loginRefreshTime.RefreshTime, Is.EqualTo(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 0, 0)));
            Assert.That(nowTime.Subtract(loginRefreshTime.RefreshTime).ToString(), Is.EqualTo("-00:00:01"));
        }

        [Test]
        public void TestBeforeDay()
        {
            var nowTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day - 1, 12, 0, 0);

            var loginRefreshTime = new LoginRefreshTimeData(12, 0, 0);
            using var gameRepository = new GameTimeLocalRepository(loginRefreshTime);
            var subscribeCount = 0;
            
            gameRepository.OnRefreshLogin.Subscribe(_ =>
            {
                subscribeCount += 1;
            });
            gameRepository.Apply(new GameTimeData(nowTime));
            
            Assert.That(subscribeCount, Is.EqualTo(0));
            Assert.That(loginRefreshTime.RefreshTime, Is.EqualTo(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 0, 0)));
            Assert.That(nowTime.Subtract(loginRefreshTime.RefreshTime).Days, Is.EqualTo(-1));
        }
        
        [Test]
        public void TestNextDay()
        {
            var nowTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 1, 11, 59, 59);

            var loginRefreshTime = new LoginRefreshTimeData(12, 0, 0);
            using var gameRepository = new GameTimeLocalRepository(loginRefreshTime);
            var subscribeCount = 0;
            
            gameRepository.OnRefreshLogin.Subscribe(_ =>
            {
                subscribeCount += 1;
            });
            gameRepository.Apply(new GameTimeData(nowTime));
            
            Assert.That(subscribeCount, Is.EqualTo(1));
            Assert.That(loginRefreshTime.RefreshTime, Is.EqualTo(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 0, 0)));
            Assert.That(nowTime.Subtract(loginRefreshTime.RefreshTime).Days, Is.EqualTo(0));
        }
    }
}
