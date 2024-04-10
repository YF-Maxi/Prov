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
    public void Sort()
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
                //Checking if converting the values to double is possible. Using double instead of float in case the user inputs a very large value.
                if (double.TryParse(Convert.ToString(current.value), out double result1) && double.TryParse(Convert.ToString(current.next.value), out double result2))
                {
                    //Converting the values to double and comparing them. Without this part, numbers soemetimes get sorted wrong
                    if (Convert.ToDouble(current.value).CompareTo(Convert.ToDouble(current.next.value)) > 0)
                    {
                        SwapNodes(current, current.next); //Swap place of the nodes
                    }
                }
                //If converting to double wasn't possible, it converts them to strings instead and compares them
                else if (Convert.ToString(current.value).CompareTo(Convert.ToString(current.next.value)) > 0)
                {
                    SwapNodes(current, current.next);
                }
                current = current.next;
            }
        }
    }

    public void SwapNodes(Node node1, Node node2)
    {
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
