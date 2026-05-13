using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTS2026_GrupaA.Test
{
    internal class GameTest
    {
        [TestFixture]
        public class GameTest
        {
            private Game game;

            private Position P(int x, int y, int z)
            {
                return new Position(x, y, z);
            }

            private void AssertPosition(Position actual, int expectedX, int expectedY, int expectedZ)
            {
                Assert.That(actual.X, Is.EqualTo(expectedX));
                Assert.That(actual.Y, Is.EqualTo(expectedY));
                Assert.That(actual.Z, Is.EqualTo(expectedZ));
            }

            [SetUp]
            public void SetUp()
            {
                game = new Game(P(1, 1, 1), P(2, 2, 2));
            }

            // F1


            [Test]
            public void Game_ValidPositions_CreatesGame()
            {
                Position playerPosition = P( , , );
                Position revealItemPosition = P( , , );

                Game createdGame = new Game(playerPosition, revealItemPosition);

                Assert.That(createdGame, Is.Not.Null);
                Assert.That(createdGame.Player, Is.Not.Null);
                Assert.That(createdGame.Map, Is.Not.Null);
            }

            [TestCase(/* playerX */, /* playerY */, /* playerZ */,
                  /* itemX */,   /* itemY */,   /* itemZ */)]

            public void Game_InvalidPositions_ThrowsPositionOutsideOfMapException(
            int playerX, int playerY, int playerZ,
            int itemX, int itemY, int itemZ)

            {
                Position playerPosition = P(playerX, playerY, playerZ);
                Position revealItemPosition = P(itemX, itemY, itemZ);

                var ex = Assert.Throws<PositionOutsideOfMapException>(() =>
                    new Game(playerPosition, revealItemPosition));
                Assert.That(ex.Message, Is.EqualTo("Positions must be valid!"));
            }

            //F2

            [Test]
            public void MovePlayer_VakidMove_PlayerPositionChanged()
            {
                game = new GameTest(P( , , ), P(2,2,2)); 
                game.MovePlayer(/* Move.Right / Move.Up */);
                AssertPosition(game.Player.Position, , );
            }

            [Test]
            public void MovePlayer_MoveOutsideMap_PlayerPositionNotChanged()
            {
                game = new Game(P( , , ), P(2, 2, 2));
                game.MovePlayer(/* potez koji izlazi van mape */);
                AssertPosition(game.Player.Position, , );
            }

            [Test]
            public void MovePlayer_MoveIntoBarrier_PlayerPositionNotChanged()
            {
                game = new Game(P( , , ), P(2, 2, 2));
                game.MovePlayer(/* potez ka prepreci */);
                AssertPosition(game.Player.Position, );
            }

            //F3

            [Test]
            public void ValidatePosition_StandardTile_ReturnsTrue()
            {
                Position position = P( , , );
                bool result = game.ValidatePosition(position);
                Assert.That(result, Is.True);
            }

            [Test]
            public void ValidatePosition_PositionOutsideMap_ReturnsFalse()
            {
                Position position = P( , , );
                bool result = game.ValidatePosition(position);
                Assert.That(result, Is.False);
            }

            [Test]
            public void ValidatePosition_MapBarrier_ReturnsFalse()
            {
                Position position = P();
                bool result = game.ValidatePosition(position);
                Assert.That(result, Is.False);
            }

            [Test]
            public void ValidatePosition_HiddenWithoutRevealItem_ReturnsFalse()
            {
                game.Map.AddTile(TileType.Hidden, TileContent.Empty, , , );
                Position position = P( , , );
                bool result = game.ValidatePosition(position);
                Assert.That(result, Is.False);
            }

            [Test]
            public void ValidatePosition_HiddenWithRevealItem_ReturnsTrue()
            {
                game.Player.CanRevealHidden = true;
                game.Map.AddTile(TileType.Hidden, TileContent.Empty, , , );

                Position position = P(, , );
                bool result = game.ValidatePosition(position);
                Assert.That(result, Is.True);
            }

            //F4

            [Test]
            public void CollectItems_StandardGold_IncreasesAmountOfGold()
            {
                game = new Game(P(, , ), P(2, 2, 2));

                game.Map.AddTile(TileType.Standard, TileContent.Gold,
                    game.Player.Position.X,
                    game.Player.Position.Y,
                    game.Player.Position.Z);

                game.CollectItems();

                Assert.That(game.Player.AmountOfGold, Is.EqualTo());
                Assert.That(game.Map.Tiles[
                    game.Player.Position.X,
                    game.Player.Position.Y,
                    game.Player.Position.Z].Content, Is.EqualTo(TileContent.Empty));
            }

            [Test]
            public void CollectItems_HiddenGold_IncreasesAmountOfHiddenGold()
            {
                game.Player.CanRevealHidden = true;

                game.Map.AddTile(TileType.Hidden, TileContent.Gold,
                    game.Player.Position.X,
                    game.Player.Position.Y,
                    game.Player.Position.Z);

                game.CollectItems();
                Assert.That(game.Player.AmountOfHiddenGold, Is.EqualTo());
            }

            [Test]
            public void CollectItems_RevealHiddenItem_PlayerCanRevealHidden()
            {
                game.Map.AddTile(TileType.Standard, TileContent.RevealHiddenItem,
                    game.Player.Position.X,
                    game.Player.Position.Y,
                    game.Player.Position.Z);

                game.CollectItems()
                Assert.That(game.Player.CanRevealHidden, Is.True);
            }

            [TestCase(, , , )]
            public void CalculateScore_ReturnsExpectedScore(
            int amountOfGold,
            int amountOfHiddenGold,
            bool canRevealHidden,
            Score expectedScore)
            {
                game.Player.AmountOfGold = amountOfGold;
                game.Player.AmountOfHiddenGold = amountOfHiddenGold;
                game.Player.CanRevealHidden = canRevealHidden;

                Score result = game.CalculateScore();
                Assert.That(result, Is.EqualTo(expectedScore));
            }

            //F5

            [TestCase( , , , )]
            public void CalculateScore_ReturnsExpectedScore(
           int amountOfGold,
           int amountOfHiddenGold,
           bool canRevealHidden,
           Score expectedScore)
            {
                game.Player.AmountOfGold = amountOfGold;
                game.Player.AmountOfHiddenGold = amountOfHiddenGold;
                game.Player.CanRevealHidden = canRevealHidden;

                Score result = game.CalculateScore();
                Assert.That(result, Is.EqualTo(expectedScore));
            }
        }
    }
