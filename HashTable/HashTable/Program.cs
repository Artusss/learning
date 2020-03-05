using System;
using System.Collections.Generic;
using System.Linq;

namespace HashTable
{
    class Item
    {
        public string Key { get; private set; }
        public string Value { get; private set; }

        public Item(string Key, string Value)
        {
            this.Key = Key;
            this.Value = Value;
        }
    }

    class HashTable
    {
        private const int maxHashLength = 10;

        private Dictionary<int, List<Item>> HashTableDict;

        public HashTable()
        {
            HashTableDict = new Dictionary<int, List<Item>>();
        }

        public void Insert(string Key, string Value)
        {
            Item item = new Item(Key, Value);

            int hashCode = this.GetHash(Key);

            if (this.HashTableDict.ContainsKey(hashCode))
            {
                this.HashTableDict[hashCode].Add(item);
            }
            else
            {
                List<Item> ItemList = new List<Item>() { item };

                this.HashTableDict.Add(hashCode, ItemList);
            }
        }

        public void Delete(string Key)
        {
            int hashCode = this.GetHash(Key);

            if (!this.HashTableDict.ContainsKey(hashCode))
            {
                return;
            }
            
            List<Item> ItemList = this.HashTableDict[hashCode];

            var item = ItemList.SingleOrDefault(i => i.Key == Key);

            if (item != null)
            {
                ItemList.Remove(item);
            }
        }

        public string Search(string Key)
        {
            int hashCode = this.GetHash(Key);

            if (!this.HashTableDict.ContainsKey(hashCode))
            {
                return "Not found";
            }

            List<Item> ItemList = this.HashTableDict[hashCode];

            var item = ItemList.SingleOrDefault(i => i.Key == Key);

            if (item != null)
            {
                return item.Value; 
            }

            return "Not found";
        }

        private int GetHash(string Key)
        {
            if (Key.Length == 0)
            {
                throw new Exception("Empty key");
            }
            else if (Key.Length > maxHashLength)
            {
                throw new Exception("Over max size");
            } 

            return Key.Length;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            HashTable hashTable = new HashTable();

            hashTable.Insert("qwer1", "barbaz1");
            hashTable.Insert("qwer12", "barbaz12");
            hashTable.Insert("qwer123", "barbaz123");
            hashTable.Insert("qwer1234", "barbaz1234");
            hashTable.Insert("qwer12345", "barbaz12345");
            hashTable.Insert("qwer123456", "barbaz123456");

            Console.WriteLine($"Key: qwer1; Value: {hashTable.Search("qwer1")}");
            Console.WriteLine($"Key: qwer12; Value: {hashTable.Search("qwer12")}");
            Console.WriteLine($"Key: qwer123; Value: {hashTable.Search("qwer123")}");
            Console.WriteLine($"Key: qwer1234; Value: {hashTable.Search("qwer1234")}");
            Console.WriteLine($"Key: qwer12345; Value: {hashTable.Search("qwer12345")}");
            Console.WriteLine($"Key: qwer123456; Value: {hashTable.Search("qwer123456")}");

            hashTable.Delete("qwer12");
            hashTable.Delete("qwer1234");
            hashTable.Delete("qwer123456");

            Console.WriteLine($"Key: qwer1; Value: {hashTable.Search("qwer1")}");
            Console.WriteLine($"Key: qwer12; Value: {hashTable.Search("qwer12")}");
            Console.WriteLine($"Key: qwer123; Value: {hashTable.Search("qwer123")}");
            Console.WriteLine($"Key: qwer1234; Value: {hashTable.Search("qwer1234")}");
            Console.WriteLine($"Key: qwer12345; Value: {hashTable.Search("qwer12345")}");
            Console.WriteLine($"Key: qwer123456; Value: {hashTable.Search("qwer123456")}");
            Console.ReadKey();
        }
    }
}
