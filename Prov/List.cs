using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

class List
{
    //Private set so that it can't be accessed outside the class
    private Node head;

    public List()
    {
        head = null;
    }

    /// <summary>
    /// Add value to líst.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="input"></param>
    public void AddValue<T>(T input)
    {
        if (head == null)
        {
            //If there is no first value, add the input value
            head = new Node(input);
        }
        else
        {
            //If there is a first value, make a new node
            Node current = head;
            //While the next value exists, set the current reference to the next one
            while (current.next != null)
            {
                current = current.next;
            }
            //Create a new node for the next value at the end of the list
            current.next = new Node(input);
        }
    }

    /// <summary>
    /// Add value to the position of the index. Index 0 is first in the list.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <param name="index"></param>
    public void AddValueAtIndex<T>(T value, int index)
    {
        Node previous = null;
        Node current = head;

        //Abort if the index is negative
        if (index < 0)
        {
            return;
        }

        //If the list is empty or the index is 0, add the value at the beginning
        if (head == null || index == 0)
        {
            Node newNode = new Node(value);
            newNode.next = head;
            head = newNode;
            return;
        }

        //Move along the list index amount of timnes
        for (int i = 0; i < index; i++)
        {
            //If the next value doesn't exist, it's the end of the list. Then add the value like normal.  
            if (current.next == null)
            {
                AddValue(value);
                return;
            }
            previous = current;
            current = current.next;
        }

        //Adds the value to a new node if it's not the first value in the lsit
        Node newNode2 = new Node(value);
        newNode2.next = current;
        previous.next = newNode2;
    }

    /// <summary>
    /// Sorts the list.
    /// </summary>
    public void SortOld() //Temporary sloppy solution. Will make my own sorting if I have time
    {
        //Can't sort an empty list or a list with only one item
        if (GetLength() == 0 || GetLength() == 1)
        {
            return;
        }

        //Creating a list for each variable type and one (object) for things that don't fit in all the others
        List<string> stringList = new List<string>();
        List<int> intList = new List<int>();
        List<float> floatList = new List<float>();
        List<double> doubleList = new List<double>();
        List<bool> boolList = new List<bool>();
        List<long> longList = new List<long>();
        List<object> objectList = new List<object>();

        Node current = head;

        //Loops through the whole linked list and places each value in the corresponding list
        while (current != null)
        {
            if (current.value is string)
            {
                stringList.Add((string)current.value);
            }
            else if (current.value is int)
            {
                intList.Add((int)current.value);
            }
            else if (current.value is float)
            {
                floatList.Add((float)current.value);
            }
            else if (current.value is double)
            {
                doubleList.Add((double)current.value);
            }
            else if (current.value is bool)
            {
                boolList.Add((bool)current.value);
            }
            else if (current.value is long)
            {
                longList.Add((long)current.value);
            }
            else
            {
                objectList.Add((object)current.value);
            }

            current = current.next;
        }

        //Sorts the lists
        stringList.Sort();
        intList.Sort();
        doubleList.Sort();
        boolList.Sort();
        longList.Sort();
        floatList.Sort();
        objectList.Sort();

        //Clears this linked list so new values can be added
        Clear();

        //Checks which lists have something in them, then adds all values in them to the linked list
        if (stringList.Count > 0)
        {
            foreach (string string_ in stringList)
            {
                AddValue(string_);
            }
        }
        if (intList.Count > 0)
        {
            foreach(int int_ in intList)
            {
                AddValue(int_);
            }
        }
        if (floatList.Count > 0)
        {
            foreach (float float_ in floatList)
            {
                AddValue(float_);
            }
        }
        if (doubleList.Count > 0)
        {
            foreach (double double_ in doubleList)
            {
                AddValue(double_);
            }
        }
        if (longList.Count > 0)
        {
            foreach(long long_ in longList)
            {
                AddValue(long_);
            }
        }
        if (boolList.Count > 0)
        {
            foreach (bool bool_ in boolList)
            {
                AddValue(bool_);
            }
        }
        if (objectList.Count > 0)
        {
            foreach(object object_ in objectList)
            {
                AddValue(object_);
            }
        }
    }

