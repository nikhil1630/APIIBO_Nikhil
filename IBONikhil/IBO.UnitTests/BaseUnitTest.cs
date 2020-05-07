using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Ploeh.AutoFixture;

namespace IBO.UnitTests
{
    public abstract class BaseUnitTest
    {
        public MockRepository MockBaseRepository { get; private set; }
        public virtual Fixture TestFixture { get; private set; }

        protected BaseUnitTest()
        {
            MockBaseRepository = new MockRepository(MockBehavior.Strict);
            TestFixture = new Fixture();
        }
    }
}
