using System;

namespace Rope.Model
{
    public interface ICloseWindows
    {
        event Action Closed;
        void Close();
    }
}
