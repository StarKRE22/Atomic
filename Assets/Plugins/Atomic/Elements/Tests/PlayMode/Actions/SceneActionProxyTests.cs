using NUnit.Framework;
using UnityEngine;

namespace Atomic.Elements
{
    public sealed class SceneActionProxyTests
    {
        [Test]
        public void InvokeWhenSomeActionsAreNull()
        {
            //Arrange:
            var a2 = new ActionStub();
            var a1 = new ActionStub();

            GameObject gameObject = new GameObject();
            var sceneAction = gameObject.AddComponent<SceneActionDefault>();
            sceneAction.Construct(null, a2, null, a1);
            var sceneActionReference = new SceneActionProxy(sceneAction);
            
            //Act:
            sceneActionReference.Invoke();
            
            //Assert:
            Assert.IsTrue(a1.wasInvoke);
            Assert.IsTrue(a2.wasInvoke);
        }

        [Test]
        public void InvokeWhenValueIsNull()
        {
            //Arrange:
            var sceneActionReference = new SceneActionProxy();
            
            //Act:
            sceneActionReference.Invoke();
        }
    }
}