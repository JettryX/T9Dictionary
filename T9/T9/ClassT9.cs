﻿using System;
//using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Collections.Specialized;
using System.Collections;

namespace T9
{

    class T9
    {
        private OrderedDictionary dict;
        private IDictionaryEnumerator myEnum;

        public int Count
        {
            get
            {
                return dict.Count;
            }
        }
 
        public  T9()
        {
          dict = new OrderedDictionary();
        }
        public int Add(string word) // Функция добавления слова в словарь.
        {
            word = word.ToLower();
            string ResCode = GenerateCode(word);
            if (!ResCode.Equals("0") && !dict.Contains(word)) dict.Add(word, ResCode);
            else if (dict.Contains(word)) return -1;
            else return 0;
            myEnum = dict.GetEnumerator();
            return 1;
        }

        public string GenerateCode(string wordToCode) // Функция генерации подходящего числового кода на основе полученного слова.
        {
            wordToCode = wordToCode.ToLower();
            int length = wordToCode.Length;
            if (length == 0) return "0";
            string Code = string.Empty;
            for (int i = 0; i < length; i++)
            {
                int x = ParseChar(wordToCode[i]);
                if (x != 0) Code += x;
                else return "0";
            }
            return Code;
        }

        public  int ParseChar(char x)   // Функция для определения, что слово состоит из поддерживаемых символов, а также определения подходящей цифры.
            {
                if (x == 40) return 1; // Если символ '@'
                if (x >= 'а' && x <= 'г') return 2; // Если это один из символов АБВГ или абвг
                if (x >= 'д' && x <= 'з') return 3; // Если это один из символов ДЕЖЗ или дежз
                if (x >= 'и' && x <= 'л') return 4; // Если это один из символов ИЙКЛ или ийкл
                if (x >= 'м' && x <= 'п') return 5; // Если это один из символов МНОП или мноп
                if (x >= 'р' && x <= 'у') return 6; // Если это один из символов РСТУ или рсту
                if (x >= 'ф' && x <= 'ч') return 7; // Если это один из символов ФХЦЧ или фхцч
                if (x >= 'ш' && x <= 'ы') return 8; // Если это один из символов ШЩЪЫ или шщъы
                if (x >= 'ь' && x <= 'я') return 9; // Если это один из символов ЬЭЮЯ или ьэюя
                return 0; // Не подходящий символ
            }
 
        public string GetWordCode(string word) // Функция поиска кода по слову.
        {
            /* foreach(KeyValuePair<string,string> pair in dict)
             {
                 if (pair.Key == word) return pair.Value;
             }
             return "0"; */
            string temp = "0";
            while (myEnum.MoveNext())
            {
                if ((myEnum.Key).ToString() == word)
                {
                    temp = (myEnum.Value).ToString();
                    myEnum = dict.GetEnumerator();
                    return temp;
                }                 
            }
            return temp;
        }

        public string[] GetWordFromCode(string code, int amount) // Функция получения слов на основе пользовательского ввода
        {
            Regex regex = new Regex(string.Format(@"^{0}(\w*)", Regex.Escape(code)));
            string[] matches = new string[amount];
            //Array.Clear(matches, 0, amount);
            
         /*   foreach(KeyValuePair<string,string> pair in dict)
            {
                if (regex.IsMatch(pair.Value))
                {
                    MatchSort(pair.Key, 0);
                }
            }
            return matches;
        */
            while (myEnum.MoveNext())
            {
                if (regex.IsMatch((myEnum.Value).ToString()))
                    MatchSort((myEnum.Key).ToString(), 0);
            }
            myEnum = dict.GetEnumerator();
            return matches;

            void MatchSort(string str, int n) // Функция сортировки выборки слов на основе приоритета 
            {
                if (matches[n] == null) matches[n] = str;
                else if (str.Length == code.Length && matches[n].Length > code.Length)
                {
                    string temp = matches[n];
                    matches[n] = str;
                    for (int i = n+1; i< amount; i++)
                    {
                        str = temp;
                        temp = matches[i];
                        matches[i] = str;
                    }
                }
                else if (n + 1 < amount) MatchSort(str, n + 1);
            }
        }

        public void ShowDictionary() // Отображение всего словаря с соотвествующими кодами пользовательского ввода
        {
            /* foreach(KeyValuePair<string,string> pair in dict)
             {
                 Console.WriteLine(pair.Key + '\t' + pair.Value);
             } */
            while (myEnum.MoveNext())
            {
                Console.WriteLine("{0, -25} {1}", myEnum.Key, myEnum.Value);
            }
            myEnum = dict.GetEnumerator();
        }
    }
}
