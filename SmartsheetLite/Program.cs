using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartsheetLite
{
    class Program
    {
        const uint CAPACITY = 10;

        static Row[] sheet;
        static uint top = 0;

        enum Command {
            Insert,
            Display,
            Quit,
            Help,
            None
        }

        static void Main(string[] args)
        {
            sheet = new Row[CAPACITY];

            // Add some initial data.
            Insert(new Row("UW Open", 11.94f, 10.96f));
            Insert(new Row("Multnomah Last Chance", 12.34f, 10.76f));
            Insert(new Row("WU Invite", 13f, 11.32f));
            Insert(new Row("WAOR Meet", 14.23f, 12.32f));

            Command lastCommand = Command.Display;
            while (lastCommand != Command.Quit)
            {
                // Handle the last command.
                if (lastCommand == Command.Insert)
                {
                    LetUserInsertRow();
                }

                if (lastCommand == Command.Help)
                {
                    PrintHelp();
                }

                if (lastCommand == Command.Display)
                {
                    PrintSheet();
                }

                // Get the next command.
                lastCommand = GetCommand();
            }
        }

        static void LetUserInsertRow()
        {
            // TODO: Read in the three cells for the row, create a new row, and insert it.
        }

        static void PrintHelp()
        {
            Console.WriteLine("The following commands are available:");
            Console.WriteLine("  'q' or 'quit' to exit the program");
            Console.WriteLine("  'd' or 'display' to print out the current state of the sheet");
            Console.WriteLine("  'i' or 'insert' to insert a new row");
            Console.WriteLine();
        }

        static Command GetCommand()
        {
            Console.Write("Enter command ('h' or 'help' to list commands): ");
            string input = Console.ReadLine();

            if (input == "h" || input == "help")
            {
                return Command.Help;
            }
            else if (input == "q" || input == "quit")
            {
                return Command.Quit;
            }
            else if (input == "d" || input == "display")
            {
                return Command.Display;
            }
            else if (input == "i" || input == "insert")
            {
                return Command.Insert;
            }
            else
            {
                Console.WriteLine("Unrecognized command.");
                return Command.None;
            }
        }

        static void PrintSheet()
        {
            for(int i = 0; i < top; ++i)
            {
                sheet[i].print();
            }
            Console.WriteLine();
        }

        static void Insert(Row row)
        {
            sheet[top] = row;
            ++top;
        }
    }

    class Row
    {
        string meetName;
        float weighThrow;
        float shotPut;

        public Row(string meetName, float weighThrow, float shotPut)
        {
            this.meetName = meetName;
            this.weighThrow = weighThrow;
            this.shotPut = shotPut;
        }

        public void print()
        {
            Console.WriteLine("Meet name: {0},\t Weight Throw distance: {1} meters,\t Shot Put distance: {2} meters.",
                meetName,
                weighThrow,
                shotPut);
        }
    }
}
