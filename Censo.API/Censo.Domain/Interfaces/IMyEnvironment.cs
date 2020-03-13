namespace Censo.Domain.Interfaces
{
    using System;

    public interface IMyEnvironment
    {
        string GetVariable(string variable);
    }
}
