using NUnit.Framework;
using UnityEngine;

namespace Atomic.Elements
{
    [TestFixture]
    public sealed class MonoActionConfigurableTests
    {
        [Test]
        public void InvokeWhenSomeActionsAreNull()
        {
            //Arrange:
            var a2 = new ActionSpy();
            var a1 = new ActionSpy();

            var sceneAction = new GameObject().AddComponent<MonoActionConfigurable>();
            sceneAction.actions = new IAction[]{null, a2, null, a1};

            //Act:
            sceneAction.Invoke();

            //Assert:
            Assert.IsTrue(a1.WasInvoked);
            Assert.IsTrue(a2.WasInvoked);
        }

        [Test]
        public void InvokeWhenActionsNull()
        {
            //Arrange:
            var sceneAction = new GameObject().AddComponent<MonoActionConfigurable>();
            sceneAction.actions = new IAction[] {null};

            //Act:
            sceneAction.Invoke();
        }
    }
}