    public void BubbleSort()
    {
        //Abort if there only is one or no values
        if (GetLength() < 2)
        {
            return;
        }

        //Move along the list the same amount of times as values in the list
        for (int i = 0; i < GetLength(); i++)
        {
            Node current = head;
            //Go through the list to compare each value with the next one
            while (current != null && current.next != null)
            {
                //CompareTo returns negative if current.value goes before current.next.value if current.value is before in alphabetical order
                if (Convert.ToString(current.value).CompareTo(Convert.ToString(current.next.value)) < 0)
                {
                    SwapNodes(current, current.next); //Swap place of the nodes
                }
                current = current.next;
            }
        }
    }
    public void SwapNodes(Node node1, Node node2)
    {
        //Abort if they are the same
        if (node1.value == node2.value)
        {
            return;
        }

        //Find the previous node to node1
        Node previousNode1 = null;
        Node current = head;
        while (current != null && current != node1)
        {
            previousNode1 = current;
            current = current.next;
        }

        //Find the previous node to node2
        Node previousNode2 = null;
        current = head;
        while (current != null && current != node2)
        {
            previousNode2 = current;
            current = current.next;
        }

        //abort if node doesn't exist
        if (current == null)
        {
            return;
        }

        //Update pointers from the previous nodes
        if (previousNode1 != null) //If it's not first in the list, make the next one node2
        {
            previousNode1.next = node2;
        }
        else { head = node2; }//Make node2 head becuase there was no previous node

        if (previousNode2 != null)
        {
            previousNode2.next = node1;
        }
        else { head = node1; }

        //Handles the exception when one of the nodes are the head
        if (node1 == head) { head = node2; }
        else if (node2 == head) { head = node1; }

        //Temporary node keeps track of node1.next so it can be replaced by node2.next and be used later, swapping the nodes next position
        Node temporaryNode = node1.next;
        node1.next = node2.next;
        node2.next = temporaryNode;
    }

    /// <summary>
    /// Remove position in the list, 0 is first. Returns a bool.
    /// </summary>
    /// <param name="idex"></param>
    public bool RemoveAtIndex(int index)
    {
        Node previous = null;
        Node current = head;

        //Abort if the index is negative
        if (index < 0) { return false; }

        //Move along the list index amount of timnes
        for (int i = 0; i < index; i++)
        {
            //Abort if the index is larger than the list length
            if (current.next == null)
            {
                return false;
            }
            previous = current;
            current = current.next;
        }
        
        //Checks if it's the first value (index = 0)
        if (previous == null)
        {
            head = head.next;
            return true;
        }

        //Delete the current value by moving the pointer past it and to the next node
        previous.next = current.next;
        return true;
    } 

    /// <summary>
    /// Removes the input value.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="input"></param>
    public void RemoveValue<T>(T input)
    {
        //Keep track of the previous and current node. The previous node is set to null because there is no node before head
        Node previous = null;
        Node current = head;

        while (current != null)
        {
            if (current.value.Equals(input))
            {
                //If the head node has the value because the previous node never got assigned a value, it skips it like the RemoveFirst() method
                if (previous == null)
                {
                    head = head.next;
                }
                else
                {
                    //Moves the reference from the previous node to the next one, skipping the one with the value that should get removed
                    previous.next = current.next;
                }
                return;
            }

            //Go to the next node so that the while loop works. Same as the other functions. 
            previous = current;
            current = current.next;
        }
    }

    /// <summary>
    /// Prints all values.
    /// </summary>
    public void PrintAll()
    {
        //Creates a new node at head, i.e the start of the list
        Node current = head;
        //While the current one exists, print its value then set the current node to the next one. Will stop if the next node (that becomes the current) equals null, which means that it doesn't exist.
        while (current != null)
        {
            //Creates an array with two strings, the first one being the first half of the value's variable type that just contains the
            //word "System", and the second half being the variable type I want to print in the console. 
            string type = current.value.GetType().ToString();
            if (type.Contains("."))
            {
                string[] type2 = type.Split(".");
                Console.WriteLine($"{type2[1]}: {current.value}");
            }
            else
            {
                Console.WriteLine($"{type}: {current.value}");
            }
            current = current.next;
        }
    }

    /// <summary>
    /// Removes the first item in the list.
    /// </summary>
    public void RemoveFirst()
    {
        //Moves the refernece to the second node so that becomes the new head
        head = head.next;
    }

    /// <summary>
    /// Returns the amount of items in the list.
    /// </summary>
    /// <returns></returns>
    public int GetLength()
    {
        Node current = head;
        int length = 0;
        while(current != null)
        {
            current = current.next;
            length++;
        }
        return length;
    }

    /// <summary>
    /// Returns an object of the value at the index. 0 is first.
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public object GetValueFromIndex(int index)
    {
        Node current = head;

        for (int i = 0; i < index; i++)
        {
            //Abort if the index is larger than the list length
            if (current == null)
            {
                return ("Index out of bounds of the list.");
            }
            current = current.next;
        }

        return current.value;
    }

    /// <summary>
    /// Clears the list.
    /// </summary>
    public void Clear()
    {
        head = null;
    }

    /// <summary>
    /// Searches the list for specified item and returns a bool.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="input"></param>
    /// <returns></returns>
    public bool Contains<T>(T input)
    {
        Node current = head;

        while (current != null)
        {
            if (current.value.Equals(input))
            {
                return true;
            }
            current = current.next;
        }
        return false;
    }
}

class Node
{   
    public object value { get; }
    public Node next { get; set; }
    //Constructor, takes an input and sets the node's value to it. Sets the next node in the list to null, meaning that it doesn't "exist" yet.
    public Node(object input)
    {
        value = input;
        next = null;
    }
}
