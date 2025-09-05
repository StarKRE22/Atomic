using NUnit.Framework;
using UnityEngine;

namespace Atomic.Entities
{
    public partial class EntityViewTests
    {
        [Test]
        public void Name_ReturnsCustomName_WhenOverrideNameEnabled()
        {
            var go = new GameObject("OriginalName");
            var view = go.AddComponent<EntityView>();

            view.overrideName = true;
            view.customName = "CustomName";

            Assert.AreEqual("CustomName", view.Name);
            Object.DestroyImmediate(go);
        }

        [Test]
        public void Name_ReturnsGameObjectName_WhenOverrideNameDisabled()
        {
            var go = new GameObject("OriginalName");
            var view = go.AddComponent<EntityView>();
            view.overrideName = false;
            
            Assert.AreEqual("OriginalName", view.Name);
            Object.DestroyImmediate(go);
        }
    }
}