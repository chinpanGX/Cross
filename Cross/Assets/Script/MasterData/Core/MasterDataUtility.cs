using System;

namespace MasterData
{
    public static class MasterDataUtility
    {
        public static void CheckInvalidId(int id, int dataCount)
        {
            if (id< 0 || id > dataCount)
            {
                throw new ArgumentException($"指定されたIDは不正な値です{id}");
            }
        }
    }
}