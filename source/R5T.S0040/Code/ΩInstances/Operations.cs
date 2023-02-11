using System;


namespace R5T.S0040.Functionalities
{
    public sealed class Operations : IOperations
    {
        #region Infrastructure

        public static Operations Instance { get; } = new();

        private Operations()
        {
        }

        #endregion
    }
}
