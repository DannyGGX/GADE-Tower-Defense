using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomLinkedList<T>
{
    public Node<T> Head { get; private set; }
    public Node<T> Last { get; private set; }
    public int Length { get; private set; }
    
    /// <summary>
    /// Add a node to the end of the linked list
    /// </summary>
    /// <param name="data"> The data that the node will store</param>
    public void Add(T data)
    {
        Node<T> newNode = new Node<T>(data);
        if (IsLinkedListEmpty())
        {
            Head = newNode;
            Last = newNode;
        }
        else
        {
            Last.NextNode = newNode;
            Last = newNode;
        }
        Length++;
        
        Last.NextNode = Head; // Circular
    }
    private bool IsLinkedListEmpty() => Head == null;

    public void AddFirst(T data)
    {
        Node<T> newNode = new Node<T>(data);
        if (IsLinkedListEmpty())
        {
            Head = newNode;
            Last = newNode;
        }
        else
        {
            newNode.NextNode = Head;
            Head = newNode;
        }
        Length++;
        
        Last.NextNode = Head; // Circular
    }


    public void RemoveFirst()
    {
        if (IsLinkedListEmpty())
        {
            return;
        }
        Head = Head.NextNode;
        Length--;
        
        Last.NextNode = Head; // Circular
    }

    public void Remove(Node<T> nodeToRemove)
    {
        if (nodeToRemove == Head)
        {
            RemoveFirst();
        }
        else if (nodeToRemove == Last)
        {
            
        }
        //Search for last element;
        Length--;

        Last.NextNode = Head; // Circular
        throw new NotImplementedException();
    }

    public void PrintAll(Node<T> head)
    {
        Node<T> currentNode = head;
        Debug.Log(currentNode.Data);

        while (currentNode.NextNode != Last)
        {
            currentNode = currentNode.NextNode;
            Debug.Log(currentNode.Data);
        }
    }
}
