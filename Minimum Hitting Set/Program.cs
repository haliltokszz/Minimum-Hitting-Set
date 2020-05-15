using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Minimum_Hitting_Set
{
    public class Program
    {
        static List<Item> items = new List<Item>();

        static Dictionary<int, List<int>> columns = new Dictionary<int, List<int>>();

        static void MissIndex(Item item)
        {
            List<int> missValue= new List<int>();
            foreach (int column in columns.Keys)
            {
                bool isHas = false;
                foreach (int value in item.Index)
                {
                    if (column == value)
                    {
                        isHas = true;
                    }
                }
                if (isHas == false)
                {
                    missValue.Add(column);
                }
            }
            item.MissIndex= missValue;
        }

        public static void Main(string[] args)
        {
            StreamReader tReader = new StreamReader(@"C:\Users\Monster-Halil\source\repos\Minimum Hitting Set\inputs\inp2.txt");
            List<String> lines = new List<String>();
            //satır satır okuma
            while (tReader.EndOfStream != true)
            {
                    lines.Add(tReader.ReadLine());
            }

            string[] universalItems = lines[0].Split(' ');//tüm elemanları evrensel kümeden aldık
            foreach(string uItems in universalItems)
            {
                items.Add(new Item(Convert.ToInt32(uItems)));
            }

            //satırlardaki değerleri column dict'e atıyoruz
            for (int j = 1; j < lines.Count; j++)
            {
                List<int> lineItems = new List<int>();
                string[] tempItems = lines[j].Split(' ');
                lines[j] = lines[j].Replace(" ", "");
                foreach (string temp in tempItems)
                {
                    if (temp != "")
                        lineItems.Add(Convert.ToInt32(temp));
                }
                columns.Add(j, lineItems);
            }

            //değerlerin bulundukları indexleri alıyoruz
            for (int i = 0; i < universalItems.Length; i++)
            {
                List<int> index = new List<int>();
                for (int j = 1; j < lines.Count; j++)
                {
                    var columnValue = columns[j];
                    foreach (int item in columnValue)
                    {
                        if (Convert.ToInt32(universalItems[i]) == item)
                        {
                            index.Add(j);
                        }
                    }

                }
                items[i].Repretition = index.Count;
                items[i].Index = index;
                MissIndex(items[i]);
            }

            string mhs=""; 
            List<Item> sortedList = items.OrderByDescending(i => i.Repretition).ToList(); //tekrar sayısına göre sıraladık
            for(int i=0; i<sortedList.Count; i++)
            {
                //eğer en çok tekrarlayan değer power set sayısına eşitse sonuç olarak onu döndürüyoruz
                if ((lines.Count - 1) - sortedList[i].Repretition == 0)
                {
                    mhs = sortedList[i].No.ToString();
                    Console.WriteLine("MHS: " + mhs);
                    Console.ReadKey();
                    return;
                }
                Console.WriteLine(sortedList[i].No+ " keyinin bulunduğu satırlar: " + sortedList[i].PrintIndex()+ " bulunmadığı satırlar: " + sortedList[i].PrintMissIndex());
                List<int> tempMHS = new List<int>();
                List<int> tempMHSItems = new List<int>();
                
                for (int j = i + 1; j < sortedList.Count; j++)//sonraki satırlara bakıyoruz
                {
                    for (int missIndex = 0; missIndex < sortedList[i].MissIndex.Count; missIndex++)//bulunduğumuz elamanın eksik satırlarında geziyoruz       
                    {
                        foreach (int index in sortedList[j].Index)//bu satırda hangi elemanların olup olmadığını kontrol edicez
                        {
                            if (sortedList[i].MissIndex[missIndex] == index)
                            {
                                Console.WriteLine("--> "+ sortedList[i].No + " keyinde eksik olan "+index + " satırı " + sortedList[j].No + " elemanında bulundu");
                                if (!tempMHSItems.Contains(index))
                                {
                                    tempMHSItems.Add(index);
                                    if (!tempMHS.Contains(sortedList[j].No))
                                    {
                                        tempMHS.Add(sortedList[j].No);
                                    }
                                        
                                    if (tempMHSItems.Count == sortedList[i].MissIndex.Count)
                                    {
                                        if (sortedList[i].MHS == null || sortedList[i].MHS.Length > tempMHS.Count)
                                        {
                                            string result = "";
                                            foreach (int e in tempMHS)
                                            {
                                                result += e;
                                            }
                                            sortedList[i].MHS = result + sortedList[i].No;
                                        }
                                        tempMHSItems.Clear();
                                    }
                                }     
                            }
                        }
                    }
                }
            }

            List<Item> sortedMHS = items.OrderBy(i => i.MHS.Length).ToList();
            char[] minimumHittingSets = sortedMHS[0].MHS.ToCharArray();
            foreach(char c in minimumHittingSets)
            {
                mhs += c + ",";
            }
            mhs=mhs.Remove(mhs.Length-1);
            Console.WriteLine("\n \nMinimum Hitting Set: { " + mhs +" }");

            Console.ReadKey();
        }

    }
}

