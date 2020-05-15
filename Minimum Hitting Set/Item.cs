using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minimum_Hitting_Set
{
    class Item
    {
        private int no;
        private string mhs;

        public int No
        {
            get { return no; }
            set { no = value; }
        }

        public int Repretition { get; set; }
        public List<int> Index { get; set; }

        public List<int> MissIndex { get; set; }

        public string MHS
        {
            get { if (mhs != null) return mhs; else return "Hesaplanmaya Gerek Duyulmamıştır."; ; }
            set { mhs = value; }
        }

        public Item(int itemNo)
        {
            this.no = itemNo;
            Index = new List<int>();
            MissIndex = new List<int>();
        }

        public string PrintMissIndex()
        {
            string missIndex = "";
            foreach (int miss in MissIndex)
                missIndex += miss+",";
            return missIndex;
        }
        public string PrintIndex()
        {
            string indexler="";
            foreach (int index in Index)
               indexler+= index+",";
            return indexler;
        }
    }
}
