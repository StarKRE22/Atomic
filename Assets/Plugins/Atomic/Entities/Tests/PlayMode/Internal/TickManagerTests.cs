using System.Collections;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Atomic.Entities
{
    public class TickManagerTests
    {
        [UnityTest]
        public IEnumerator Should_Invoke_OnUpdate()
        {
            // Arrange
            var updatable = new TickSourceDummy();
            TickManager.Instance.Register(updatable);

            // Act
            yield return new WaitForSeconds(0.1f); // Wait for a few frames

            // Assert
            Assert.Greater(updatable.UpdateCount, 0);
        }

        [UnityTest]
        public IEnumerator Should_Invoke_OnFixedUpdate()
        {
            // Arrange
            var updatable = new TickSourceDummy();
            TickManager.Instance.Register(updatable);

            // Act
            yield return new WaitForFixedUpdate();
            yield return new WaitForFixedUpdate();

            // Assert
            Assert.Greater(updatable.FixedCount, 0);
        }

        [UnityTest]
        public IEnumerator Should_Invoke_OnLateUpdate()
        {
            // Arrange
            var updatable = new TickSourceDummy();
            TickManager.Instance.Register(updatable);

            // Act
            yield return new WaitForEndOfFrame(); // Triggers LateUpdate

            // Assert
            Assert.Greater(updatable.LateCount, 0);
        }

        [UnityTest]
        public IEnumerator Should_StopCallingAfterRemove()
        {
            // Arrange
            var updatable = new TickSourceDummy();
            TickManager.Instance.Register(updatable);

            yield return new WaitForSeconds(0.1f);
            int before = updatable.UpdateCount;

            TickManager.Instance.Unregister(updatable);

            yield return new WaitForSeconds(0.1f);
            int after = updatable.UpdateCount;

            // Assert
            Assert.AreEqual(before, after);
        }

        [UnityTest]
        public IEnumerator ShouldIgnoreNullAdd()
        {
            // Act — не должно выбросить исключение
            TickManager.Instance.Register(null);

            // Wait to allow Unity frame update
            yield return null;
            Assert.Pass(); // если не упало — тест успешен
        }

        [UnityTest]
        public IEnumerator ShouldIgnoreNullDel()
        {
            TickManager.Instance.Unregister(null);
            yield return null;
            Assert.Pass();
        }

        [UnityTest]
        public IEnumerator ShouldNotAddDuplicate()
        {
            // Arrange
            var updatable = new TickSourceDummy();
            var manager = TickManager.Instance;

            // Act
            manager.Register(updatable);
            manager.Register(updatable); // второй раз
            
            // Assert: между двумя моментами вызов должен увеличиться только на 1 шаг
            Assert.AreEqual(1, manager._updatables.Count(it => Equals(it, updatable)));
            yield break;
        }

        [UnityTest]
        public IEnumerator ShouldStopCallingUpdate_AfterDel()
        {
            var updatable = new TickSourceDummy();
            var manager = TickManager.Instance;

            manager.Register(updatable);

            // Wait and collect count
            yield return new WaitForSeconds(0.05f);
            int before = updatable.UpdateCount;

            manager.Unregister(updatable);

            yield return new WaitForSeconds(0.05f);
            int after = updatable.UpdateCount;

            Assert.AreEqual(before, after, "UpdateManager should stop calling after Del()");
        }

        [UnityTest]
        public IEnumerator ShouldCallAllThreeUpdates()
        {
            var updatable = new TickSourceDummy();
            var manager = TickManager.Instance;
            manager.Register(updatable);

            // Act
            yield return new WaitForSeconds(0.05f); // Update
            yield return new WaitForFixedUpdate(); // FixedUpdate
            yield return new WaitForEndOfFrame(); // LateUpdate

            // Assert
            Assert.Greater(updatable.UpdateCount, 0);
            Assert.Greater(updatable.FixedCount, 0);
            Assert.Greater(updatable.LateCount, 0);
        }
    }
}