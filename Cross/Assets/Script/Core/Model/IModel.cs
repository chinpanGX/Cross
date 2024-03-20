using System;

namespace Core.Model
{
    public interface IModel : IDisposable
    {
        public void Execute();
    }
}