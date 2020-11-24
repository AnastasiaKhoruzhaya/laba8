using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
1) Создать заданный в варианте класс. Определить в классе необходимые
методы, конструкторы, индексаторы и заданные перегруженные
операции. Написать программу тестирования, в которой проверяется
использование перегруженных операций.
2) Добавьте в свой класс вложенный объект Owner, который содержит Id,
имя и организацию создателя. Проинициализируйте его
3) Добавьте в свой класс вложенный класс Date (дата создания).
Проинициализируйте
4) Создайте статический класс StatisticOperation, содержащий 3 метода для
работы с вашим классом (по варианту п.1): сумма, разница между
максимальным и минимальным, подсчет количества элементов.
5) Добавьте к классу StatisticOperation методы расширения для типа string
и вашего типа из задания№1. См. задание по вариантам.*/

/*
Класс - список List. Дополнительно перегрузить следующие
операции: 

+ - добавить элемент (item+list); 
— - удалить первый элемент из списка (--list); 
!= - проверка на неравенство; 
* - объединение двух списков.

Методы расширения:
1) Подсчет количества слов с заглавной буквы
2) Проверка на повторяющиеся элементы в списке*/


namespace ConsoleApp5
{
    class Program
    {
        class MyList
        {
            public class Owner//добавляем в класс вложеный объект Owner 
            {
                public int id;
                public string name;
                public string university;
            }

            public int number { get; set; }
            public List<int> myLists;

            public MyList(int newNumber)
            {
                Random rand = new Random((int)(DateTime.Now.Ticks));
                number = newNumber;
                myLists = new List<int>(number);//выделяем место списку под количество элементов number
                for (int i = 0; i < number; i++)
                    myLists.Add(rand.Next(0, 10));// инициализация списка
            }

            //перегрузка
            public static bool operator !=(MyList list1, MyList list2)//!= - проверка на неравенство;
            {
                for (int i = 0; i < list1.myLists.Count; i++)
                {
                    for (int j = 0; j < list2.myLists.Count; j++)
                        if (list1.myLists[i] == list2.myLists[i])
                            return false;
                }
                return true;
            }
            public static bool operator ==(MyList list1, MyList list2)//!= - проверка на равенство;
            {
                for (int i = 0; i < list1.myLists.Count; i++)
                {
                    for (int j = 0; j < list2.myLists.Count; j++)
                        if (list1.myLists[i] == list2.myLists[i])
                            return true;
                }
                return false;

            }
            public static MyList operator +(MyList list, int num)//+ - добавить элемент (item+list); 
            {
                ++list.number;
                list.myLists.Add(num);
                return list;
            }
            public static MyList operator --(MyList list)//— - удалить первый элемент из списка (--list); 
            {
                list.number--;
                list.myLists.RemoveAt(0);
                return list;
            }
            public static MyList operator *(MyList list1, MyList list2)//* - объединение двух списков.
            {
                MyList resultList = new MyList(list1.number + list2.number);
                for (int i = 0; i < list1.number; i++)
                    resultList.myLists[i] = list1[i];
                for (int i = 0; i < list2.number; i++)
                    resultList.myLists[i] = list2[i];
                return resultList;
            }

            public int this[int i]//индексатор
            {
                get
                {
                    return myLists[i];
                }
                set
                {
                    myLists[i] = value;
                }
            }

        }
        class Date
        {
            public string dataTime()
            {
                DateTime now = DateTime.Now;
                return ("Date: " + now.ToString("D"));
            }
        }
        static class StatisticOperation
        {
            public static int Sum(MyList list)
            {
                return list.myLists.Sum();
            }
            public static int Quantity(MyList list)
            {
                return list.myLists.Count;
            }
            public static int Difference(MyList list)
            {
                return list.myLists.Max() - list.myLists.Min();
            }
            public static int Str(string str)
            {
                string[] strArray = str.Split(' ');
                int i = 0;
                foreach (string st in strArray)
                {
                    foreach (char ch in st)
                    {
                        if (char.IsUpper(ch))
                        {
                            i++;
                            break;
                        }
                    }
                }
                return i;
            }
            public static bool CheckTheSame(MyList list)
            {
                int counter = 0;
                for (int i = 0; i < list.myLists.Count; i++)
                {
                    if (list.myLists.Contains(list.myLists[i]))
                        counter++;

                }
                if (counter > 1)
                    return true;
                else return false;
            }

        }
        static void Main(string[] args)
        {
            MyList.Owner owner = new MyList.Owner();
            owner.name = "Anastasia";
            Console.WriteLine(owner.name);

            Date date = new Date();
            Console.WriteLine(date.dataTime() + "\n");

            int number = 5;
            MyList list = new MyList(number);
            MyList list1 = new MyList(number);

            Console.WriteLine("The first list is ");
            for (int i = 0; i < number; i++)
            {
                Console.Write(list[i] + " ");
            }
            Console.WriteLine("\nIts sum is " + StatisticOperation.Sum(list) +
                "\nThe diference between its elements is " + StatisticOperation.Difference(list) +
                "\nIts quantity is " + StatisticOperation.Quantity(list) + "\n");

            Console.WriteLine("\nThe second list is ");
            for (int i = 0; i < number; i++)
            {
                Console.Write(list1[i] + " ");
            }
            Console.WriteLine("\nIts sum is " + StatisticOperation.Sum(list1) +
                "\nThe diference between its elements is " + StatisticOperation.Difference(list1) +
                "\nIts quantity is " + StatisticOperation.Quantity(list1) + "\n");


            Console.WriteLine("\n\nList1 == List2 ? The answer is " + (list == list1)
                + "\n\nAfter the +operator ");
            list += 1;
            for (int i = 0; i < list.number; i++)
            {
                Console.Write(list[i] + " ");
            }

            Console.WriteLine("\n\nThe decriment of the list: ");
            list--;
            for (int i = 0; i < list.number; i++)
            {
                Console.Write(list[i] + " ");
            }

            Console.WriteLine("\n\nThe multiplication of two lists: ");
            MyList rlist = list * list1;
            for (int i = 0; i < rlist.number; i++)
            {
                Console.Write(rlist.myLists[i] + " ");
            }

            Console.WriteLine("\n\nChecking out the string operations: \nEnter the string");
            string str = Console.ReadLine();
            Console.WriteLine("There's " + StatisticOperation.Str(str) + " words with uppercase words");

            Console.WriteLine("\nIs there any same elements in the 2 list?\n"
            + StatisticOperation.CheckTheSame(list1));



            Console.ReadKey();



        }
    }
}
