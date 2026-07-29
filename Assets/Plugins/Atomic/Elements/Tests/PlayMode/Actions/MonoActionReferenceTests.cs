using NUnit.Framework;
using UnityEngine;

namespace Atomic.Elements
{
    public sealed class MonoActionReferenceTests
    {
        [Test]
        public void InvokeWhenSomeActionsAreNull()
        {
            //Arrange:
            var a2 = new ActionSpy();
            var a1 = new ActionSpy();

            GameObject gameObject = new GameObject();
            var sceneAction = gameObject.AddComponent<MonoActionConfigurable>();
            sceneAction.actions = new IAction[]{null, a2, null, a1};
            var sceneActionReference = new MonoActionReference(sceneAction);
            
            //Act:
            sceneActionReference.Invoke();
            
            //Assert:
            Assert.IsTrue(a1.WasInvoked);
            Assert.IsTrue(a2.WasInvoked);
        }

        [Test]
        public void InvokeWhenValueIsNull()
        {
            //Arrange:
            var sceneActionReference = new MonoActionReference();
            
            //Act:
            sceneActionReference.Invoke();
        }
    }
}