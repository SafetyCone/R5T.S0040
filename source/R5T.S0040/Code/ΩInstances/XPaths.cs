using System;


namespace R5T.S0040
{
    public class XPaths : IXPaths
    {
        #region Infrastructure

        public static IXPaths Instance { get; } = new XPaths();


        private XPaths()
        {
        }

        #endregion
    }
}
