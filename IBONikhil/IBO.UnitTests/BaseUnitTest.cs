using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace IBO.UnitTests
{
    public abstract class BaseUnitTest
    {
        public MockRepository MockBaseRepository { get; private set; }

    }
}
