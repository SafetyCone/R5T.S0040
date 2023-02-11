using System;


namespace R5T.S0040.Functionalities
{
    public interface IOperations :
        S0040.IOperations,
        Library.IOperations
    {
        public new void Test()
        {
            (this as Library.IOperations).Test();
        }
    }
}
