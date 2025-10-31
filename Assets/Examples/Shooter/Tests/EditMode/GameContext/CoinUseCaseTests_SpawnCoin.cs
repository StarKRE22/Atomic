// using Atomic.Elements;
// using Atomic.Entities;
// using NUnit.Framework;
// using UnityEngine;
// using Random = UnityEngine.Random;
//
// namespace BeginnerGame
// {
//     [TestFixture]
//     public partial class CoinsUseCaseTests
//     {
//         [Test]
//         public void SpawnCoin_Successfully()
//         {
//             Vector3 areaCenter = Vector3.zero;
//             Vector3 areaSize = new Vector3(5, 0, 5);
//             
//             //Arrange:
//             var gameContext = new TestGameContext();
//             gameContext.AddCoinSpawnArea(new Bounds(areaCenter, areaSize));
//             gameContext.AddCoinPool(new EntityPoolMock<IGameEntity>
//             {
//                 RentMethod = () =>
//                 {
//                     IGameEntity coin = new TestActor();
//                     coin.AddCoinTag();
//                     coin.AddPosition(new ReactiveVariable<Vector3>());
//                     return coin;
//                 }
//             });
//             
//             Random.InitState(12345);
//             
//             //Act:
//             IGameEntity coin = CoinsUseCase.Spawn(gameContext);
//             var coinPos = coin.GetPosition().Value;
//             
//             //Assert:
//             Assert.That(
//                 coinPos.x,
//                 Is.InRange(areaCenter.x - areaSize.x / 2f, areaCenter.x + areaSize.x / 2f),
//                 "Coin X position is outside area bounds"
//             );
//
//             Assert.That(
//                 coinPos.z,
//                 Is.InRange(areaCenter.z - areaSize.z / 2f, areaCenter.z + areaSize.z / 2f),
//                 "Coin Z position is outside area bounds"
//             );
//             
//             Random.InitState(12345);
//             Vector3 coinPos2 = CoinsUseCase.RandomSpawnPosition(gameContext);
//             Assert.That(coinPos2, Is.EqualTo(coinPos), "Coin position should be deterministic with fixed seed");
//         }
//     }
// }