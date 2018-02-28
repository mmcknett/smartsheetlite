using NUnit.Framework;
using System;
using SmartsheetLite;
using System.IO;

namespace UnitTests
{
    [TestFixture()]
    public class GetCommandTests
    {
        void GetCommand_GivenInputOnStdIn_ReturnsCommand(
            Program.Command expectedCommand,
            string input)
        {
            // Arrange
            using (var sr = new StringReader(input))
            {
                Console.SetIn(sr);

                // Act
                Program.Command result = Program.GetCommand();

                // Assert
                Assert.AreEqual(
                    expectedCommand,
                    result,
                    string.Format("GetCommand must return Help when the input is '{0}'", input)
                );
            }
        }

        [Test()]
        public void GetCommand_Enter_h_OnStdIn_ReturnsCommandHelp()
        {
            GetCommand_GivenInputOnStdIn_ReturnsCommand(
                Program.Command.Help,
                "h"
            );
        }

        [Test()]
        public void GetCommand_Enter_help_OnStdIn_ReturnsCommandHelp()
        {
            GetCommand_GivenInputOnStdIn_ReturnsCommand(
                Program.Command.Help,
                "help"
            );
        }

        [Test()]
        public void GetCommand_Enter_q_OnStdIn_ReturnsCommandQuit()
        {
            GetCommand_GivenInputOnStdIn_ReturnsCommand(
                Program.Command.Quit,
                "q"
            );
        }

        [Test()]
        public void GetCommand_Enter_quit_OnStdIn_ReturnsCommandQuit()
        {
            GetCommand_GivenInputOnStdIn_ReturnsCommand(
                Program.Command.Quit,
                "quit"
            );
        }

        [Test()]
        public void GetCommand_Enter_d_OnStdIn_ReturnsCommandDisplay()
        {
            GetCommand_GivenInputOnStdIn_ReturnsCommand(
                Program.Command.Display,
                "d"
            );
        }

        [Test()]
        public void GetCommand_Enter_display_OnStdIn_ReturnsCommandDisplay()
        {
            GetCommand_GivenInputOnStdIn_ReturnsCommand(
                Program.Command.Display,
                "display"
            );
        }

        [Test()]
        public void GetCommand_Enter_i_OnStdIn_ReturnsCommandInsert()
        {
            GetCommand_GivenInputOnStdIn_ReturnsCommand(
                Program.Command.Insert,
                "i"
            );
        }

        [Test()]
        public void GetCommand_Enter_insert_OnStdIn_ReturnsCommandInsert()
        {
            GetCommand_GivenInputOnStdIn_ReturnsCommand(
                Program.Command.Insert,
                "insert"
            );
        }

        [Test()]
        public void GetCommand_Enter_someRandomCommand_OnStdIn_ReturnsCommandNone()
        {
            GetCommand_GivenInputOnStdIn_ReturnsCommand(
                Program.Command.None,
                "someRandomCommand"
            );
        }
    }

