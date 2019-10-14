using System;
using System.Text.RegularExpressions;

namespace T9
{
    class Program
    {
        const int ElementsToShow = 3; // Количество вариантов для отображения.

        static void WorkWithDictionary (T9 dict)
        {
            string _word = string.Empty;
            int reg;
            Console.WriteLine("Режим ввода слов в словарь!");
            do
            {
                Console.Write("Ожидание ввода слова: ");
                _word = Console.ReadLine();
                reg = dict.Add(_word);
                if (!_word.Equals(string.Empty))
                {
                    if (reg == 0)
                        Console.WriteLine("Использование неподдерживаемых символов, слово не занесено в словарь.\n Для окончания ввода нажмите enter без ввода символов");
                    else if (reg == -1)
                        Console.WriteLine("Данное слово уже есть в словаре. Для окончания ввода нажмите enter без ввода символов");
                    else Console.WriteLine("Слово внесено в словарь. Для окончания ввода нажмите enter без ввода символов");
                }
            }
            while (!(_word.Equals(string.Empty)));
            Console.WriteLine("Окончание ввода слов в словарь.");
        } // Работа со словарём

        static string WorkWithUserInput (T9 dict, string _code)
        {
            if (dict.Count != 0)
            {
                int reg;
                string[] results;
                results = dict.GetWordFromCode(_code, ElementsToShow);
                if (results[0] != null)
                {
                    Console.WriteLine("Возможно вы имели в виду это: ");
                    for (int i = 0; i < ElementsToShow; i++)
                    {
                        if (results[i] != null)
                            Console.WriteLine(string.Format("{0}) {1}", i + 1, results[i]));
                        else break;
                    }
                }
                else
                {
                    Console.WriteLine("Подходящих вариантов в словаре не найдено");
                    Console.WriteLine("Для продолжения нажмите enter...");
                    Console.ReadKey();
                    return string.Empty;
                }

                while (true)
                {
                    Console.WriteLine("Для отмены введите 0");
                    Console.Write("Введите номер нужного слова: ");
                    _code = Console.ReadLine();
                    if (_code == "0") return string.Empty;
                    Regex parser = new Regex(@"[\d]");
                    if (parser.IsMatch(_code))
                    {
                        reg = Convert.ToInt32(_code);
                        if (reg > 0 && reg <= ElementsToShow)
                        {
                            if (results[reg - 1] != null)
                            {
                                return results[reg - 1];
                            }
                            else Console.WriteLine("Такого значения здеcь нет, попробуйте снова");
                        }
                        else Console.WriteLine("Такого значения здесь нет, попробуйте снова");
                    }
                    else Console.WriteLine("Неверный ввод. Попробуйте снова");                  
                }
            }
            else
            {               
                return null;
            }
        } // Работа с пользовательским вводом

        static void Main(string[] args)
        {
            Console.WriteLine("Данная программа работает по следующему принципу:" +
                               "\n1) Сначало вводятся слова, каждое должно заканчиваться нажатием enter, пока не будет введена пустая строка, " +
                               "\n2) Затем идёт пользовательский ввод, состоящий из числовой комбинации." +
                               "\n3) На основе ввода система предложит 3 слова соответствующие этой комбинации (программа поддержимает только кириллицу)");
            T9 dict = new T9();
            string ChosenWord;
            string FromInput;          
            WorkWithDictionary(dict);
            //dict.ShowDictionary();  //Отображает словарь с соответствующими кодами.

            do
            {
                Console.Clear();
                FromInput = string.Empty;
                Console.WriteLine("Режим пользовательского ввода!");
                Console.WriteLine("Для отображения словаря введите 000");
                Console.WriteLine("Для перехода в режим словаря введите 010");
                Console.WriteLine("Для выхода из программы нажмите enter без ввода чисел");
                Console.Write("Ожидание пользовательского ввода: ");
                FromInput = Console.ReadLine();

                switch (FromInput)
                {
                    case "000":
                        Console.Clear();
                        dict.ShowDictionary();
                        Console.WriteLine("Для продолжения нажмите enter...");
                        Console.ReadKey();
                        break;

                    case "010":
                        Console.Clear();
                        WorkWithDictionary(dict);
                        Console.WriteLine("Для продолжения нажмите enter...");
                        break;

                    default:
                        if (FromInput != string.Empty)
                        {
                            ChosenWord = WorkWithUserInput(dict, FromInput);
                            if (!String.IsNullOrEmpty(ChosenWord))
                            {
                                Console.WriteLine("Выбранное слово: " + ChosenWord);
                                Console.WriteLine("Для продолжения нажмите enter...");
                                Console.ReadKey();
                            }                                
                            else if (ChosenWord == null)
                            {
                                Console.WriteLine("Словарь пуст.");
                                Console.WriteLine("Для продолжения нажмите enter...");
                                Console.ReadKey();
                            }
                        }
                        break;
                }                
            }
            while (FromInput != string.Empty);
            Console.WriteLine("Завершение работы программы");
            //Console.ReadKey();
        }
    }
}
