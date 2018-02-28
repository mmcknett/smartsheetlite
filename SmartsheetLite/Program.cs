using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartsheetLite
{
    public class Program
    {
        const uint CAPACITY = 10;

        static Sheet sheet;

        public enum Command {
            Insert,
            Display,
            Quit,
            Help,
            None
        }

        public static void Main(string[] args)
        {
            sheet = new Sheet(CAPACITY);

            // Add some initial data.
            sheet.Insert(new Row("UW Open", 11.94f, 10.96f));
            sheet.Insert(new Row("Multnomah Last Chance", 12.34f, 10.76f));
            sheet.Insert(new Row("WU Invite", 13f, 11.32f));
            sheet.Insert(new Row("WAOR Meet", 14.23f, 12.32f));

            Command lastCommand = Command.None;
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

        public static Command GetCommand()
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
            for(uint i = 0; i < sheet.RowCount(); ++i)
            {
                sheet.GetRow(i).print();
            }
            Console.WriteLine();
        }
    }

    public class Sheet
    {
        Row[] sheet;
        uint top = 0;

        public Sheet(uint rowCapacity)
        {
            this.sheet = new Row[rowCapacity];
        }

        public uint RowCount()
        {
            return top;
        }

        public uint RowCapacity()
        {
            return (uint)sheet.Length;
        }

        public void Insert(Row row)
        {
            if (top >= sheet.Length)
            {
                throw new OutOfMemoryException();
            }

            sheet[top++] = row;
        }

        public Row GetRow(uint i)
        {
            if (i >= top)
            {
                throw new IndexOutOfRangeException();
            }

            return sheet[i];
        }
    }

    public class Row
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
