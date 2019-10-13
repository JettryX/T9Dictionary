using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace T9
{
    class Program
    {
        const int ElementsToShow = 3; // Количество вариантов для отображения.
        static void Main(string[] args)
        {
            Console.WriteLine("Данная программа работает по следующему принципу: " +
                               "Сначало вводятся слова, каждое должно заканчиваться нажатием enter, пока не будет введена пустая строка, " +
                               "\n Затем идёт пользовательский ввод, состоящий из числовой комбинации.\n " +
                               "На основе ввода система предложит 3 слова соответствующие этой комбинации (программа работает только под русский язык)");
            T9 dict = new T9();
            string _code;
            string _word = string.Empty;
            int reg;
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
            Console.WriteLine("Окончание ввода слов в словарь, переключение режима на считывание пользовательского ввода");
            if (dict.count != 0)
            {
                dict.ShowDictionary();
                Console.Write("Ожидание пользовательского ввода: ");
                _code = Console.ReadLine();
                string[] results;
                results = dict.GetWordFromCode(_code, ElementsToShow);
                if (results[0] != null)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        if (results[i] != null)
                            Console.WriteLine(string.Format("{0}) {1}", i + 1, results[i]));
                        else break;
                    }
                }
                else Console.WriteLine("Слова в словаре не найдены");
                
                while (true)
                {
                    _code = Console.ReadLine();
                    Regex parser = new Regex(@"[\d]");
                    if (parser.IsMatch(_code))
                    {
                        reg = Convert.ToInt32(_code);
                        if (reg > 0 && reg <= ElementsToShow)
                        {
                            try
                            {
                                Console.WriteLine(results[reg-1]);
                                break;
                            }
                            catch { Console.Write("Такого значения здеь нет, попробуйте снова"); }
                        }
                        else Console.WriteLine("Такого значения здесь нет, попробуйте снова");
                    }
                    else Console.WriteLine("Неверный ввод. Попробуйте снова");
                }              
            }
            else
            {
                Console.WriteLine("Словарь пуст. завершение программы");
                Console.ReadKey();
            }
            Console.ReadKey();
        }
    }
}
