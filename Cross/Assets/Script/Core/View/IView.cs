using UnityEngine;

namespace Core.View
{
    public interface IView
    {
        Canvas Canvas { get; }
        void Push();
        void Pop();
        void Open();
        void Close();
    }
}