using System;
using GluonGui.WorkspaceWindow.Views.WorkspaceExplorer.Explorer.Operations;

namespace Repository.GameTime
{
    /// <summary>
    /// ゲーム内の時間
    /// </summary>
    public readonly struct GameTimeData
    {
        public readonly DateTime DateTime;

        public GameTimeData(DateTime time)
        {
            if (time == DateTime.MinValue)
            {
                throw new ArgumentException($"DateTimeがデフォルト値です。{time}");
            }
            DateTime = time;
        }
    }
}