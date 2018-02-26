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
            Row uWOpen = new Row("UW Open", 11.94f, 10.96f);
            Row lastChance = new Row("Multnomah Last Chance", 12.34f, 10.76f);
            Row wUInvite = new Row("WU Invite", 13f, 11.32f);
            Row wAOR = new Row("WAOR Meet", 14.23f, 12.32f);

            uWOpen.print();
            lastChance.print();
            wUInvite.print();
            wAOR.print();

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
