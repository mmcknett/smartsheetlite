using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartsheetLite
{
    class Program
    {
        static void Main(string[] args)
        {
            Row[] sheet = new Row[]
            {
                new Row("UW Open", 11.94f, 10.96f),
                new Row("Multnomah Last Chance", 12.34f, 10.76f),
                new Row("WU Invite", 13f, 11.32f),
                new Row("WAOR Meet", 14.23f, 12.32f)
            };

            for(int i = 0; sheet.Length > i; ++i)
            {
                sheet[i].print();
            }                  

            Console.ReadKey();
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
