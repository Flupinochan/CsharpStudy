using System;
using System.Text;

namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            // Object Browser (表示 => オブジェクト ブラウザー)
            int number = 100;
            const string NAME = "metalmental"; // final
            var something = 1;
            Console.WriteLine(number);
            Console.WriteLine(something);
            Console.WriteLine("{0}", NAME);

            // Type Conversion
            byte b = 1;
            int i = (int)b;
            string strNumber = "100";
            int intNumber = int.Parse(strNumber);
            int intNumber2 = Convert.ToInt32(strNumber);
            strNumber = intNumber.ToString();

            // Operator
            if (number == intNumber && number == intNumber2)
            {
                Console.WriteLine("値は等しいです");
            }

            // Class
            Person person = new Person("Tetsuro", "Kawagoe");
            person.Introduce();

            // Array (Element数は不変)
            int[] numbers = new int[3];
            numbers[0] = 1;
            numbers[1] = 2;
            numbers[2] = 3;
            int[] numbers2 = new int[3] { 1, 2, 3 };

            // String (charの配列と考え、プリミティブ型ではなくクラス、stringはStringのエイリアス)
            String firstName = "Tetsuro";
            string lastName = "Kawagoe";
            Int32 num1; // intはInt32のエイリアス
            // string.Format()
            string fullName = string.Format("My name is{0} {1}", firstName, lastName);
            string[] names = new string[3] { "John", "Jack", "Mary" };
            // string.Join()
            string namesCombine = string.Join(", ", names);
            // row文字列
            string fullPath = @"c:\drive\folder\test.txt";
            Console.WriteLine(fullPath);

            // Enum (定数の集合を定義する型)
            ShippingMethod express = ShippingMethod.Express;
            Console.WriteLine(express); // Keyを出力
            Console.WriteLine(express.ToString()); // Keyを出力
            Console.WriteLine((int)express); // 値を出力
            string strExpress = "express";
            ShippingMethod method = (ShippingMethod)Enum.Parse(typeof(ShippingMethod), strExpress, true);

            // if
            int hour = 19;
            if (hour < 0 && hour < 12)
            {
                Console.WriteLine("Morning");
            }
            else if (hour >= 12 && hour < 18)
            {
                Console.WriteLine("Afternoon");
            }
            else
            {
                Console.WriteLine("Evening");
            }

            // switch
            Season season = Season.Summer;
            switch (season)
            {
                case Season.Autumn:
                    Console.WriteLine("Autumn");
                    break;
                case Season.Spring:
                case Season.Summer:
                    Console.WriteLine("Spring or Summer");
                    break;
                default:
                    Console.WriteLine("Winter");
                    break;
            }

            // for
            for (i = 1; i <= 10; i++)
            {
                if (i % 2 != 0)
                {
                    continue;
                }
                Console.WriteLine(i);
            }

            // forEach (pythonのlistと同じ)
            String[] namesArray = new String[3] { "Alice", "Bob", "Jack" };
            foreach (String name in namesArray)
            {
                Console.WriteLine(name);
            }
            for (int j = 0; j < namesArray.Length; j++)
            {
                Console.WriteLine(namesArray[j]);
            }

            // while
            while (true)
            {
                Console.WriteLine("while-true");
                break;
            }

            // random
            char[] charArray = new char[10];
            Random random = new Random();
            for (int j = 0; j < 10; j++)
            {
                charArray[j] = (char)random.Next(97, 122);
            }
            String password = String.Join("", charArray);
            String password2 = new string(charArray); // StringはcharのArray
            Console.WriteLine(password);

            // Array(固定長)
            int[] numbers3 = new int[4] {2, 7, 9, 3 };
            int numbers3Length = numbers3.Length;
            int[] numbers3Copy = new int[4];
            Array.Copy(numbers3, numbers3Copy, numbers3.Length);
            Array.Sort(numbers3Copy);
            Array.Reverse(numbers3Copy);

            // List(可変長)
            List<int> numbers4 = new List<int>();
            numbers4.Add(1);
            numbers4.AddRange(numbers3);
            numbers4.Remove(7); // 要素の値
            numbers4.RemoveAt(0); // 要素のIndex
            int count = numbers4.Count();
            foreach(int number4 in numbers4){
                Console.WriteLine(number4);
            }

            // DateTime
            DateTime now = DateTime.Now;
            Console.WriteLine(now.ToString());
            DateTime today = DateTime.Today;
            Console.WriteLine(today.ToString());
            DateTime today9 = today.AddHours(9);
            Console.WriteLine(today9.ToString("yyyy年MM月dd日 HH時mm分ss秒"));
            Console.WriteLine(now.ToShortTimeString());
            DateTime start = DateTime.Now;
            DateTime end = DateTime.Now.AddMinutes(5);
            Console.WriteLine($"Duration: {(end - start)}");

            // TimeSpan(日数や時間数)
            TimeSpan timespan = new TimeSpan(1, 2, 3, 4); // 1日 2時間 3分 4秒
            Console.WriteLine($"Days: {timespan.Days}");
            Console.WriteLine($"TotalMinutes: {timespan.TotalHours}");
            // 文字列からの型変換は、だいたいParseを使用する
            if(today == DateTime.Parse("2025/02/08"))
            {
                Console.WriteLine("今日の日付は、02/08");
            }

            // String
            // .Trim()    空白を除去
            // .ToUpper()    大文字化
            // .Substring(int startIndex, int length)    指定したIndex間の文字列を切り取る
            // .Split(",")    // 指定した区切り文字でArrayに格納する
            // .Replace(string oldValue, string newValue)    // 置換
            // String.IsNullOrEmpty("space")    // nullまたは""空文字をチェック
            float price = 30.15f;
            Console.WriteLine(price.ToString("C")); // $30に変換される

            // StringBuilder(mutable) - String(immutable)
            // 文字列の変更が多い場合にパフォーマンスが良い
            StringBuilder strBuiler = new StringBuilder();
            strBuiler.Append("metal")
                     .Append("mental")
                     .Replace("m", "M")
                     .AppendLine();
            Console.Write(strBuiler);

            // File(Static)
            if(File.Exists(@"c:\tmp\src.txt"))
            {
                File.Copy(@"c:\tmp\src.txt", @"d:\tmp\dist.txt", true);
                string content = File.ReadAllText(@"c:\tmp\src.txt");
            }
            // FileInfo(Instance) ※Objectを使いまわすためパフォーマンスが良い(objectごとに権限が確認される)
            FileInfo fileInfo = new FileInfo(@"c:\tmp\src.txt");
            if (fileInfo.Exists)
            {
                using (StreamReader reader = fileInfo.OpenText())
                {
                    string content = reader.ReadToEnd();
                    Console.WriteLine(content);
                }
            }
            // Directory
            string dir = Directory.GetCurrentDirectory();
            Console.WriteLine(dir);
            if (Directory.Exists(@"c:\tmp\"))
            {
                Console.WriteLine("Directory Exists");
            }
            // DirectoryInfo
            DirectoryInfo dirInfo = new DirectoryInfo(@"c:\tmp\");

            // Path (ファイル名やパスの結合、OSに依存しないパス区切り)
            string folder = @"C:\Users\Public";
            string file = "example.txt";

            string fullPath2 = Path.Combine(folder, file);
            Console.WriteLine(fullPath2);
            Path.GetTempPath();
            // Path.GetTempFileName(); tmpファイルを作成






        }
    }

    // class と structs (基本的にstructsは不要、何千というオブジェクトを再生成する時にstructsで効率化できるかもしれない)
    public class Person
    {
        public string FirstName;
        public string LastName;

        public Person(string FirstName, string LastName) 
        { 
            this.FirstName = FirstName;
            this.LastName = LastName;
        }

        public void Introduce()
        {
            Console.WriteLine("My name is " + FirstName + " " + LastName);
        }
    }

    // Enum(key-value)
    public enum ShippingMethod
    {
        RegularAirMail = 1,
        RegisteredAirMail = 2,
        Express = 3
    }

    public enum Season
    {
        Spring,
        Summer,
        Autumn,
        Winter
    }

    // プリミティブ型(Stack)は=でコピーされるが、ArrayやStringなどのClass(Heap)は参照が増えるだけ
}
