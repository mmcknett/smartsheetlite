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
}
