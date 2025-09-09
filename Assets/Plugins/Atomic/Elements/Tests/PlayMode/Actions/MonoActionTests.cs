using NUnit.Framework;
using UnityEngine;

namespace Atomic.Elements
{
    [TestFixture]
    public sealed class SceneActionDefaultTests
    {
        [Test]
        public void InvokeWhenSomeActionsAreNull()
        {
            //Arrange:
            var a2 = new ActionStub();
            var a1 = new ActionStub();

            var sceneAction = new GameObject().AddComponent<SceneActionDefault>();
            sceneAction.actions = new IAction[]{null, a2, null, a1};

            //Act:
            sceneAction.Invoke();

            //Assert:
            Assert.IsTrue(a1.wasInvoke);
            Assert.IsTrue(a2.wasInvoke);
        }

        [Test]
        public void InvokeWhenActionsNull()
        {
            //Arrange:
            var sceneAction = new GameObject().AddComponent<SceneActionDefault>();
            sceneAction.actions = new IAction[] {null};

            //Act:
            sceneAction.Invoke();
        }
    }
}