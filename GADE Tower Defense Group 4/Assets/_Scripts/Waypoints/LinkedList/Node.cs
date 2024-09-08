using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node<T>
{
    public T Data { get; set; }
    public Node<T> NextNode { get; set; }

    public Node(T data)
    {
        Data = data;
    }
}
