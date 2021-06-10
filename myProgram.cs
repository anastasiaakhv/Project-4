using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Reflection;

//Assignment 4

public delegate bool F<T>(T element);

//Create generic class
public class Set<T>
{
    //  Declare readonly properties
    //  Count is autoimplemented property
    public int Count { get; }

    private bool isEmpty;
    public bool IsEmpty
    {
        get
        {
            if (this.Count == 0) return true;
            else return false;
        }
    }

    // Declare a list
    protected List<T> TheList = new List<T>();

    //  Methods

    //  Default Constructor 
    public Set()
    {

    }

    // Explicit Value Constructor
    public Set(IEnumerable<T> MyList)
    {
        /* 
           Add every element of the passed list to TheList 
           using enhanced for loop
        */

        // Firstly remove duplicates in the passed list
        List<T> WithoutDuplicate = MyList.Distinct().ToList();

        // Add each element to the set
        foreach (var element in WithoutDuplicate)
        {
            TheList.Add(element);
        }

    }

    // Display Method - in order to display the set in an appropriate format
    public static void display(Set<T> MySet)
    {
        Console.Write("\n{");
        foreach (var element in MySet.TheList)
        {
            Console.Write("{0}, ", element);
        }
        Console.Write("}");
    }

    // Operator Overloading - the function takes two sets as parameters
    public static Set<T> operator +(Set<T> lhs, Set<T> rhs)
    {
        //  Declare two lists: FirstList and SecondList
        var FirstList = new List<T>();
        var SecondList = new List<T>();

        //  Add every element of first object - lhs to the FirstList
        foreach (var element in lhs.TheList)
        {
            FirstList.Add(element);
        }

        //  Add every element of second object - rhs to the SecondList
        foreach (var element in rhs.TheList)
        {
            SecondList.Add(element);
        }

        //  Declare the list which will contain the union of the two sets
        var ListOfUnion = FirstList.Union(SecondList);

        //  Instantiate a new Set object that will contain the union
        Set<T> UnionSet = new Set<T>(ListOfUnion);

        //  Return the union of two sets
        return UnionSet;
    }

    public virtual bool Add(T item)
    {
        // Declare a list
        List<T> OneElementList = new List<T>();

        // Add the item to the list
        OneElementList.Add(item);

        // Declare IEnumerable intersection of the item and the set
        IEnumerable<T> MyIntersection = this.TheList.Intersect(OneElementList);

        /* 
         * Declare a bool EmptyOrNot
         * if EmptyOrNot is true, there exists an intersection
         * if EmptyOrNot is false, there exists no intersection
         */
        bool EmptyOrNot = MyIntersection.Any();

        /* 
         * If there is no intersection Add item to the set 
         * Display the set
         */

        if (EmptyOrNot == false)
        {
            this.TheList.Add(item);
            Console.WriteLine("The element was added to the set");
            return true;
        }

        /* 
         * If there exists intersection do not add item to the set 
         * Display the set
         */

        else
        {
            Console.WriteLine("The element was not added to the set");
            return false;
        }

    }

    public virtual bool Remove(T item)
    {
        // Declare a list
        List<T> OneElementList = new List<T>();

        // Add the item to the list
        OneElementList.Add(item);

        // Declare IEnumerable intersection of the item and the set
        IEnumerable<T> MyIntersection = this.TheList.Intersect(OneElementList);

        // Find out if intersection exists
        bool EmptyOrNot = MyIntersection.Any();

        // If there is no intersection => There is no such element in the collection
        if (EmptyOrNot == false)
        {
            // Can not remove anything 
            Console.Write("\nCould not remove element\n");
            return false;
        }
        // There exists such element in the collection
        // Therefore, one could remove it 
        else
        {
            this.TheList.Remove(item);
            return true;
        }
    }

    public virtual bool Contains(T item)
    {
        List<T> OneElementList = new List<T>();
        // Add the item to the list
        OneElementList.Add(item);

        // Declare IEnumerable intersection of the item and the set
        IEnumerable<T> MyIntersection = this.TheList.Intersect(OneElementList);

        // Find out if intersection exists
        bool EmptyOrNot = MyIntersection.Any();

        if (EmptyOrNot == true)
        {
            Console.WriteLine("The set contains such element");
            return true;
        }
        else
        {
            Console.WriteLine("The set does not contain such element");
            return false;
        }
    }

    public Set<T> Filter(F<T> filterFunction)
    {
        var MyFirstList = new List<T>();


        foreach (var element in this.TheList)
        {

            if ((filterFunction(element)) == true)
            {
                MyFirstList.Add(element);
            }
        }

        Set<T> FilteredSet = new Set<T>(MyFirstList);

        return FilteredSet;
    }

}



public class SortedSet<T> : Set<T> where T : IComparable
{
    public SortedSet()
    {

    }

