using System;

namespace Repository.GameTime
{
    public  class GameTimeDirector
    {
        public static GameTimeData GameTimeData;
        
        public static void Init()
        {
            GameTimeData = new GameTimeData(DateTime.Now);
        }
    }
}