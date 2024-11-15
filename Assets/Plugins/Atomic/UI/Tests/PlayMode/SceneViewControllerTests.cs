using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace Atomic.UI
{
    public sealed class SceneViewControllerTests
    {
        private const string PREFAB_PATH = "Assets/Plugins/Atomic/UI/Tests/Prefabs/View.prefab";

        [UnityTest]
        public IEnumerator Lifecycle()
        {
            //Arrange:
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(PREFAB_PATH);
            var sceneController = GameObject.Instantiate(prefab).GetComponent<SceneViewController>();
            ViewControllerStub stub = sceneController.GetController<ViewControllerStub>();
            
            yield return null;
            
            //Assert:
            Assert.IsTrue(stub.wasInit);
            Assert.IsTrue(stub.wasEnable);
            Assert.IsTrue(stub.wasUpdate);
            
            Assert.AreEqual(nameof(ViewControllerStub.Init), stub.approvalSequence[0]);
            Assert.AreEqual(nameof(ViewControllerStub.Enable), stub.approvalSequence[1]);

            yield return new WaitForFixedUpdate();
            
            Assert.IsTrue(stub.wasFixedUpdate);
            Assert.IsTrue(stub.wasLateUpdate);

            sceneController.enabled = false;
            Assert.IsTrue(stub.wasDisable);
            
            GameObject.Destroy(sceneController.gameObject);
            
            yield return null;
            Assert.IsTrue(stub.wasDispose);
            
            Assert.AreEqual(nameof(ViewControllerStub.Disable), stub.approvalSequence[^2]);
            Assert.AreEqual(nameof(ViewControllerStub.Dispose), stub.approvalSequence[^1]);
        }
        
        [UnityTest]
        public IEnumerator EnableDisableController()
        {
            //Arrange:
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(PREFAB_PATH);
            var sceneController = GameObject.Instantiate(prefab).GetComponent<SceneViewController>();
            ViewControllerStub stub = sceneController.GetController<ViewControllerStub>();
            
            yield return null;
            
            //Assert:
            Assert.IsTrue(stub.wasInit);
            Assert.IsTrue(stub.wasEnable);
            Assert.IsTrue(stub.wasUpdate);
            
            //Act:
            sceneController.enabled = false;
            
            //Assert:
            Assert.IsTrue(stub.wasDisable);

            //Act:
            sceneController.enabled = true;
            
            //Assert:
            Assert.AreEqual(nameof(ViewControllerStub.Enable), stub.approvalSequence[^1]);
            
            //Finalize:
            GameObject.Destroy(sceneController.gameObject);
        }
        
        [UnityTest]
        public IEnumerator RemoveController()
        {
            //Arrange:
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(PREFAB_PATH);
            var sceneController = GameObject.Instantiate(prefab).GetComponent<SceneViewController>();
            ViewControllerStub stub = sceneController.GetController<ViewControllerStub>();
            
            yield return null;
            
            //Act:
            bool success = sceneController.DelController(stub);
            
            //Assert:
            Assert.IsTrue(success);
            Assert.IsTrue(stub.wasDisable);
            Assert.IsTrue(stub.wasDispose);
            Assert.AreEqual(nameof(ViewControllerStub.Disable), stub.approvalSequence[^2]);
            Assert.AreEqual(nameof(ViewControllerStub.Dispose), stub.approvalSequence[^1]);
            
            //Finalize:
            GameObject.Destroy(sceneController.gameObject);
        }
        
        

        [UnityTest]
        public IEnumerator AddControllerBeforeAwake()
        {
            //Arrange:
            var sceneController = new GameObject().AddComponent<SceneViewController>();

            ViewControllerStub stub = new ViewControllerStub();
            bool success = sceneController.AddController(stub);
            Assert.IsTrue(success);
            
            Assert.IsFalse(stub.wasInit);
            Assert.IsFalse(stub.wasEnable);

            yield return null;
            Assert.IsTrue(stub.wasInit);
            Assert.IsTrue(stub.wasEnable);
            Assert.AreEqual(nameof(ViewControllerStub.Init), stub.approvalSequence[0]);
            Assert.AreEqual(nameof(ViewControllerStub.Enable), stub.approvalSequence[1]);

            yield return null;
            Assert.IsTrue(stub.wasUpdate);
            yield return new WaitForFixedUpdate();
            Assert.IsTrue(stub.wasFixedUpdate);
            Assert.IsTrue(stub.wasLateUpdate);

            //Finalize:
            GameObject.Destroy(sceneController.gameObject);
            
            yield return null;
            Assert.IsTrue(stub.wasDisable);
            Assert.IsTrue(stub.wasDispose);
            
            Assert.AreEqual(nameof(ViewControllerStub.Disable), stub.approvalSequence[^2]);
            Assert.AreEqual(nameof(ViewControllerStub.Dispose), stub.approvalSequence[^1]);
        }

        [Test]
        public void WhenRemoveAbsentControllerThenFailed()
        {
            //Arrange:
            var sceneController = new GameObject().AddComponent<SceneViewController>();
            ViewControllerStub stub = new ViewControllerStub();
            
            //Act:
            bool success = sceneController.DelController(stub);
            
            //Assert:
            Assert.IsFalse(success);
            
            //Finalize:
            GameObject.Destroy(sceneController.gameObject);
        }

        [UnityTest]
        public IEnumerator AddControllerAfterStart()
        {
            //Arrange:
            var sceneController = new GameObject().AddComponent<SceneViewController>();
            yield return new WaitForSeconds(0.25f);

            ViewControllerStub stub = new ViewControllerStub();
            bool success = sceneController.AddController(stub);
            Assert.IsTrue(success);
            
            Assert.IsTrue(stub.wasInit);
            Assert.IsTrue(stub.wasEnable);
            Assert.AreEqual(nameof(ViewControllerStub.Init), stub.approvalSequence[0]);
            Assert.AreEqual(nameof(ViewControllerStub.Enable), stub.approvalSequence[1]);

            yield return null;
            Assert.IsTrue(stub.wasUpdate);
            yield return new WaitForFixedUpdate();
            Assert.IsTrue(stub.wasFixedUpdate);
            Assert.IsTrue(stub.wasLateUpdate);

            //Finalize:
            GameObject.Destroy(sceneController.gameObject);
        }

        [Test]
        public void WhenGetAbsentControllerThenReturnNull()
        {
            //Arrange:
            var sceneController = new GameObject().AddComponent<SceneViewController>();

            //Act:
            ViewControllerStub stub = sceneController.GetController<ViewControllerStub>();
            
            //Assert:
            Assert.IsNull(stub);
            
            //Finalize:
            GameObject.Destroy(sceneController.gameObject);
        }
    }
}