    public SortedSet(IEnumerable<T> MyList)
    {
        /* 
           Add every element of the passed list to TheList 
           using enhanced for loop
        */

        // Firstly remove duplicates

        List<T> SortedWithoutDuplicate = MyList.Distinct().ToList();

        // Add each element to set

        foreach (var element in SortedWithoutDuplicate)
        {
            TheList.Add(element);
        }

        // Sorting the list
        TheList.Sort();

    }

    public override bool Add(T item)
    {
        // Declare a list
        List<T> OneElementList = new List<T>();

        // Add the item to the list
        OneElementList.Add(item);

        // Declare IEnumerable intersection of the item and the set
        IEnumerable<T> MyIntersection = this.TheList.Intersect(OneElementList);

        // Declare a bool EmptyOrNot

        /* 
         * if EmptyOrNot is true, there exists an intersection
         * if EmptyOrNot is false, there exists no intersection
         */
        bool EmptyOrNot = MyIntersection.Any();

        /* 
         * If there is no intersection Add item to the set 
         * Display the set
         */

        if (EmptyOrNot == false)
        {
            this.TheList.Add(item);
            Console.WriteLine("The element was added to the set");
            this.TheList.Sort();
            return true;
        }

        /* 
         * If there exists intersection do not add item to the set 
         * Display the set
         */

        else
        {
            Console.WriteLine("The element was not added to the set");
            this.TheList.Sort();
            return false;
        }
    }

    public override bool Remove(T item)
    {
        // Declare a list
        List<T> OneElementList = new List<T>();
        // Add the item to the list
        OneElementList.Add(item);

        // Declare IEnumerable intersection of the item and the set
        IEnumerable<T> MyIntersection = this.TheList.Intersect(OneElementList);

        // Find out if intersection exists
        bool EmptyOrNot = MyIntersection.Any();

        if (EmptyOrNot == false)
        {
            // Can not remove anything 
            Console.WriteLine("\n Could not remove element");
            this.TheList.Sort();
            return false;
        }
        // There exists such element in the collection
        // Therefore, one could remove it 
        else
        {
            this.TheList.Remove(item);
            this.TheList.Sort();
            Console.WriteLine("The element was removed from the set\n");
            return true;
        }
    }

    public static SortedSet<T> operator +(SortedSet<T> lhs, SortedSet<T> rhs)
    {
        //  Declare two lists: FirstList and SecondList
        var FirstList = new List<T>();
        var SecondList = new List<T>();

        //  Add every element of first object - lhs to the FirstList
        foreach (var element in lhs.TheList)
        {
            FirstList.Add(element);
        }

        //  Add every element of second object - rhs to the SecondList
        foreach (var element in rhs.TheList)
        {
            SecondList.Add(element);
        }

        //  Declare the list which will contain the union of the two sets
        var ListOfUnion = FirstList.Union(SecondList);

        //  Instantiate a new Set object that will contain the union
        SortedSet<T> UnionSet = new SortedSet<T>(ListOfUnion);

        //  Return the union of two sets
        return UnionSet;
    }
}


class MainClass
{

