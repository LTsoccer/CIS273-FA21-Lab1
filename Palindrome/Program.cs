using System;
using System.Collections.Generic;

namespace Palindrome
{
    public class Program
    {
        static void Main(string[] args)
        {
            LinkedList<String> doublyLinkedList = new LinkedList<String>();
            doublyLinkedList.AddLast("alex");
            doublyLinkedList.AddLast("alex");
            Console.WriteLine(IsPalindrome(doublyLinkedList));
        }

        public static bool IsPalindrome<T>(LinkedList<T> linkedList)
        {
            if (linkedList.Count == 0)
            {
                return true;
            }
            else if (linkedList.Count == 1)
            {
                return true;
            }
            else if (linkedList.First.Value.ToString() == linkedList.Last.Value.ToString())
            {
                linkedList.RemoveFirst();
                linkedList.RemoveLast();
                while (linkedList.Count > 1)
                {
                    IsPalindrome(linkedList);
                }
                   if (IsPalindrome(linkedList) == true)
                {
                    return true;
                }
                   else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

    }
}
