using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
/*1. Создайте обобщенный интерфейс с операциями добавить, удалить,
просмотреть.
2. Возьмите за основу лабораторную № 4 «Перегрузка операций» и
сделайте из нее обобщенный тип (класс) CollectionType<T>, в который
вложите обобщённую коллекцию. Наследуйте в обобщенном классе интерфейс
из п.1. Реализуйте необходимые методы. Добавьте обработку исключений c
finally. Наложите какое-либо ограничение на обобщение.
3. Проверьте использование обобщения для стандартных типов данных (в
качестве стандартных типов использовать целые, вещественные и т.д.).
Определить пользовательский класс, который будет использоваться в качестве
параметра обобщения. Для пользовательского типа взять класс из лабораторной
№5 «Наследование». 
*/


namespace ConsoleApp5
{
    class Program
    {
        class ProductExceptions : Exception
        {
            public ProductExceptions(string message)
            : base(message)
            { }
            public string message = "Products have lost their shelf life";
            public string diagnostics = "Need to replace products";
        }
        class Product
        {
            public string name;
            public int cost;

            public Product(string n, int c)
            {
                name = n; cost = c;
            }
            public override string ToString()
            {
                return ("\nName: " + name + " Cost: " + cost);
            }
            private int age;
            public int Age
            {
                get { return age; }
                set
                {
                    if (value > 5)
                        throw new ProductExceptions("Products have lost their shelf life");
                    else
                        age = value;
                }
            }
        }
        interface IList<T>
        {
            public void Add(T number);
            public void Delete();
            public void Output();
        }
        class MyList<T>: IList<T>
        {
            public class Owner//добавляем в класс вложеный объект Owner 
            {
                public int id;
                public string name;
                public string university;
            }

            public int number { get; set; }
            public List<T> myLists;

            public MyList(int newNumber)
            {
                number = newNumber;
                myLists = new List<T>(number);//выделяем место списку под количество элементов number
            }

            public void Add(T num)
            {
                ++number;
                myLists.Add(num);
            }
            public void Delete()
            {
                number--;
                myLists.RemoveAt(0);
            }
            public void Output()
            {
                foreach (T num in myLists)
                    Console.Write(num + " ");
            }

            //перегрузка
            public static bool operator !=(MyList<T> list1, MyList<T> list2)//!= - проверка на неравенство;
            {
                if (list1.GetType() == list2.GetType())
                    return false;
                return true;
            }
            public static bool operator ==(MyList<T> list1, MyList<T> list2)//!= - проверка на равенство;
            {
                if (list1.GetType() == list2.GetType())
                    return true;
                return false;
            }
            public static MyList<T> operator +(MyList<T> list, T num)//+ - добавить элемент (item+list); 
            {
                ++list.number;
                list.myLists.Add(num);
                return list;
            }
            public static MyList<T> operator --(MyList<T> list)//— - удалить первый элемент из списка (--list); 
            {
                list.number--;
                list.myLists.RemoveAt(0);
                return list;
            }
            public static MyList<T> operator *(MyList<T> list1, MyList<T> list2)//* - объединение двух списков.
            {
                MyList<T> resultList = new MyList<T>(list1.number + list2.number);
                for (int i = 0; i < list1.number; i++)
                    resultList.myLists[i] = list1[i];
                for (int i = 0; i < list2.number; i++)
                    resultList.myLists[i] = list2[i];
                return resultList;
            }

            public T this[int i]//индексатор
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
        static class StatisticOperation<T>
        {
            public static int Quantity(MyList<T> list)
            {
                return list.myLists.Count;
            }
            public static bool CheckTheSame(MyList<T> list)
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
            MyList<int> listInt = new MyList<int>(5);
            listInt.Add(3); listInt.Add(6); listInt.Add(1); listInt.Add(2); listInt.Add(4);
            listInt.Output();
            Console.WriteLine();

            MyList<string> listString = new MyList<string>(5);
            listString.Add("help"); listString.Add("me"); listString.Add("please"); listString.Add("I'm"); listString.Add("dead");
            listString.Output();
            Console.WriteLine();

            Product product = new Product("apple", 100);
            Product product2 = new Product("pen", 50);

            MyList<Product> listProduct = new MyList<Product>(2);
            listProduct.Add(product);
            listProduct.Add(product2);
            listProduct.Output();
            Console.WriteLine();

            try
            {
                Product receipt = new Product("ring",1000);
                Console.Write("Enter age: ");
                receipt.Age = Convert.ToInt32(Console.ReadLine());
                Debug.Assert(receipt.Age >= 0);
            }
            catch (ProductExceptions ex)
            {
                Console.WriteLine("Exception: " + ex.message);
                Console.WriteLine("Diagnostics, how to avoid: " + ex.diagnostics);
            }

            Console.ReadKey();
        }
    }
}