    [TestFixture()]
    public class ProgramTests
    {
        [Test()]
        public void RunProgram_WithHelpCommand_OutputsHelpText()
        {
            // Arrange
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                using (var sr = new StringReader(
                    string.Format("h{0}q", Environment.NewLine)))
                {
                    Console.SetIn(sr);
                    string expected =
@"Enter command ('h' or 'help' to list commands): The following commands are available:
  'q' or 'quit' to exit the program
  'd' or 'display' to print out the current state of the sheet
  'i' or 'insert' to insert a new row

Enter command ('h' or 'help' to list commands): ";

                    // Act
                    Program.Main(new string[] { });

                    // Assert
                    Assert.AreEqual(expected, sw.ToString(),
                        "The help text must match the output of the help command.");
                }
            }
        }
    }

    [TestFixture()]
    public class SheetTests
    {
        [Test()]
        public void RowCount_EmptySheetWithAnyCapacity_ReturnsZero()
        {
            // Arrange
            uint anyCapacity = 10;

            // Act
            var sheet = new Sheet(anyCapacity);

            // Assert
            Assert.AreEqual(0, sheet.RowCount(), "Row count must be zero.");
        }

        [Test()]
        public void RowCapacity_EmptySheetWithAnyCapacity_ReturnsCapacity()
        {
            // Arrange
            uint anyCapacity = 10;

            // Act
            var sheet = new Sheet(anyCapacity);

            // Assert
            Assert.AreEqual(anyCapacity, sheet.RowCapacity(), "Row capacity must be 10.");
        }

        [Test()]
        public void Insert_EmptySheetWithSomeCapacity_IncreasesRowCount()
        {
            // Arrange
            uint anyCapacity = 10;
            uint expectedRowCount = 1;
            var rowToInsert = new Row("Test meet name", 10.0f, 20.0f);
            var sheet = new Sheet(anyCapacity);

            // Act
            sheet.Insert(rowToInsert);

            // Assert
            Assert.AreEqual(expectedRowCount, sheet.RowCount(), "Row count must be 1.");
        }

        [Test()]
        public void Insert_SheetWithOneElementAndSomeCapacity_RowCountIsTwo()
        {
            // Arrange
            uint anyCapacity = 10;
            uint expectedRowCount = 2;
            var row0 = new Row("Test meet name", 10.0f, 20.0f);
            var row1 = new Row("Second test meet name", 15.0f, 25.0f);
            var sheet = new Sheet(anyCapacity);
            sheet.Insert(row0);

            // Act
            sheet.Insert(row1);

            // Assert
            Assert.AreEqual(expectedRowCount, sheet.RowCount(), "Row count must be 2.");
        }

        [Test()]
        public void Insert_SheetWithOneElementAndCapacityOne_ThrowsOutOfMemory()
        {
            // Arrange
            var row0 = new Row("Test meet name", 10.0f, 20.0f);
            var row1 = new Row("Second test meet name", 15.0f, 25.0f);
            var sheet = new Sheet(1);
            sheet.Insert(row0);

            // Act
            TestDelegate action = () => { sheet.Insert(row1); };

            // Assert
            Assert.Throws<OutOfMemoryException>(action, "Inserting beyond capacity must throw an exception.");
        }

        [Test()]
        public void GetRow_SheetWithOneRow_GetsFirstRow()
        {
            // Arrange
            uint anyCapacity = 10;
            var row = new Row("Test meet name", 10.0f, 20.0f);
            var sheet = new Sheet(anyCapacity);
            sheet.Insert(row);

            // Act
            var actual = sheet.GetRow(0);

            // Assert
            Assert.AreEqual(row, actual, "Row must match.");
        }

        [Test()]
        public void GetRow_SheetWithTwoRows_GetsSecondRow()
        {
            // Arrange
            uint anyCapacity = 10;
            var row0 = new Row("Test meet name", 10.0f, 20.0f);
            var row1 = new Row("Test meet name 2", 15.0f, 25.0f);
            var sheet = new Sheet(anyCapacity);
            sheet.Insert(row0);
            sheet.Insert(row1);

            // Act
            var actual = sheet.GetRow(1);

            // Assert
            Assert.AreEqual(row1, actual, "Row must match.");
        }

        [Test()]
        public void GetRow_InvalidIndexSheetWithTwoRows_Throws()
        {
            // Arrange
            uint anyCapacity = 10;
            var row0 = new Row("Test meet name", 10.0f, 20.0f);
            var row1 = new Row("Test meet name 2", 15.0f, 25.0f);
            var sheet = new Sheet(anyCapacity);
            sheet.Insert(row0);
            sheet.Insert(row1);

            // Act
            TestDelegate action = () => { sheet.GetRow(2); };

            // Assert
            Assert.Throws<IndexOutOfRangeException>(action, "GetRow must throw.");
        }
    }
}