    public static void Main(string[] args)
    {

        //testing default constructor
        Set<int> someSet = new Set<int>();

        IEnumerable<int> MyNewList = new List<int>();
        Set<int> NewSet = new Set<int>(MyNewList);

        // Testing Explicit Value Constructor 
        IEnumerable<int> ListOne = new List<int>() { 1, 2, 2 };
        IEnumerable<int> ListTwo = new List<int>() { 7, 2, 6 };

        Set<int> SetOne = new Set<int>(ListOne);
        Set<int> SetTwo = new Set<int>(ListTwo);

        Console.Write("\n---------------------");
        Console.Write("\nThe first set is: ");
        Set<int>.display(SetOne);
        Console.WriteLine("\n---------------------\n");

        Console.Write("\n---------------------");
        Console.Write("\nThe second set is: ");
        Set<int>.display(SetTwo);
        Console.WriteLine("\n---------------------\n");

        // Testing for + operator => Union
        Console.Write("\n---------------------");
        NewSet = SetOne + SetTwo;
        Console.Write("\nThe Union : ");
        Set<int>.display(NewSet);
        Console.Write("\n---------------------\n");

        // Testing for Add function
        Console.WriteLine("\n---------------------");
        NewSet.Add(1000);
        NewSet.Add(2);
        Set<int>.display(NewSet);
        Console.WriteLine("\n---------------------\n");

        // Testing for Remove Function
        Console.Write("\n---------------------");
        // Set contains this value
        Console.WriteLine("\nTesting for removing 7");
        NewSet.Remove(7);

        // Set does not contain this value 
        Console.WriteLine("Testing for removing 13");
        NewSet.Remove(13);
        Console.WriteLine("The resulting set");
        Set<int>.display(NewSet);
        Console.WriteLine("\n---------------------\n");

        // Testing for Contains method
        Console.Write("\n---------------------");
        Console.WriteLine("\nTesting Contains Method\n");

        Console.WriteLine("The output must be that the set contains this value (2)");
        Console.Write("Function Output: ");
        NewSet.Contains(2);
        Console.WriteLine("\n\nThe output must be that the set does not contain this value(43)");
        Console.Write("Function Output: ");
        NewSet.Contains(43);

        // Testing for Filterfunction <int>
        F<int> TestingDelegate = new F<int>(F<int>);
        Set<int> FilteredSet = NewSet.Filter(TestingDelegate);

        Console.Write("\n---------------------");
        Console.WriteLine("\nTesting for Delegate");
        Console.WriteLine("The filtered set is set of integers: ");
        Set<int>.display(FilteredSet);
        Console.WriteLine("\n---------------------\n");

        // Testing the program for characters
        // Testing for Filterfunction <char>
        Console.Write("\n---------------------");
        Console.WriteLine("\nTESTING FOR CHARACTERS\n");

        IEnumerable<char> MyStringList = new List<char>();
        Set<char> StringSet = new Set<char>(MyStringList);
        Console.WriteLine("Trying to insert a");
        StringSet.Add('a');

        IEnumerable<char> MyStringListOri = new List<char>();
        Set<char> StringSetOri = new Set<char>(MyStringListOri);
        Console.WriteLine("Trying to insert b");
        StringSet.Add('b');

        IEnumerable<char> MyStringListSami = new List<char>();
        Set<char> StringSetsami = new Set<char>(MyStringListSami);

        StringSetsami = StringSetOri + StringSet;
        Console.Write("\n\nThe Union of the two sets");
        Set<char>.display(StringSet);

        F<char> TestingDelegateChar = new F<char>(F<char>);
        Set<char> FilteredSetChar = StringSetsami.Filter(TestingDelegateChar);
        Console.Write("\n\nEmpty set since the filterfunction selects only integers");
        Set<char>.display(FilteredSetChar);
        Console.WriteLine("\n---------------------\n");

        // All functionalities of S<T> are tested at this point 

        // Testing functionalities of SortedSet <T> 

        //  Testing default constructor
        SortedSet<int> SomeUnsortedSet = new SortedSet<int>();

        IEnumerable<int> MyUnsortedList = new List<int>();
        SortedSet<int> MyUnsortedSet = new SortedSet<int>(MyUnsortedList);

        IEnumerable<int> UnsortedOne = new List<int>() { 1, 2, 7, 9, 23, 55 };
        IEnumerable<int> UnsortedTwo = new List<int>() { 7, 2, 6, 9, 0, 11, 13 };
        SortedSet<int> UnsortedSetOne = new SortedSet<int>(UnsortedOne);
        SortedSet<int> UnsortedSetTwo = new SortedSet<int>(UnsortedTwo);

        Console.WriteLine("First Unsorted Set: {1, 2, 7, 9, 23, 55}");
        Console.WriteLine("Second Unsorted Set: {7, 2, 6, 9, 0, 11, 13};");

        Console.Write("\n---------------------");
        Console.WriteLine("\nDisplaying Sorted Sets\n");

        Console.Write("First:");
        SortedSet<int>.display(UnsortedSetOne);
        Console.Write("\n\nSecond:");
        SortedSet<int>.display(UnsortedSetTwo);

        Console.WriteLine("\n\nThe Union of this sets: ");
        MyUnsortedSet = UnsortedSetOne + UnsortedSetTwo;
        SortedSet<int>.display(MyUnsortedSet);

        Console.Write("\n\n\n-------------------------------------");
        Console.WriteLine("\nAdding 12 and removing 11 from the set\n");
        MyUnsortedSet.Add(12);
        MyUnsortedSet.Remove(11);
        Console.WriteLine("The resulting set:");
        SortedSet<int>.display(MyUnsortedSet);

        //Testing for Contains method
        Console.WriteLine("\n\nTesting for contains method, if the set contains 11");
        MyUnsortedSet.Contains(11);
        Console.WriteLine("\nTesting the same method for 0");
        MyUnsortedSet.Contains(0);

        // testing filterfunction
        F<int> TestingDelegateSorted = new F<int>(F<int>);
        Set<int> FilteredSetSorted = MyUnsortedSet.Filter(TestingDelegateSorted);
        Console.Write("\n\n\n-------------------------------------");
        Console.WriteLine("\nTesting filter function\nThe Output has to be the same sorted set");
        Set<int>.display(FilteredSetSorted);
    }

    // the function filtering integers
    public static bool F<T>(T element)
    {
        if (element is int) return true;
        else return false;
    }

}
