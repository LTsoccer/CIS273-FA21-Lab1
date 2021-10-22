using System;
namespace KthToLast
{

    public class DoublyLinkedListNode<T>
    {
        public T Data { get; set; }
        public DoublyLinkedListNode<T> Next { get; set; }
        public DoublyLinkedListNode<T> Prev { get; set; }

        public DoublyLinkedListNode(T data = default(T), DoublyLinkedListNode<T> prev = null, DoublyLinkedListNode<T> next = null)
        {
            Data = data;
            Prev = prev;
            Next = next;

        }

        public override string ToString()
        {
            return Data.ToString();
        }
    }

    public class DoublyLinkedList<T> : IList<T>
    {
        public DoublyLinkedListNode<T> Head { get; set; }
        public DoublyLinkedListNode<T> Tail { get; set; }
        public DoublyLinkedList()
        {
            Head = null;
            Tail = null;
        }

        public int Length
        {
            get
            {
                int count = 0;
                var currentNode = Head;
                while (currentNode != null)
                {
                    count++;
                    currentNode = currentNode.Next;
                }

                return count;
            }
        }

        public bool IsEmpty => Head == null;

        public T First => Head.Data;

        public T Last => Tail.Data;

        public T this[int index] => throw new NotImplementedException();

        public void Append(T item)
        {
            var newNode = new DoublyLinkedListNode<T>(item);

            // empty list
            if (IsEmpty)
            {
                Head = newNode;
                Tail = newNode;
            }
            // non empty list
            else
            {
                // Add new node after Tail
                Tail.Next = newNode;
                newNode.Prev = Tail;

                // Update Tail
                Tail = newNode;

            }
        }

        public void Clear()
        {
            Head = null;
            Tail = null;
        }

        public int FirstIndexOf(T existingValue)
        {
            int index = 0;

            var currentNode = Head;
            while (currentNode.Next != null)
            {
                if (currentNode.Data.Equals(existingValue))
                {
                    return index;
                }
                index++;
                currentNode = currentNode.Next;
            }
            if (currentNode.Next == null)
            {
                if (currentNode.Data.Equals(existingValue))
                {
                    return index;
                }
            }

            return -1;
        }

        public void InsertAfter(T newValue, T existingValue)
        {
            if (IsEmpty)
            {
                Prepend(newValue);
            }
            else
            {
                var index = FirstIndexOf(existingValue) + 1;
                if (index == 0)
                {
                    Append(newValue);
                }
                else if (index == Length)
                {
                    Append(newValue);
                }
                else
                {
                    InsertAt(newValue, index);
                }
            }
        }

        public void InsertAt(T newValue, int index)
        {
            if (IsEmpty)
            {
                Prepend(newValue);
            }
            else if (index > Length)
            {
                throw new IndexOutOfRangeException();
            }

            else if (index < 0)
            {
                throw new IndexOutOfRangeException();
            }
            else if (index == Length)
            {
                Append(newValue);
            }
            else if (index == 0)
            {
                Prepend(newValue);
            }
            else
            {
                var newNode = new DoublyLinkedListNode<T>(newValue);
                var prevNode = Head;
                var currentNode = Head.Next;
                for (int i = 1; i < index; i++)
                {
                    prevNode = prevNode.Next;
                    currentNode = currentNode.Next;
                }
                newNode.Next = currentNode;
                currentNode.Prev = newNode;
                prevNode.Next = newNode;
                newNode.Prev = prevNode;
            }
        }

        public void Prepend(T item)
        {
            var newNode = new DoublyLinkedListNode<T>(item);

            // empty list
            if (IsEmpty)
            {
                Head = newNode;
                Tail = newNode;
            }
            // non empty list
            else
            {
                // Add new node before Head
                newNode.Next = Head;
                Head.Prev = newNode;

                // Update Head
                Head = newNode;
            }
        }

        public void Remove(T value)
        {
            // If the list is empty, return immediately 
            if (IsEmpty)
            {
                return;
            }

            // Remove head
            if (Head.Data.Equals(value))
            {
                // Removing node from 1-element list
                if (Head == Tail)
                {
                    Tail = null;
                    Head = null;
                }
                else
                {
                    Head = Head.Next;
                    Head.Prev = null;
                }
                return;
            }

            // Remove non-head node

            var currentNode = Head;

            while (currentNode != null)
            {
                if (currentNode.Next != null && currentNode.Next.Data.Equals(value))
                {
                    var nodeToDelete = currentNode.Next;
                    if (nodeToDelete == Tail)
                    {
                        currentNode.Next = null;
                        Tail.Prev = null;
                        Tail = currentNode;
                    }
                    else
                    {
                        // update previous node's next to skip the deleted node
                        currentNode.Next.Next.Prev = currentNode;
                        currentNode.Next = currentNode.Next.Next;
                        nodeToDelete.Next = null;
                        nodeToDelete.Prev = null;
                    }
                }

                currentNode = currentNode.Next;
            }
        }

        public void RemoveAt(int index)
        {
            if (IsEmpty)
            {
                throw new IndexOutOfRangeException();
            }
            else if (index >= Length)
            {
                throw new IndexOutOfRangeException();
            }

            else if (index < 0)
            {
                throw new IndexOutOfRangeException();
            }

            else
            {
                var currentNode = Head;
                if (index == 0)
                {
                    Remove(Head.Data);
                }
                else if (index == Length - 1)
                {
                    Remove(Tail.Data);
                }
                else
                {
                    for (int i = 0; i < index; i++)
                    {
                        currentNode = currentNode.Next;
                    }
                    Remove(currentNode.Data);
                }
            }
        }

        public IList<T> Reverse()
        {
            var result = new DoublyLinkedList<T>();

            var currentNode = Tail;
            while (currentNode != null)
            {
                //Append every single one of them
                result.Append(currentNode.Data);

                currentNode = currentNode.Prev;
            }

            return result;
        }

        public override string ToString()
        {
            string result = "[";
            var currentNode = Head;
            while (currentNode != null)
            {
                result += currentNode.Data;
                if (currentNode != Tail)
                {
                    result += ", ";
                }
                currentNode = currentNode.Next;
            }

            result += "]";

            return result;
        }

        public bool Contains(T item)
        {
            throw new NotImplementedException();
        }

        public void InsertAt(int index, T item)
        {
            throw new NotImplementedException();
        }

        public T KthToLast(int k)
        {
            if (IsEmpty)
            {
                throw new IndexOutOfRangeException();
            }
            else
            {
                var currentNode = Tail;
                for (int i = Length; i > (Length - k); i--)
                {
                    currentNode = currentNode.Prev;
                }
                return currentNode.Data;
            }
        }
        }
    }
