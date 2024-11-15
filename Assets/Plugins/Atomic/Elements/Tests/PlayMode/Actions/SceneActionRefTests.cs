using NUnit.Framework;
using UnityEngine;

namespace Atomic.Elements
{
    public sealed class SceneActionRefTests
    {
        [Test]
        public void InvokeWhenSomeActionsAreNull()
        {
            //Arrange:
            var a2 = new ActionStub();
            var a1 = new ActionStub();

            GameObject gameObject = new GameObject();
            var sceneAction = gameObject.AddComponent<SceneAction>().Compose(null, a2, null, a1);
            var sceneActionReference = new SceneActionRef(sceneAction);
            
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
            var sceneActionReference = new SceneActionRef();
            
            //Act:
            sceneActionReference.Invoke();
        }
    }
}