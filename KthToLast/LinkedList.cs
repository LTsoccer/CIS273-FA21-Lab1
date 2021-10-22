using System;
namespace KthToLast
{

        public class LinkedListNode<T>
        {
            public T Data { get; set; }
            public LinkedListNode<T> Next { get; set; }

            public LinkedListNode(T data = default(T), LinkedListNode<T> next = null)
            {
                Data = data;
                Next = next;
            }

            public override string ToString()
            {
                return Data.ToString();
            }
        }


        public class LinkedList<T> : IList<T>
        {
            public LinkedListNode<T> Head { get; set; }
            public LinkedListNode<T> Tail { get; set; }


            public LinkedList()
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

            public void Append(T item)
            {
                var newNode = new LinkedListNode<T>(item);

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

                //for ( currentNode = Head, index=0; currentNode.Next != null; currentNode = currentNode.Next, index++)
                //{
                //    if (currentNode.Data.Equals(existingValue))
                //    {
                //        return index;
                //    }
                //}

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
                    var newNode = new LinkedListNode<T>(newValue);
                    var prevNode = Head;
                    var currentNode = Head.Next;
                    for (int i = 1; i < index; i++)
                    {
                        prevNode = prevNode.Next;
                        currentNode = currentNode.Next;
                    }
                    newNode.Next = currentNode;
                    prevNode.Next = newNode;
                index++;
                currentNode = currentNode.Next;
            }
            }

            public void Prepend(T item)
            {
                var newNode = new LinkedListNode<T>(item);

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
                            Tail = currentNode;
                        }
                        else
                        {
                            // update previous node's next to skip the deleted node
                            currentNode.Next = currentNode.Next.Next;
                            nodeToDelete.Next = null;
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
                var result = new LinkedList<T>();

                var currentNode = Head;
                while (currentNode != null)
                {
                    //Prepend every single one of them
                    result.Prepend(currentNode.Data);

                    currentNode = currentNode.Next;
                }

                return result;
            }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index > Length - 1)
                {
                    throw new IndexOutOfRangeException();
                }

                int currentIndex = 0;
                for (var currentNode = Head; currentNode != null; currentNode = currentNode.Next)
                {
                    if (currentIndex == index)
                    {
                        return currentNode.Data;
                    }

                    currentIndex++;
                }

                throw new IndexOutOfRangeException();
            }
        }

        public bool Contains(T item)
        {
            for (var currentNode = Head; currentNode != null; currentNode = currentNode.Next)
            {
                if (currentNode.Data.Equals(item))
                {
                    return true;
                }
            }

            return false;
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
            else if (k == 0)
            {
                return Tail.Data;
            }
            else if (k == Length)
            {
                return Head.Data;
            }
            else
            {
                var currentNode = Head;
                for (int i = 1; i < (Length - k); i++)
                {
                    currentNode = currentNode.Next;
                }
                return currentNode.Data;
            }
        }

        public T KthToLast(int k)
        {
            throw new NotImplementedException();
        }
    }
    }
