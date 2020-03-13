namespace Censo.Infra.Environment
{
    using System;
    using Domain.Interfaces;

    public class MyEnvironment : IMyEnvironment
    {
        public string GetVariable(string variable)
        {
            var value = Environment.GetEnvironmentVariable(variable);
            if (string.IsNullOrEmpty(value))
                throw new ApplicationException($"System variable {variable} was not set!");
            return value;
        }
    }
}
