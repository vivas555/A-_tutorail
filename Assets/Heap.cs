﻿using UnityEngine;
using System.Collections;
using System;

public class Heap <T> where T : IHeapitem<T>
{

    T[] items;

    int currentitemCount;

    public Heap(int maxHeapSize)
    {
        items = new T[maxHeapSize];
    }

    public void Add(T item)
    {
        item.HeapIndex = currentitemCount;
        items[currentitemCount] = item;
        SortUp(item);
        currentitemCount++;
    }

    private void SortUp(T item)
    {
        int parentIndex = (item.HeapIndex - 1)/2;

        while (true)
        {
            T parentItem = items[parentIndex];
            if (item.CompareTo(parentItem) > 0)
            {
                Swap(item,parentItem);
            }
            else
            {
                break;
            }
            parentIndex = (item.HeapIndex - 1)/2;
        }
    }

    public T RemoveFirst()
    {
        T firstItem = items[0];
        currentitemCount--;
        items[0] = items[currentitemCount];
        items[0].HeapIndex = 0;
        SortDown(items[0]);
        return firstItem;
    }

    public int Count
    {
        get { return currentitemCount; }
    }

    //Pour le pathfindinf pas besoin de caller SortDown ici, mais dans un autre cas faudrait
    public void UpdateItem(T item)
    {
        SortUp(item);
    }

    public bool Contains(T item)
    {
        return Equals(items[item.HeapIndex], item);
    }

    void SortDown(T item)
    {
        while (true)
        {
            int childIndexLeft = item.HeapIndex*2 + 1;
            int childIndexRight = item.HeapIndex*2 + 2;
            int swapIndex = 0;

            if (childIndexLeft < currentitemCount)
            {
                swapIndex = childIndexLeft;
                if (childIndexRight<currentitemCount)
                {
                    if (items[childIndexLeft].CompareTo(items[childIndexRight])<0)
                    {
                        swapIndex = childIndexRight;
                    }
                }

                if (item.CompareTo(items[swapIndex])<0)
                 {
                Swap(item,items[swapIndex]);
                 }
                else
                 {
                return;
                   }
            }
            
           else
           {
            return;
            
           }

        }
        
    }

    void Swap(T itemA, T itemB)
    {
        items[itemA.HeapIndex] = itemB;
        items[itemB.HeapIndex] = itemA;
        int itemAIndex = itemA.HeapIndex;
        itemA.HeapIndex = itemB.HeapIndex;
        itemB.HeapIndex = itemAIndex;
    }
    
}
public interface IHeapitem<T>: IComparable<T>
    {
        int HeapIndex
        {
            get; set;
        }
    }