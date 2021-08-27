using NUnit.Framework;

namespace GameOfLife
{
    [TestFixture]
    public class GameTests
    {
        [Test]
        public void CheckNextstep_EmptyField()
        {
            var emptyField = new bool[,] { };
            Assert.AreEqual(emptyField, Game.NextStep(emptyField));
        }
        [Test]
        public void CheckNextstep_BlankField_3x3()
        {
            var blankField_3x3 = new bool[3, 3] { {false, false, false },
                                                   { false, false, false },
                                                   { false, false, false} };
            Assert.AreEqual(blankField_3x3, Game.NextStep(blankField_3x3));
        }

        [Test]
        public void CheckNextstep_IsSingleCellDies()
        {
            var expected = new bool[1, 1] { {false}};
            Assert.AreEqual(expected, Game.NextStep(new bool[1, 1] { { true } }));
        }

        [Test]
        public void CheckNextstep_CellsKeepsAlive()
        {
            var expected = new bool[2, 2] { { true, true },
                                            { true, true } };
            Assert.AreEqual(expected, Game.NextStep(new bool[2, 2] { { true, false },
                                                                     { true, true } }));
        }

        [Test]
        public void CheckNextstep_CellDies_Overpopulation()
        {
            var expected = new bool[3, 3] { { true, true, true },
                                            { true, false, true },
                                            { true, true, true }};
            Assert.AreEqual(expected, Game.NextStep(new bool[3, 3] { { false, true, false },
                                                                    { true, true, true },
                                                                    { false, true, false } }));
        }
    }
}